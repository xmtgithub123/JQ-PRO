using Common;
using DataModel.Models;
using DBSql;
using DBSql.PubFlow;
using JQ.Util;
using JQ.Web;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;

namespace Engineering.FlowProcessor
{
    public class CGProcessor : IFlowProcessor
    {
        #region 私有变量

        private CGManage model = new CGManage();
        private CGManageData cgdata = new CGManageData();

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

            CGManageData cgBus = new CGManageData();
            this.Options.Message = string.Format("【{0}】 采购策划单", DTHelper.GetEntity<DesTaskGroup>(cgBus.GetProj(model.EngineeringID)).ProjName);

            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
            }
        }

        public void OnSaveForm()
        {
            CGManageData cgBus = new CGManageData();

            switch (this.Options.Action)
            {
                case DBSql.PubFlow.Action.Save:
                case DBSql.PubFlow.Action.Next:
                case DBSql.PubFlow.Action.Back:
                case DBSql.PubFlow.Action.Finish:
                case DBSql.PubFlow.Action.Reject:
                case DBSql.PubFlow.Action.Transfer:
                    {
                        if (this.Options.RefID > 0)
                        {
                            model = DTHelper.GetEntity<CGManage>(cgBus.Get(this.Options.RefID));
                            if (model == null)
                            {
                                throw new Common.JQException("无法找到有效的实例");
                            }
                        }
                        else
                        {
                            model = new DataModel.Models.CGManage();
                        }
                        model.MvcSetValue();

                        if (model.Id > 0)
                        {
                            cgBus.Update(model);
                        }
                        else
                        {
                            model.CreatorEmpId = this.Options.CurrentUser.EmpID;
                            model.CreatorEmpName = this.Options.CurrentUser.EmpName;
                            model.CreatorTime = DateTime.Now;
                            model.EmpManageId = TypeHelper.parseInt(Request["empid"]);
                            var cgOp = new DBSql.Engineering.CGManage().FirstOrDefault(p=>p.EmpManageId == model.EmpManageId);
                            if (cgOp == null)
                                model.Id = cgBus.Insert(model);
                            else
                            {
                                model.Id = cgOp.Id;
                                cgBus.Update(model);
                            }
                            //cgOp.Delete(p => p.EmpManageId == model.EmpManageId);
                            //model.Id = cgBus.Insert(model);
                        }

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                        this.Options.Title = string.Format("【{0}】 采购策划单", new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjName);
                            //DTHelper.GetEntity<DesTaskGroup>(cgBus.GetProj(model.EngineeringID)).ProjName);
                    }
                    break;
            }
        }

        #endregion
    }
}
