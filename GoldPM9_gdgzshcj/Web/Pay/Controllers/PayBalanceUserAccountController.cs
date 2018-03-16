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
using System.Web.Script;

namespace Pay.Controllers
{
   
  
    [Description("PayBalanceUserAccount")]
    public class PayBalanceUserAccountController : CoreController
    { 
        private DBSql.Pay.PayBalanceUserAccount op = new DBSql.Pay.PayBalanceUserAccount();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Pay.PayBalanceEngineering engi = new DBSql.Pay.PayBalanceEngineering();
        private DBSql.Pay.PayBalanceLot lot = new DBSql.Pay.PayBalanceLot();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        private DBSql.Pay.PayBalanceChangeHist change = new DBSql.Pay.PayBalanceChangeHist();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            BindViewBags();
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("PreBalanceManage")));
            return View();
        }
        #endregion

        #region  预结算人员汇总信息
        public ActionResult BalancePreview()
        {
            ViewBag.TotalProduct=op.GetTotalAmount();
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("PreBalancePreView")));
            return View();
        }
        #endregion

        #region   技术人员历史统计页面
        [Description("技术人员历史统计页面")]
        public ActionResult HistoryTechStatistics()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OldBalanceTech")));
            return View();
        }
        #endregion

        
        #region   技术人员历史统计详细页面
        [Description("技术人员历史统计详细页面")]
        public ActionResult HisTechDetailInfo()
        {
            int ProjID = TypeHelper.parseInt(Request.QueryString["id"]);
            BindProjDataViewBags(ProjID);
            return View();
        }
        #endregion

        #region 管理系数导入
        [Description("管理系数导入")]
        public ActionResult selectCoffee()
        {
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
            string BalanceEngineeringID = Request.QueryString["BalanceEngineeringID"];
            string BalanceType = Request.QueryString["BalanceType"];//结算类别
            var list = op.GetPagedList(PageModel).ToList();
            if (!string.IsNullOrEmpty(BalanceEngineeringID)&&BalanceEngineeringID!="0")
            {
                int balanceEngiID = Convert.ToInt32(BalanceEngineeringID);
                list = list.Where(p=>p.BalanceEngineeringID== balanceEngiID).ToList();
            }
            if(!string.IsNullOrEmpty(BalanceType))
            {
                ///   BalanceType=1表示技术人员结算  BalanceType=2表示管理人员结算
                int BalanceTypeId = Convert.ToInt32(BalanceType);
                list = list.Where(p=>p.BalanceType==BalanceTypeId).ToList();
            }
            var targetList = from item in list
                             let empMode= emp.Get(item.EmpId)
                             let SpeciModel=data.Get(item.SpecID)
                             let BalancePercent=item.BalancePercent.ToString()
                             let BalanceAmount=item.BalanceAmount.ToString()
                             where item.SpecID!=-1
                             select new
                             {
                                 item.Id,
                                 item.BalanceEngineeringID,
                                 item.EmpId,
                                 TechEmpName=empMode==null?"":empMode.EmpName,//计算技术人员
                                 item.BalanceReason,//来源
                                 BalancePercent,//计算比例
                                 item.SpecID,
                                 SpecialName=SpeciModel == null?"":SpeciModel.BaseName,//专业名称
                                 BalanceAmount,//技术人员计算产值
                                 item.BalanceNote//备注信息
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion

        #region   绑定变量
        public void BindViewBags()
        {
            ViewBag.EngiCount = engi.GetWaitingBalanceProjectCount();//获取待 结算的项目数量
            ViewBag.SumAcount = op.GetTechSumAcount();//技术人员的总产值
            ViewBag.SumEmpCount = op.GetTotalTechPerson();//设定技术的总人数
        }
        #endregion

        #region   管理人员绩效
        [Description("管理人员绩效")]
        [HttpPost]
        public string ManagerJson()
        {
            //Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //if (!PageModel.SelectOrder.isNotEmpty())
            //{
            //    PageModel.SelectOrder = "ManageCoeff desc";
            //}
            //var list = op.GetPagedList(p=>p.BalanceLotID==0&&p.BalanceType==2,PageModel).ToList();
            var list = op.GetList(p => p.BalanceLotID == 0 && p.BalanceType == 2).ToList();
            var targetList = from item in list
                             let empMode = emp.Get(item.EmpId)
                             let SpeciModel = data.Get(item.SpecID)
                             select new
                             {
                                 item.Id,
                                 item.EmpId,
                                 EmpName = empMode == null ? "" : empMode.EmpName,//计算技术人员
                                 item.SpecID,
                                 item.ManageCoeff,
                                 item.BalanceAmount,//技术人员计算产值
                                 item.BalanceNote//备注信息
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = targetList
            });
        }
        #endregion

        #region   技术人员历史统计详细信息
        [Description("技术人员历史统计详细信息")]
        public string TechStatisticsDetailJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "BalanceLotID desc";
            }
            int EngiID = TypeHelper.parseInt(Request.QueryString["EngiID"]);
            //var list = op.GetPagedList(P=>P.BalanceType==1,PageModel).ToList();//技术人员
            var list = op.GetList(P => P.BalanceType == 1).ToList();//技术人员
            var targetList = from item in list
                             let empMode = emp.Get(item.EmpId)
                             let lotModel=lot.Get(item.BalanceLotID)
                             let engiModel=engi.Get(item.BalanceEngineeringID)
                             orderby item.BalanceLotID descending, item.BalanceAmount descending
                             where engi.GetList(p=>p.Id==item.BalanceEngineeringID&&p.EngineeringID==EngiID&&p.BalanceState==(int)DataModel.BalanceStatus.已结算).Count()>0//已结算信息
                            // where item.BalanceLotID>0&&item.
                             select new
                             {
                                 item.Id,
                                 item.BalanceEngineeringID,
                                 item.BalanceLotID,
                                 LotName=lotModel==null?"":lotModel.BalanceLotName,//批次名称
                                 BalanceDate = lotModel==null?new DateTime(1900,1,1):lotModel.BalanceDate,//批次日期
                                 EngAmountArrange=engiModel == null?0:engiModel.EngAmountArrange,//分配产值
                                 item.EmpId,
                                 TechEmpName = empMode == null ? "" : empMode.EmpName,//计算技术人员
                                 item.BalanceReason,//来源
                                 item.BalanceAmount,//技术人员计算产值
                                 item.BalanceMoney,//金额
                                 item.BalanceNote,//备注信息
                                 ChangeCount=change.GetList(p=>p.BalanceEmpAccountID==item.Id).Count()
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion

        #region  更新人数
        [HttpPost]
        public ActionResult UpdateCount(FormCollection form)
        {
            int result = 0;
            int PersonCount = TypeHelper.parseInt(form["PersonCount"]);
            DataModel.Models.PayBalanceLot model = lot.Get(1);
            model.TechEmpCount = PersonCount;
            lot.Edit(model);
            lot.UnitOfWork.SaveChanges();
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);

        }
        #endregion

        #region  批量保存数据
        [HttpPost]
        public ActionResult Save(FormCollection form)
        {
            int result = 0;
            string JsonRowsVal = form["JsonRowsVal"];
            List<ManagerBalance> manager = new List<ManagerBalance>();
            List<DataModel.Models.PayBalanceUserAccount> payUserCount = new List<PayBalanceUserAccount>();
            try
            {

                if (!string.IsNullOrEmpty(JsonRowsVal))
                {
                    manager = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<ManagerBalance>>(JsonRowsVal);
                    foreach (ManagerBalance managerBalance in manager)
                    {
                        DataModel.Models.PayBalanceUserAccount model = new PayBalanceUserAccount();
                        model.Id = managerBalance.Id;
                        model.BalanceLotID = 0;
                        model.BalanceEngineeringID = 0;
                        model.EmpId = managerBalance.EmpId;
                        model.BalanceReason = "管理人员";
                        model.BalanceType = 2;
                        model.SpecID = managerBalance.SpecialId;
                        model.ManageCoeff = managerBalance.ManageCoeff;
                        model.BalanceNote = managerBalance.BalanceNote;
                        ///model.BalanceMoney = managerBalance.Amount;暂时不存入，防止一直变化，提交批次后再更新此数据
                        payUserCount.Add(model);
                    }


                    op.UnitOfWork.BeginTransaction();
                    List<int> detail = payUserCount.Where(p => p.Id != 0).Select(p => p.Id).ToList();
                    op.Delete(p => !detail.Contains(p.Id) && p.BalanceLotID == 0 && p.BalanceType == 2);//删除多余信息
                    foreach (DataModel.Models.PayBalanceUserAccount pay in payUserCount)
                    {
                        if (pay.Id == 0)
                        {
                            pay.MvcDefaultSave(userInfo.EmpID);
                            op.Add(pay);
                        }
                        else
                        {
                            pay.MvcDefaultEdit(userInfo.EmpID);
                            op.Edit(pay);
                        }
                    }
                    op.UnitOfWork.CommitTransaction();
                    result++;
                }
            }
            catch
            {
                op.UnitOfWork.RollBackTransaction();
            }
            
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);

        }
        #endregion


        #region  获取人员的结算信息
        [Description("获取人员的结算信息")]
        [HttpPost]
        public string GetPersonBalanceInfo()
        {

            var list = op.GetList(p => p.BalanceLotID == 0);
            var TargetLsit = from item in list
                             let empModel=emp.Get(item.EmpId)
                             select new
                             {
                                 item.EmpId,
                                 EmpName=empModel == null?"设总":empModel.EmpName,
                                 EmpDepID=empModel == null ? 0 : empModel.EmpDepID,
                                 EmpProduct=op.GetAllAmountByPerson(item.EmpId),
                                 EmpMoney=0.00M
                             };
            if(!string.IsNullOrEmpty(Request.Params["DeptID"]))
            {
                int DeptID = TypeHelper.parseInt(Request.Params["DeptID"]);
                TargetLsit = TargetLsit.Where(p=>p.EmpDepID== DeptID);
            }
            var target = from item in TargetLsit.Distinct()
                         let model = data.Get(item.EmpDepID)
                         orderby item.EmpProduct descending
                         select new
                         {
                             item.EmpId,
                             EmpName=item.EmpName==""?"设总":item.EmpName,
                             item.EmpDepID,
                             item.EmpProduct,
                             item.EmpMoney,
                             DeptName = model == null ? "" : model.BaseName
                         };
            if(!string.IsNullOrEmpty(Request.Params["PerMoney"]))
            {
                decimal PerMoney = Convert.ToDecimal(Request.Params["PerMoney"]);
                target = from item in TargetLsit.Distinct()
                         let model = data.Get(item.EmpDepID)
                         orderby item.EmpProduct descending
                         select new
                         {
                             item.EmpId,
                             EmpName = item.EmpName == "" ? "设总" : item.EmpName,
                             item.EmpDepID,
                             item.EmpProduct,
                             EmpMoney=PerMoney * item.EmpProduct,
                             DeptName = model == null ? "" : model.BaseName
                         };
            }
            return JavaScriptSerializerUtil.getJson(new
            {
                total = target.Count(),
                rows = target
            });
        }

        #endregion

        #region   正式结算
        [Description("正式结算")]
        [HttpPost]
        public ActionResult Balance(FormCollection form)
        {
            int result = 0;
            DataModel.Models.PayBalanceLot model = new PayBalanceLot();
            decimal money = TypeHelper.parseDecimal(form["money"]);
            string Name = form["Name"];
            model.MvcDefaultSave(userInfo.EmpID);
            model.BalanceLotName = Name;
            lot.BalanceLot(money,model);
            result++;
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }
        #endregion

        public void BindProjDataViewBags(int ProjID)
        {
            DataModel.Models.Project proj = project.Get(ProjID);
            if(proj!=null)
            {
                ViewBag.ProjName = proj.ProjName;//项目名称
                ViewBag.BalanceState = proj.ProjBanlanceStatus == (int)DataModel.ProjBalanceState.已结清 ? "已结清" : "未结清";
                ViewBag.Amount = proj.ProjBanlanceFee;
                ViewBag.DistributeAmount = engi.GetDistributeFee(proj.Id);
                ViewBag.UnDistrFee = proj.ProjBanlanceFee- engi.GetDistributeFee(proj.Id);
                ViewBag.DistributeMoney = op.GetTechMoney(proj.Id);
            }
        }

        #region 管理人员结算列表信息
        [HttpPost]
        public string ManagerBalanceJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ManageCoeff desc";
            }
            int LotID = TypeHelper.parseInt(Request.QueryString["LotID"]);
            var list = op.GetPagedList(p => p.BalanceLotID == LotID && p.BalanceType == 2, PageModel).ToList();
            var targetList = from item in list
                             let empMode = emp.Get(item.EmpId)
                             let SpeciModel = data.Get(item.SpecID)
                             select new
                             {
                                 item.Id,
                                 item.EmpId,
                                 EmpName = empMode == null ? "" : empMode.EmpName,//计算技术人员
                                 item.SpecID,
                                 item.ManageCoeff,
                                 item.BalanceMoney,//金额
                                 item.BalanceAmount,//技术人员计算产值
                                 item.BalanceNote,//备注信息
                                 ChangeCount = change.GetList(p => p.BalanceEmpAccountID == item.Id).Count()
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }

        #endregion




    }
    /// <summary>
    /// 管理人员信息结算
    /// </summary>
    public class ManagerBalance
    {
        public int Id { get; set; }
        public int EmpId { get;set; }
        public int SpecialId { get; set; }
        public decimal ManageCoeff { get; set; }
        public decimal Amount { get; set; }
        public string EmpName { get; set; }
        public string BalanceNote { get; set; }

        public ManagerBalance()
        {
            Id = 0;
            EmpId = 0;
            SpecialId = 0;
            ManageCoeff = 0.00M;
            Amount = 0.00M;
            EmpName = "";
            BalanceNote = "";
        }
    }
}
