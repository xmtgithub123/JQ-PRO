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
    public class SafeProcessor : IFlowProcessor
    {
        #region 私有变量

        private SafeManage model = new SafeManage();
        private SafeManageData safedata = new SafeManageData();

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

            SafeManageData safeBus = new SafeManageData();
            this.Options.Message = string.Format("【{0}】 安全策划单", DTHelper.GetEntity<DesTaskGroup>(safeBus.GetProj(model.EngineeringID)).ProjName);

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
            SafeManageData safeBus = new SafeManageData();

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
                            model = DTHelper.GetEntity<SafeManage>(safeBus.Get(this.Options.RefID));
                            if (model == null)
                            {
                                throw new Common.JQException("无法找到有效的实例");
                            }
                        }
                        else
                        {
                            model = new DataModel.Models.SafeManage();
                        }
                        model.MvcSetValue();

                        if (model.Id > 0)
                        {
                            safeBus.Update(model);
                        }
                        else
                        {
                            model.CreatorEmpId = this.Options.CurrentUser.EmpID;
                            model.CreatorEmpName = this.Options.CurrentUser.EmpName;
                            model.CreatorTime = DateTime.Now;
                            model.EmpManageId = TypeHelper.parseInt(Request["empid"]);
                            var op = new DBSql.Engineering.SafeManage().FirstOrDefault(p => p.EmpManageId == model.EmpManageId);
                            if (op == null)
                                model.Id = safeBus.Insert(model);
                            else
                            {
                                model.Id = op.Id;
                                safeBus.Update(model);
                            }

                            //var safeOp = new DBSql.Engineering.SafeManage();
                            //safeOp.Delete(p => p.EmpManageId == model.EmpManageId);
                            //model.Id = safeBus.Insert(model);
                        }

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                        this.Options.Title = string.Format("【{0}】 安全策划单", new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjName); 
                        //DTHelper.GetEntity<DesTaskGroup>(safeBus.GetProj(model.EngineeringID)).ProjName);
                    }
                    break;
            }
        }

        #endregion
    }
}
