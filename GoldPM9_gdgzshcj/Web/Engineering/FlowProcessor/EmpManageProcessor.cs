using Common;
using DataModel.Models;
using DBSql;
using DBSql.PubFlow;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;

namespace Engineering.FlowProcessor
{
    public class EmpManageProcessor : IFlowProcessor
    {
        #region 私有变量

        private EmpManage model = new EmpManage();
        private EmpManageData cgdata = new EmpManageData();

        #endregion

        #region 公共变量

        public DbContext CurrentDbContext
        {
            get; set;
        }

        public FlowWareOptions Options
        {
            get; set;
        }

        public HttpRequest Request
        {
            get; set;
        }

        public Action<object> SetCreateProperties
        {
            get; set;
        }

        public Action<object> SetModifyProperties
        {
            get; set;
        }

        public HttpSessionState Session
        {
            get; set;
        }

        #endregion

        #region 公共方法

        public void OnExecuting()
        {
        }

        public void OnExecuted(bool isSuccess)
        {
        }

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            
            this.Options.Message = string.Format("【{0}】 人员策划单", model.ProjName);

            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
            }
        }
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
                oaMessModel.MessLinkUrl = string.Format("engineering/ProjManage/add?EngID={0}&empId={1}", model.EngineeringId, model.Id);
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
                oaMessModel.MessLinkUrl = string.Format("engineering/SafeManage/add?EngID={0}&empId={1}", model.EngineeringId, model.Id);
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
                oaMessModel.MessLinkUrl = string.Format("engineering/CGManage/add?EngID={0}&empId={1}", model.EngineeringId, model.Id);
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
                oaMessModel.MessLinkUrl = string.Format("engineering/SSManage/add?EngID={0}&empId={1}", model.EngineeringId, model.Id);
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
                oaMessModel.MessLinkUrl = string.Format("engineering/WDManage/info?EngID={0}&ProjID={1}&empId={2}&taskGroupId={3}", model.EngineeringId, project.ProjId, model.Id, model.ProjPhase);
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
        public void OnSaveForm()
        {
            EmpManageData cgBus = new EmpManageData();

            switch (this.Options.Action)
            {
                case DBSql.PubFlow.Action.Save:
                case DBSql.PubFlow.Action.Next:
                case DBSql.PubFlow.Action.Back:
                case DBSql.PubFlow.Action.Finish:
                case DBSql.PubFlow.Action.Reject:
                case DBSql.PubFlow.Action.Transfer:
                    {
                        EmpManageData empBusiness = new EmpManageData();
                        BaseEmployeeData eBus = new BaseEmployeeData();
                        EmpManage model = new EmpManage();
                        EmpManage oldModel = new EmpManage();
                        if (this.Options.RefID > 0)
                        {
                            model = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == this.Options.RefID);   //DTHelper.GetEntity<EmpManage>(cgBus.Get(this.Options.RefID));
                            if (model == null)
                            {
                                throw new Common.JQException("无法找到有效的实例");
                            }
                        }
                        else
                        {
                            model = new DataModel.Models.EmpManage();
                        }
                        model.MvcSetValue();

                        //if (model.Id > 0)
                        //{
                        //    cgBus.Update(model);
                        //}
                        //else
                        //{
                        //    model.CreatorEmpId = this.Options.CurrentUser.EmpID;
                        //    model.CreatorEmpName = this.Options.CurrentUser.EmpName;
                        //    model.CreationTime = DateTime.Now;
                        //    model.Id = cgBus.Insert(model);

                        //    //var cgOp = new DBSql.Engineering.EmpManage().FirstOrDefault(p=>p.EmpManageId == model.EmpManageId);
                        //    //if (cgOp == null)
                        //    //    model.Id = cgBus.Insert(model);
                        //    //else
                        //    //{
                        //    //    model.Id = cgOp.Id;
                        //    //    cgBus.Update(model);
                        //    //}
                        //    //cgOp.Delete(p => p.EmpManageId == model.EmpManageId);
                        //    //model.Id = cgBus.Insert(model);
                        //}

                        if (model.Id > 0)
                        {
                            model = DTHelper.GetEntity<EmpManage>(empBusiness.Get(model.Id));
                            oldModel = DTHelper.GetEntity<EmpManage>(empBusiness.Get(model.Id));
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
                        if (model.Id > 0)
                        {
                            model.MvcDefaultEdit(this.Options.CurrentUser);
                            reuslt = empBusiness.Update(model);
                        }
                        else
                        {
                            model.MvcDefaultSave(this.Options.CurrentUser);
                            reuslt = model.Id = empBusiness.Insert(model);
                        }

                        int EngineeringID = TypeHelper.parseInt(Request["EngineeringID"]);
                        if (model.SafeEmpId != 0)
                        {
                            var safeOp = new DBSql.Engineering.SafeManage();
                            var safeModel = safeOp.FirstOrDefault(p => p.EmpManageId == model.Id);//.FirstOrDefault();
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
                            var cgModel = cgOp.FirstOrDefault(p => p.EmpManageId == model.Id);//.FirstOrDefault();
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
                            var ssModel = ssOp.FirstOrDefault(p => p.EmpManageId == model.Id);//.FirstOrDefault();
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
                            var projModel = projOp.FirstOrDefault(p => p.EmpManageId == model.Id);//.FirstOrDefault();
                            string projEmpName = projModel == null ? "" : projModel.CreatorEmpName;  //以前的负责人
                            if (Request["type"] != "1" && model.EngineeringEmpName != projEmpName) //如果负责人没有改变  type!=1 代表是修改数据 type=1是添加
                            {
                                if (projModel != null)
                                {
                                    projOp.Delete(p => p.EmpManageId == projModel.EmpManageId); //如果修改了实施负责人 则删除该实施数据  并新增
                                }
                                new ProjManageData().Insert(new ProjManage() { EngineeringID = EngineeringID, CreatorEmpId = model.EngineeringEmpId, CreatorEmpName = model.EngineeringEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                            }
                            else if (Request["type"] == "1")
                                new ProjManageData().Insert(new ProjManage() { EngineeringID = EngineeringID, CreatorEmpId = model.EngineeringEmpId, CreatorEmpName = model.EngineeringEmpName, EmpManageId = model.Id, CreatorTime = DateTime.Now });
                        }
                        if (model.Id <= 0)
                        {
                            using (var ba1 = new DBSql.Sys.BaseAttach())
                            {
                                ba1.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                            }
                        }

                        if (reuslt > 0)
                        {
                            SendMessage(oldModel, model);
                        }

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                        this.Options.Title = string.Format("【{0}】 人员策划单", model.ProjName);
                    }
                    break;
            }
        }
        
        #endregion
    }
}
