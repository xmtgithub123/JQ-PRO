using Common;
using DataModel.Models;
using DBSql;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Engineering.Controllers
{
    [Description("EmpManage")]
    public class EmpManageController : CoreController
    {
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            EmpManageData empBusiness = new EmpManageData();
            DataTable list = empBusiness.GetList(PageModel.PageSize, PageModel.PageIndex, PageModel.PredicateValue != null ? PageModel.PredicateValue[0].ToString() : "", "Id DESC", out recordcount,userInfo.EmpID);

            var op = new DBSql.PubFlow.Flow();

            var showList = (from p in list.AsEnumerable()
                            join t1 in op.GetList(f => f.FlowRefTable == "EmpManage") on p.Field<int>("Id") equals t1.FlowRefID into t11
                            from temp in t11.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Field<int>("Id"),
                                CreatorEmpName = p.Field<string>("CreatorEmpName"),
                                CreatorEmpId = p.Field<int>("CreatorEmpId"),
                                ProjName = p.Field<string>("ProjName"),
                                ProjPhase = p.Field<string>("ProjPhase"),
                                EngineeringEmpName = p.Field<string>("EngineeringEmpName"),
                                SafeEmpName = p.Field<string>("SafeEmpName"),
                                CGEmpName = p.Field<string>("CGEmpName"),
                                JSEmpName = p.Field<string>("JSEmpName"),
                                WDEmpName = p.Field<string>("WDEmpName"),
                                XCEmpName = p.Field<string>("XCEmpName"),
                                EngineeringId = p.Field<int>("EngineeringId"),
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml,
                                FlowFinishControls = temp == null ? "" : temp.FlowFinishControls
                            });


            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = showList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new EmpManage();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //EmpManageData empBusiness = new EmpManageData();
            var model = new DBSql.Engineering.EmpManage().Get(id);
            //var model = DTHelper.GetEntity<EmpManage>(empBusiness.Get(id));
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            EmpManageData empBusiness = new EmpManageData();
            int reuslt = empBusiness.Delete(id, userInfo.EmpID, userInfo.EmpName, userInfo.AgenEmpID, userInfo.AgenEmpName);

            var op1 = new DBSql.Engineering.SafeManage();
            var op2 = new DBSql.Engineering.CGManage();
            var op3 = new DBSql.Engineering.SSManage();
            op1.Delete(p => id.Contains(p.EmpManageId));
            op2.Delete(p => id.Contains(p.EmpManageId));
            op3.Delete(p => id.Contains(p.EmpManageId));

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 检查是否存在
        public string IsExist(string ProjName, string ProjNumber, string ProjPhase)
        {
            //int eId = TypeHelper.parseInt(EngineeringId);
            //bool isExist = new DBSql.Engineering.EmpManage().GetList(p => p.EngineeringId == ProjId && p.DeleterEmpId != 1).isEmpty();
            bool isExist = new DBSql.Engineering.EmpManage().GetList(p => p.ProjPhase == ProjPhase && p.ProjName == ProjName && p.ProjNumber == ProjNumber && p.DeleterEmpId == 0 ).isEmpty();
            return isExist ? "1" : "0";
        }
        #endregion

        #region 检查是否正确
        public string IsExist1(string ProjName, string ProjNumber,int Id,string ProjPhase)
        {
            //var obj = new DBSql.Engineering.EmpManage().FirstOrDefault(p=>p.EngineeringId == eId && p.ProjName == ProjName && p.ProjNumber == ProjNumber&& p.DeleterEmpId == 0);
            var obj = new DBSql.Engineering.EmpManage().GetList(p=>p.ProjName == ProjName && p.ProjNumber == ProjNumber&& p.DeleterEmpId == 0 && p.ProjPhase == ProjPhase);
            if (obj.isEmpty())
                return "1";
            else
            {
                if (obj.Where(p => p.Id == Id).isNotEmpty())  //没有改过项目名称和编号可修改
                    return "1";
                else
                    return "0";
            }
            //return obj == null ? "1" : obj.Id == id ? "1" : "0";
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            EmpManageData empBusiness = new EmpManageData();
            BaseEmployeeData eBus = new BaseEmployeeData();
            EmpManage model = new EmpManage();
            EmpManage oldModel = new EmpManage();

            if (Id > 0)
            {
                model = DTHelper.GetEntity<EmpManage>(empBusiness.Get(Id));
                oldModel = DTHelper.GetEntity<EmpManage>(empBusiness.Get(Id));
            }
            model.MvcSetValue();
            model.EngineeringEmpId = TypeHelper.parseInt(Request.Form["EngineeringEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["EngineeringEmpId"].Split('-')[0]) : oldModel.EngineeringEmpId;
            model.SafeEmpId = TypeHelper.parseInt(Request.Form["SafeEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["SafeEmpId"].Split('-')[0]) : oldModel.SafeEmpId;
            model.CGEmpId = TypeHelper.parseInt(Request.Form["CGEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["CGEmpId"].Split('-')[0]) : oldModel.CGEmpId;
            model.JSEmpId = TypeHelper.parseInt(Request.Form["JSEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["JSEmpId"].Split('-')[0]) : oldModel.JSEmpId;
            model.WDEmpId = TypeHelper.parseInt(Request.Form["WDEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["WDEmpId"].Split('-')[0]) : oldModel.WDEmpId;
            model.XCEmpId = TypeHelper.parseInt(Request.Form["XCEmpId"].Split('-')[0]) > 0 ? TypeHelper.parseInt(Request.Form["XCEmpId"].Split('-')[0]) : oldModel.XCEmpId;

            int[] param = { model.EngineeringEmpId, model.SafeEmpId, model.CGEmpId, model.JSEmpId, model.WDEmpId, model.XCEmpId };
            Dictionary<int, string> empDic = eBus.Get(param);

            model.EngineeringEmpName = (empDic.ContainsKey(model.EngineeringEmpId) ? empDic[model.EngineeringEmpId] : model.EngineeringEmpName);
            model.SafeEmpName = (empDic.ContainsKey(model.SafeEmpId) ? empDic[model.SafeEmpId] : model.SafeEmpName);
            model.CGEmpName = (empDic.ContainsKey(model.CGEmpId) ? empDic[model.CGEmpId] : model.CGEmpName);
            model.JSEmpName = (empDic.ContainsKey(model.JSEmpId) ? empDic[model.JSEmpId] : model.JSEmpName);
            model.WDEmpName = (empDic.ContainsKey(model.WDEmpId) ? empDic[model.WDEmpId] : model.WDEmpName);
            model.XCEmpName = (empDic.ContainsKey(model.XCEmpId) ? empDic[model.XCEmpId] : model.XCEmpName);
            model.ProjName = Request["ProjName"];
            model.ProjNumber = Request["ProjNumber"];
            model.ProjPhase = Request["ProjPhase"];
            model.DesTaskGroupId = TypeHelper.parseInt(Request["DesTaskGroupId"]);



            int reuslt = 0;
            if (Id > 0)
            {
                model.MvcDefaultEdit(userInfo);
                reuslt = empBusiness.Update(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                reuslt = model.Id = empBusiness.Insert(model);
            }

            int EngineeringID = TypeHelper.parseInt(Request["EngineeringID"]);
            if (model.SafeEmpId != 0)
            {
                var safeOp = new DBSql.Engineering.SafeManage();
                var safeModel = safeOp.GetList(p=>p.EmpManageId==Id).FirstOrDefault();
                string safeEmpName = safeModel == null ? "" : safeModel.CreatorEmpName;
                if (Request["type"] != "1" && model.SafeEmpName != safeEmpName)
                {
                    if (safeModel != null)
                    {
                        safeOp.Delete(p => p.EmpManageId == safeModel.EmpManageId);
                    }
                    new SafeManageData().Insert(new SafeManage() { EngineeringID = EngineeringID, CreatorEmpId = model.SafeEmpId, CreatorEmpName = model.SafeEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                }
                else if (Request["type"] == "1")
                    new SafeManageData().Insert(new SafeManage() { EngineeringID = EngineeringID, CreatorEmpId = model.SafeEmpId, CreatorEmpName = model.SafeEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
            }

            if (model.CGEmpId != 0)
            {
                var cgOp = new DBSql.Engineering.CGManage();
                var cgModel = cgOp.GetList(p => p.EmpManageId == Id).FirstOrDefault();
                string cgEmpName = cgModel == null ? "" : cgModel.CreatorEmpName;
                if (Request["type"] != "1" && model.CGEmpName != cgEmpName)
                {
                    if (cgModel != null)
                    {
                        cgOp.Delete(p => p.EmpManageId == cgModel.EmpManageId);
                    }
                    new CGManageData().Insert(new CGManage() { EngineeringID = EngineeringID, CreatorEmpId = model.CGEmpId, CreatorEmpName = model.CGEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                }
                else if (Request["type"] == "1")
                    new CGManageData().Insert(new CGManage() { EngineeringID = EngineeringID, CreatorEmpId = model.CGEmpId, CreatorEmpName = model.CGEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
            }

            if (model.JSEmpId != 0)
            {
                var ssOp = new DBSql.Engineering.SSManage();
                var ssModel = ssOp.GetList(p => p.EmpManageId == Id).FirstOrDefault();
                string jsEmpName = ssModel == null ? "" : ssModel.CreatorEmpName;  //以前的负责人
                if (Request["type"] != "1" && model.JSEmpName != jsEmpName) //如果负责人没有改变  type!=1 代表是修改数据 type=1是添加
                {
                    if (ssModel != null)
                    {
                        ssOp.Delete(p => p.EmpManageId == ssModel.EmpManageId); //如果修改了实施负责人 则删除该实施数据  并新增
                    }
                    new SSManageData().Insert(new SSManage() { EngineeringID = EngineeringID, CreatorEmpId = model.JSEmpId, TechPlan = "", CreatorEmpName = model.JSEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                }
                else if (Request["type"] == "1")
                    new SSManageData().Insert(new SSManage() { EngineeringID = EngineeringID, CreatorEmpId = model.JSEmpId, TechPlan = "", CreatorEmpName = model.JSEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
            }
            if (model.EngineeringEmpId != 0)
            {
                var projOp = new DBSql.Engineering.ProjManage();
                var projModel = projOp.GetList(p => p.EmpManageId == Id).FirstOrDefault();
                string projEmpName = projModel == null ? "" : projModel.CreatorEmpName;  //以前的负责人
                if (Request["type"] != "1" && model.EngineeringEmpName != projEmpName) //如果负责人没有改变  type!=1 代表是修改数据 type=1是添加
                {
                    if (projModel != null)
                    {
                        projOp.Delete(p => p.EmpManageId == projModel.EmpManageId); //如果修改了实施负责人 则删除该实施数据  并新增
                    }
                    new ProjManageData().Insert(new ProjManage() { EngineeringID = EngineeringID, CreatorEmpId = model.EngineeringEmpId,  CreatorEmpName = model.EngineeringEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                }
                else if (Request["type"] == "1")
                    new ProjManageData().Insert(new ProjManage() { EngineeringID = EngineeringID, CreatorEmpId = model.EngineeringEmpId,  CreatorEmpName = model.EngineeringEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
            }
            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }

            if (reuslt > 0)
            {
                SendMessage(oldModel, model);
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 发送消息提醒

        private void SendMessage(EmpManage oldModel, EmpManage model)
        {
            EmpManageData empBus = new EmpManageData();
            DBSql.Oa.OaMessRead read = new DBSql.Oa.OaMessRead();
            DBSql.OA.OaMess mess = new DBSql.OA.OaMess();
            OaMess oaMessModel = new OaMess();
            DesTaskGroup project = DTHelper.GetEntity<DesTaskGroup>(empBus.GetProj(model.EngineeringId));

            bool engChange = false, safeChange = false, cgChange = false, jsChange = false, wdChange = false, xcChange = false;

            #region 如果是新增 或 项目经理人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.EngineeringEmpId != model.EngineeringEmpId)
            {
                engChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-项目目标策划提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "发起项目目标策划";
                oaMessModel.MessLinkUrl = string.Format("engineering/ProjManage/add?EngID={0}&empId={1}", model.EngineeringId,model.Id);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行项目目标策划", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "ProjManage";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 800;
                oaMessModel.MessDialogHeight = 900;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.EngineeringEmpId;
                    oaMessRead.MessReadEmpName = model.EngineeringEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion
            #region 如果是新增 或 安全员人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.SafeEmpId != model.SafeEmpId)
            {
                safeChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-安全策划提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "发起项目安全策划";
                oaMessModel.MessLinkUrl = string.Format("engineering/SafeManage/add?EngID={0}&empId={1}", model.EngineeringId,model.Id);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行项目安全策划", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "SafeManage";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 800;
                oaMessModel.MessDialogHeight = 900;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.SafeEmpId;
                    oaMessRead.MessReadEmpName = model.SafeEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion
            #region 如果是新增 或 采购员人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.CGEmpId != model.CGEmpId)
            {
                cgChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-采购策划提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "发起项目采购策划";
                oaMessModel.MessLinkUrl = string.Format("engineering/CGManage/add?EngID={0}&empId={1}", model.EngineeringId,model.Id);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行项目采购策划", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "CGManage";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 800;
                oaMessModel.MessDialogHeight = 900;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.CGEmpId;
                    oaMessRead.MessReadEmpName = model.CGEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion
            #region 如果是新增 或 技术负责人人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.JSEmpId != model.JSEmpId)
            {
                jsChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-项目实施策划提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "发起项目实施策划";
                oaMessModel.MessLinkUrl = string.Format("engineering/SSManage/add?EngID={0}&empId={1}", model.EngineeringId,model.Id);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行项目实施策划", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "SSManage";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 800;
                oaMessModel.MessDialogHeight = 900;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.JSEmpId;
                    oaMessRead.MessReadEmpName = model.JSEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion
            #region 如果是新增 或 文档管理员人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.WDEmpId != model.WDEmpId)
            {
                wdChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-项目记事和资料分类上传提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "查看任务详情";
                oaMessModel.MessLinkUrl = string.Format("engineering/WDManage/info?EngID={0}&ProjID={1}&empId={2}&taskGroupId={3}", model.EngineeringId, project.ProjId,model.Id,model.ProjPhase);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行记事和资料分类上传操作", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 450;
                oaMessModel.MessDialogHeight = 300;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.WDEmpId;
                    oaMessRead.MessReadEmpName = model.WDEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion
            #region 如果是新增 或 现场负责人人员发生变化，则发送新消息提醒
            if (0 == oldModel.Id || oldModel.EngineeringId != model.EngineeringId || oldModel.XCEmpId != model.XCEmpId)
            {
                xcChange = true;

                oaMessModel.MessDate = DateTime.Now;
                oaMessModel.MessTag = string.Empty;
                oaMessModel.MessTitle = string.Format("[{0}]{1}-项目周报填写提醒", model.ProjNumber, model.ProjName);
                oaMessModel.MessLinkTitle = "填写项目周报";
                oaMessModel.MessLinkUrl = string.Format("engineering/EmpManage/weekadd?EngID={0}&empId={1}", model.EngineeringId, model.Id);
                oaMessModel.MessNote = string.Format("{0} 提醒您对 [{1}]{2} 进行项目周报填写", (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName), project.ProjNumber, project.ProjName);
                oaMessModel.MessIsAutoReturn = false;
                oaMessModel.MessEmpId = (0 == oldModel.Id ? model.CreatorEmpId : model.LastModifierEmpId);
                oaMessModel.MessEmpName = (0 == oldModel.Id ? model.CreatorEmpName : model.LastModifierEmpName);
                oaMessModel.MessIsSystem = true;
                oaMessModel.MessIsDeleted = false;
                oaMessModel.MessRefTable = "";
                oaMessModel.MessRefID = 0;
                oaMessModel.MessDialogWidth = 800;
                oaMessModel.MessDialogHeight = 900;

                try
                {
                    read.UnitOfWork.BeginTransaction();
                    mess.Add(oaMessModel);
                    mess.DbContext.SaveChanges();

                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = model.XCEmpId;
                    oaMessRead.MessReadEmpName = model.XCEmpName;
                    //oaMessRead.MessReadEmpId = 1;
                    //oaMessRead.MessReadEmpName = "管理员";
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);

                    read.UnitOfWork.CommitTransaction();
                }
                catch
                {
                    read.UnitOfWork.RollBackTransaction();
                }
            }
            #endregion

            DBSql.Oa.OaMessRead.CacheRemove();
            List<int> EmpList = new List<int>();

            if (engChange) { EmpList.Add(model.EngineeringEmpId); }
            if (safeChange) { EmpList.Add(model.SafeEmpId); }
            if (cgChange) { EmpList.Add(model.CGEmpId); }
            if (jsChange) { EmpList.Add(model.JSEmpId); }
            if (wdChange) { EmpList.Add(model.WDEmpId); }
            if (xcChange) { EmpList.Add(model.XCEmpId); }

            var t = JQ.Util.IO.MessageMonitor.NotifyAsync(EmpList, delegate (int empID)
            {
                return new DBSql.Oa.OaMessRead().GetNotifyDatas(empID);
            });
        }

        #endregion

        #region 项目选择
        [Description("项目选择")]
        public ActionResult ChooseProjectList()
        {
            return View();
        }

        public ActionResult ChooseProjectList1()
        {
            return View();
        }
        #endregion

        #region 工程列表

        [Description("工程列表")]
        public ActionResult EngineeringList()
        {
            return View();
        }

        [Description("工程列表json")]
        [HttpPost]
        public string EngineeringJson()
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            EmpManageData empBusiness = new EmpManageData();
            DataTable list = empBusiness.GetEngineeringList(PageModel.PageSize, PageModel.PageIndex, PageModel.PredicateValue != null ? PageModel.PredicateValue[0].ToString() : "", "Id DESC", userInfo.EmpID, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        #endregion

        #region 周报

        [Description("周报列表")]
        public ActionResult weeklist()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;

            return View();
        }

        [Description("列表json")]
        [HttpPost]
        public string weekjson()
        {
            int taskgroupid = string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            EmpManageData empBusiness = new EmpManageData();
            DataTable list = empBusiness.GetWeeklyList(PageModel.PageSize, PageModel.PageIndex, PageModel.PredicateValue != null ? PageModel.PredicateValue[0].ToString() : "", "Id DESC", taskgroupid, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        [Description("添加周报")]
        public ActionResult weekadd()
        {
            var model = new DataModel.Models.Weekly();
            return View("weekinfo", model);
        }

        [Description("编辑周报")]
        public ActionResult weekedit(int id)
        {
            EmpManageData empBusiness = new EmpManageData();
            DataModel.Models.Weekly model = DTHelper.GetEntity<DataModel.Models.Weekly>(empBusiness.GetWeekly(id));
            return View("weekinfo", model);
        }

        [Description("删除周报")]
        public ActionResult weekdel(int[] id)
        {
            EmpManageData empBusiness = new EmpManageData();
            int reuslt = empBusiness.DeleteWeekly(id);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        [Description("保存")]
        [HttpPost]
        public ActionResult weeksave(int Id)
        {
            EmpManageData empBusiness = new EmpManageData();
            Weekly model = new Weekly();

            if (Id > 0)
            {
                model = DTHelper.GetEntity<Weekly>(empBusiness.GetWeekly(Id));
            }
            model.MvcSetValue();

            model.CreateEmpId = userInfo.EmpID;
            model.CreateEmpName = userInfo.EmpName;

            int reuslt = 0;
            if (Id > 0)
            {
                reuslt = empBusiness.UpdateWeekly(model);
            }
            else
            {
                reuslt = model.Id = empBusiness.InsertWeekly(model);
            }

            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion
    }
}
