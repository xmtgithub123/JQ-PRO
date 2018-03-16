using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;

namespace Pay.Controllers
{
   
  
    [Description("PayBalanceEngineering")]
    public class PayBalanceEngineeringController : CoreController
    { 
        private DBSql.Pay.PayBalanceEngineering op = new DBSql.Pay.PayBalanceEngineering();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        private DBSql.Pay.PayBalanceUserAccount userCount = new DBSql.Pay.PayBalanceUserAccount();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("UnPreBalance")));
            project.Edit(p=>(p.ProjStatus==(int)DataModel.ProjStatus.完成)&&p.ProjBanlanceStatus==0,u=>new DataModel.Models.Project() { ProjBanlanceStatus=(int)DataModel.ProjBalanceState.未结清});
            project.UnitOfWork.SaveChanges();
            ////ViewBag.permission = "['mb']";
            ////ViewBag.permission = "['EnterBalance']";
            ////ViewBag.permission = "['SetBalance']";
            ////ViewBag.permission = "['SetUnBanlance']";
            //ViewBag.permission = "['mb'],['EnterBalance'],['SetBalance'],['SetUnBanlance'],['SetProduct']";
            return View();
        }
        #endregion

        #region  技术人员结算列表
        [Description("技术人员结算")]
        public ActionResult TechList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("PreBalanceTech")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            string projId = Request.QueryString["projId"];
            var list = project.GetAllLeafNodeProjInfo().Where(p => p.ProjStatus == (int)DataModel.ProjStatus.完成).ToList();//获取完成的项目信息
            if(!string.IsNullOrEmpty(projId))
            {
                list = list.Where(p=>p.Id==TypeHelper.parseInt(projId)).ToList();
            }
            else
            {
                list = (from item in list
                       where op.GetList(p => p.EngineeringID == item.Id && p.BalanceState == (int)DataModel.BalanceStatus.预结算).Count() == 0
                       select item).ToList();
            }

            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase"];
            string isProjBalanceState = Request.Form["isProjBalanceState"];
            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];

            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                  list = list.Where(p=>p.ProjName.Contains(KeyJQSearch)).ToList();
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                list = list.Where(p => p.ProjPhaseIds.Contains(ProjectPhase)).ToList();
            }
            if (!string.IsNullOrEmpty(isProjBalanceState))
            {
                list = list.Where(p => p.ProjBanlanceStatus == TypeHelper.parseInt(isProjBalanceState)).ToList();
            }

            if (!string.IsNullOrEmpty(startTime) && TypeHelper.isDateTime(startTime))
            {
                list = list.Where(p => p.DateCreate >= DateTime.Parse(startTime).AddHours(00.00)).ToList();
                
            }
            if (!string.IsNullOrEmpty(endTime) && TypeHelper.isDateTime(endTime))
            {
                list = list.Where(p => p.DateCreate <= DateTime.Parse(endTime).AddHours(23.59)).ToList();
            }
             
            var targetLsit = from item in list
                             let state= (int)DataModel.ProjBalanceState.已结清
                             //where op.GetList(p=>p.EngineeringID==item.Id&&p.BalanceState==(int)DataModel.BalanceStatus.预结算).Count()==0 ///排除处于预结算的项目信息
                             select new
                             {
                                 item.Id,
                                 item.ProjNumber,//项目编号
                                 item.ProjName,//项目名称
                                 item.ProjEmpName,//设总姓名
                                 item.DateCreate,//立项时间
                                 item.ProjBanlanceFee,//产值
                                 item.ProjPhaseIds,
                                 Status=item.ProjBanlanceStatus== state ? "已结清":"未结清",
                                 DistributeFee=op.GetDistributeFee(item.Id),//分配产值
                                 UnDistrFee= item.ProjBanlanceFee- op.GetDistributeFee(item.Id)

                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = targetLsit
            });
        }
        #endregion


        #region   技术人员结算信息Json
        [HttpPost]
        public string TechJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(p=>p.BalanceState==(int)DataModel.BalanceStatus.预结算,PageModel).ToList();

            string KeyJQSearch = Request.Form["KeyJQSearch"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                list = list.Where(p => project.Get(p.EngineeringID).ProjName.Contains(KeyJQSearch)).ToList();
            }

            if (!string.IsNullOrEmpty(Request.Form["ProjectPhase"]))
            {
                string ProjectPhase = Request.Form["ProjectPhase"];
                list = list.Where(p => project.Get(p.EngineeringID).ProjPhaseIds.Contains(ProjectPhase)).ToList();
            }

            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];
            if (!string.IsNullOrEmpty(startTime))
            {
                list = list.Where(p => project.Get(p.EngineeringID).DateCreate >= TypeHelper.parseDateTime(startTime)).ToList();
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                list = list.Where(p => project.Get(p.EngineeringID).DateCreate <= TypeHelper.parseDateTime(endTime).AddDays(1)).ToList();
            }


            var target = from item in list
                         let pro = project.Get(item.EngineeringID)
                         let state = (int)DataModel.ProjBalanceState.已结清
                         where pro!=null
                         select new
                         { 
                             item.Id,
                             projId = pro == null ? 0 : pro.Id,
                             ProjNumber = pro == null ? "" : pro.ProjNumber,//项目编号
                             ProjName = pro == null ? "" : pro.ProjName,//项目名称
                             ProjEmpName = pro == null ? "" : pro.ProjEmpName,//设总姓名
                             DateCreate = pro == null?new DateTime(1900,1,1):pro.DateCreate,//立项时间
                             ProjBanlanceFee=pro==null?0:pro.ProjBanlanceFee,//产值
                             Status = pro==null?"":(pro.ProjBanlanceStatus == state ? "已结清" : "未结清"),
                             DistributeFee = op.GetDistributeFee(item.EngineeringID),//分配产值
                             UnDistrFee = pro==null?0:pro.ProjBanlanceFee - op.GetDistributeFee(item.EngineeringID)


                         };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

        #region  技术人员历史结算信息
        [HttpPost]
        public string HisTechStastics()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = project.GetAllLeafNodeProjInfo().Where(p => p.ProjStatus == (int)DataModel.ProjStatus.完成).ToList();//获取完成的项目信息

            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase"];
            string isProjBalanceState = Request.Form["isProjBalanceState"];
            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];

            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                list = list.Where(p => p.ProjName.Contains(KeyJQSearch)).ToList();
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                list = list.Where(p => p.ProjPhaseIds.Contains(ProjectPhase)).ToList();
            }
            if (!string.IsNullOrEmpty(isProjBalanceState))
            {
                list = list.Where(p => p.ProjBanlanceStatus == TypeHelper.parseInt(isProjBalanceState)).ToList();
            }

            if (!string.IsNullOrEmpty(startTime) && TypeHelper.isDateTime(startTime))
            {
                list = list.Where(p => p.DateCreate >= DateTime.Parse(startTime).AddHours(00.00)).ToList();

            }
            if (!string.IsNullOrEmpty(endTime) && TypeHelper.isDateTime(endTime))
            {
                list = list.Where(p => p.DateCreate <= DateTime.Parse(endTime).AddHours(23.59)).ToList();
            }
            
            
            var targetLsit = from item in list
                             let state = (int)DataModel.ProjBalanceState.已结清
                             where op.GetList(p=>p.EngineeringID==item.Id&&p.BalanceState==(int)DataModel.BalanceStatus.已结算).Count()>0 ///结算的项目信息
                             select new
                             {
                                 item.Id,
                                 item.ProjNumber,//项目编号
                                 item.ProjName,//项目名称
                                 item.ProjEmpName,//设总姓名
                                 item.DateCreate,//立项时间
                                 item.ProjBanlanceFee,//产值
                                 Status = item.ProjBanlanceStatus == state ? "已结清" : "未结清",
                                 DistributeFee = op.GetDistributeFee(item.Id),//分配产值
                                 UnDistrFee = item.ProjBanlanceFee - op.GetDistributeFee(item.Id),
                                 DistributeMoney=userCount.GetTechMoney(item.Id)
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = targetLsit
            });
        }
        #endregion

        #region  将项目进入预结算状态
        [Description("将项目进入结算状态")]
        public string SetBalanceState(FormCollection form)
        {
            string RowID = form["RowID"];
            string[] id = RowID.Split(',');
            List<int> list = new List<int>();
            foreach (string Id in id)
            {
                list.Add(int.Parse(Id));
            }
            //判断当前项目是否在预结算列表中  返回：-2,跳过不处理

            // 处于结清状态  返回-1，跳过不处理
            foreach(int projID in list)
            {
                if (op.GetList(p => p.EngineeringID == projID && p.BalanceState == (int)DataModel.BalanceStatus.预结算).Count() > 0)
                    return "-2";//只要选中的有一个处于预结算状态的就不处理
                var proj = project.Get(projID);
                if(proj!=null)
                {
                    if(proj.ProjBanlanceStatus==(int)DataModel.ProjBalanceState.已结清)
                    {
                        return "-1";
                    }
                }
            }
            
            foreach (int projID in list)
            {
                var payBalanceEngineering = new PayBalanceEngineering();
                var proj = project.Get(projID);
                if(proj != null)
                {
                    proj.MvcDefaultSave(userInfo.EmpID);
                    payBalanceEngineering.BalanceState = (int)DataModel.BalanceStatus.预结算;
                    payBalanceEngineering.BalanceLotID = 0;//暂时赋值,在确认结算时会更新
                    payBalanceEngineering.EngineeringID = proj.Id;
                    payBalanceEngineering.PayModelID = 0;
                    payBalanceEngineering.ModelXML = "";
                    op.Add(payBalanceEngineering);
                }
            }
            op.UnitOfWork.SaveChanges();
            return "1";
        }
        #endregion

        #region 将工程绩效状态设置为结清
        [Description("将工程绩效状态设置为结清")]
        public ActionResult edit(FormCollection form)
        {
            string ids = form["Ids"];
            string[] id = ids.Split(',');
            List<int> list = new List<int>();
            foreach(string Id in id)
            {
                list.Add(int.Parse(Id));
            }
            int result = 0;
            project.Edit(p=>list.Contains(p.Id),s=>new Project() { ProjBanlanceStatus=(int)DataModel.ProjBalanceState.已结清});//更新为已结清
            project.UnitOfWork.SaveChanges();
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 将工程绩效状态设置为未结清
        [Description("将工程绩效状态设置为结清")]
        public ActionResult SetUnBalance(FormCollection Form)
        {
            string rowID = Form["RowID"];
            string[] id = rowID.Split(',');
            List<int> list = new List<int>();
            foreach (string Id in id)
            {
                list.Add(int.Parse(Id));
            }
            int result = 0;
            project.Edit(p => list.Contains(p.Id), s => new Project() { ProjBanlanceStatus = (int)DataModel.ProjBalanceState.未结清 });//更新为已结清
            project.UnitOfWork.SaveChanges();
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }
        #endregion

        #region   设置产值
        [Description("设置产值")]
        public ActionResult SetProduct(int id)
        {
            DataModel.Models.Project proj = new Project();
            proj = project.Get(id);
            return View("info",proj);
        }
        #endregion

        #region   保存信息
        [Description("保存信息")]
        [HttpPost]
        public ActionResult Save(int Id)
        {
            DataModel.Models.Project proj = new Project();
            int result = 0;
            proj = project.Get(Id);
            proj.MvcSetValue();
            project.Edit(proj);
            project.UnitOfWork.SaveChanges();
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }
        #endregion

        #region  撤销结算信息
        [Description("撤销结算信息")]
        public ActionResult CancelBalance(FormCollection Form)
        {
            string rowID = Form["RowID"];
            string[] id = rowID.Split(',');
            List<int> list = new List<int>();
            foreach (string Id in id)
            {
                list.Add(int.Parse(Id));
            }
            int result = 0;
            op.CancelBalance(list);//撤销结算信息
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);

        }
        #endregion

        #region 技术人员明细
        [Description("技术人员明细")]
        public ActionResult TechInfo(int id)
        {
            var techInfo = new DataModel.Models.PayBalanceEngineering();
            var model = new DataModel.Models.Project();
            var percentBalance = new BalancePercent();
            techInfo = op.Get(id);
            model = project.Get(techInfo.EngineeringID);
            if(model!=null)
            {
                ViewBag.ProjEmpName = model.ProjEmpName;
                ViewBag.ProjName = model.ProjName;
                ViewBag.PhaseId = model.ProjPhaseIds;
                ViewBag.Volt = model.ProjVoltID;
            }
            if(!string.IsNullOrEmpty(techInfo.ModelXML))
            {
                percentBalance.XmlToModel(techInfo.ModelXML);//将xml转化为model

            }
            BindViewData(percentBalance);//绑定值
            return View("TechInfo",techInfo);
        }
        #endregion

        #region 保存技术人员明细数据
        [Description("保存技术人员明细数据")]
        public ActionResult TechInfoSave(int Id)
        {
            var PayEngiInfo = op.Get(Id);
            var percentBalance = new BalancePercent();
            var payBalanceAcount = new PayBalanceUserAccount();
            try
            {
                PayEngiInfo.MvcSetValue();
                percentBalance.MvcSetValue();
                string xml = percentBalance.ModelToXml();
                PayEngiInfo.MvcDefaultEdit(userInfo.EmpID);
                PayEngiInfo.ModelXML = xml;
                payBalanceAcount = userCount.FirstOrDefault(p => p.BalanceEngineeringID == Id && p.SpecID == -1 && p.BalanceReason == "设总");
                if (payBalanceAcount != null)
                {
                    payBalanceAcount.BalancePercent = percentBalance.ProjEmpPercent;
                    payBalanceAcount.BalanceAmount = percentBalance.ProjEmpTotal;
                    payBalanceAcount.MvcDefaultEdit(userInfo.EmpID);
                    userCount.Edit(payBalanceAcount);
                }
                else
                {
                    var modelAcount = new PayBalanceUserAccount();
                    modelAcount.BalancePercent = percentBalance.ProjEmpPercent;
                    modelAcount.BalanceAmount = percentBalance.ProjEmpTotal;
                    modelAcount.BalanceType = 1;
                    modelAcount.BalanceEngineeringID = Id;
                    modelAcount.SpecID = -1;
                    modelAcount.BalanceReason = "设总";
                    modelAcount.EmpId = project.Get(PayEngiInfo.EngineeringID) == null ? 0 : project.Get(PayEngiInfo.EngineeringID).ProjEmpId;
                    modelAcount.MvcDefaultSave(userInfo.EmpID);
                    userCount.Add(modelAcount);
                }
                string JsonRows = Request.Form["JsonRows"];
                List<TechBalanceInfo> EvalList = new List<TechBalanceInfo>();
                List<PayBalanceUserAccount> targetList = new List<PayBalanceUserAccount>();
                if (!string.IsNullOrEmpty(JsonRows))
                {
                    EvalList = new JavaScriptSerializer().Deserialize<List<TechBalanceInfo>>(JsonRows);
                    foreach (TechBalanceInfo tech in EvalList)
                    {
                        PayBalanceUserAccount payBalance = new PayBalanceUserAccount();
                        payBalance.Id = tech.Id;
                        payBalance.SpecID = tech.SpecID;
                        payBalance.BalanceEngineeringID = PayEngiInfo.Id;
                        payBalance.BalanceType = 1;//技术人员
                        payBalance.BalancePercent = tech.BalancePercent;
                        payBalance.BalanceAmount = tech.BalanceAmount;
                        payBalance.BalanceReason = tech.BalanceReason;
                        payBalance.EmpId = tech.EmpId;
                        payBalance.BalanceNote = tech.BalanceNote;
                        targetList.Add(payBalance);
                    }
                    List<int> detailId = targetList.Where(p => p.BalanceEngineeringID == PayEngiInfo.Id && p.Id != 0).Select(p => p.Id).ToList();
                    userCount.Delete(p => !detailId.Contains(p.Id) && p.BalanceEngineeringID == PayEngiInfo.Id && p.SpecID != -1);//删除多余的

                    foreach (PayBalanceUserAccount payBanUserAcc in targetList)
                    {
                        if (payBanUserAcc.Id > 0)
                        {
                            payBanUserAcc.MvcDefaultEdit(userInfo.EmpID);
                            userCount.Edit(payBanUserAcc);
                        }
                        else
                        {
                            payBanUserAcc.MvcDefaultSave(userInfo.EmpID);
                            userCount.Add(payBanUserAcc);
                        }
                    }
                    userCount.UnitOfWork.SaveChanges();
                }
                op.Edit(PayEngiInfo);
                op.UnitOfWork.SaveChanges();

                doResult = DoResultHelper.doSuccess("1","修改成功");
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);

        }
        #endregion

        #region   绑定ViewData的值
        /// <summary>
        /// 绑定ViewData的值
        /// </summary>
        public void BindViewData(BalancePercent balancePercent)
        {
            Type t = balancePercent.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach(PropertyInfo p in properties)
            {
                ViewData[p.Name] = p.GetValue(balancePercent,null);
            }
        }
        #endregion

        #region  根据人员查找所在的专业
        public JsonResult GetSpecNameByEmp()
        {
            int EmpId = TypeHelper.parseInt(Request.Params["EmpId"]);
            string SpecName = string.Empty;
            if(EmpId!=0)
            {
                IEnumerable<DBSql.Sys.EmpQualification> list = new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, EmpId);
                if(list.Count()>0)
                {
                    int SpecId = list.FirstOrDefault().QualificationSpecID;
                    DataModel.Models.BaseData data = new DBSql.Sys.BaseData().Get(SpecId);
                    if(data!=null)
                    {
                        SpecName = data.BaseName;
                    }
                }
            }
            return Json(SpecName);
        }
        #endregion

        #region 返回技术人员的导入页面
        public ActionResult selectTechEmp()
        {
            ViewBag.projId = Request.Params["projId"];
            ViewBag.PhaseId = Request.Params["PhaseId"];
            return View();
        }
        #endregion

        #region 返回技术人员导入页面的Json

        [HttpPost]
        public string selectTechEmpJson()
        {
            int projId = 0;
            List<int> PhaseId = new List<int>();
            if(!string.IsNullOrEmpty(Request.Params["projId"]))
            {
                projId = TypeHelper.parseInt(Request.Params["projId"]);
            }
            if(!string.IsNullOrEmpty(Request.Params["PhaseId"]))
            {
                string phaseIds = Request.Params["PhaseId"];
                string[] arrPhase = phaseIds.Split(',');
                foreach(string phaseid in arrPhase)
                {
                    int Id = TypeHelper.parseInt(phaseid);
                    PhaseId.Add(Id);
                }
            }
            DataTable data = op.GetEmpListByProjIdOrPhase(projId, PhaseId);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in data.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn dataColumn in data.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }
            return JavaScriptSerializerUtil.getJson(new
            {
                total = data.Rows.Count,
                rows = arrayList
            });
        }
        #endregion
    }

    /// <summary>
    /// 记录各个百分比的Xml信息(   *****用临时类转化为Model建议仅限于10个字段以内（否则不方便维护），超过采用Foreach进行遍历Form.Keys******)
    /// </summary>
    public class BalancePercent
    {
        /// <summary>
        /// 公司提成比例
        /// </summary>
        public decimal CompanyPercent { get; set; }
        /// <summary>
        /// 工程类别比例
        /// </summary>
        public decimal EngiTypePercent { get; set; }
        /// <summary>
        /// 工程难度系数
        /// </summary>
        public decimal EngiHardPercent { get; set; }
        /// <summary>
        /// 设总比例
        /// </summary>
        public decimal ProjEmpPercent { get; set; }
        /// <summary>
        /// 设总所得产值
        /// </summary>
        public decimal ProjEmpTotal { get; set; }
        /// <summary>
        /// 剩余产值
        /// </summary>
        public decimal ElseProduct { get; set; }

        /// <summary>
        /// 设计比例
        /// </summary>
        public decimal DesPercent { get; set; }
        /// <summary>
        /// 校对比例
        /// </summary>
        public decimal Checkpercent { get; set; }
        /// <summary>
        /// 审核比例
        /// </summary>
        public decimal ReviewPercent { get; set; }
        public BalancePercent()
        {
            CompanyPercent = 0.00M;
            EngiTypePercent = 0.00M;
            ProjEmpPercent = 0.00M;
            EngiHardPercent = 0.00M;
            ProjEmpTotal = 0.00M;
            ElseProduct = 0.00M;
            DesPercent = 0.00M;
            Checkpercent = 0.00M;
            ReviewPercent = 0.00M;
        }
    }

    /// <summary>
    /// 技术人员产值分配
    /// </summary>
    public  class TechBalanceInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 技术人员ID
        /// </summary>
        public int EmpId { get; set; }
        /// <summary>
        /// 专业ID
        /// </summary>
        public int SpecID { get; set; }
        /// <summary>
        /// 技术人员姓名 
        /// </summary>
        public string TechEmpName { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string BalanceReason { get; set; }
        /// <summary>
        /// 比例
        /// </summary>
        public decimal BalancePercent { get; set; }
        /// <summary>
        /// 产值
        /// </summary>
        public decimal BalanceAmount { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string BalanceNote { get; set; }

        public TechBalanceInfo()
        {
            Id = 0;
            EmpId = 0;
            SpecID = 0;
            TechEmpName = "";
            BalanceReason = "";
            BalancePercent = 0;
            BalanceAmount = 0;
            BalanceNote = "";
        }
    }
}
