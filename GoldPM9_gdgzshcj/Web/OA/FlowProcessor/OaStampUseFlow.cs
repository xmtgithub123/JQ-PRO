using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using DBSql.PubFlow;
using System.Web;
using JQ.Web;
using JQ.Util;
using System.Web.SessionState;

namespace OA.FlowProcessor
{
    public class OaStampUseFlow : IFlowProcessor
    {

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

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }

            this.Options.Title = "[" + model.FK_OaStampUse_StampID == null ? "" : model.FK_OaStampUse_StampID.StampName + "]"  +"-审批";

            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.OaStampUse model = null;
        //private DBSql.Project.Project proj = new DBSql.Project.Project();

        public void OnExecuting()
        {

        }

        public void OnSaveForm()
        {
            switch (this.Options.Action)
            {
                case DBSql.PubFlow.Action.Save:
                case DBSql.PubFlow.Action.Next:
                case DBSql.PubFlow.Action.Back:
                case DBSql.PubFlow.Action.Finish:
                case DBSql.PubFlow.Action.Reject:
                case DBSql.PubFlow.Action.Transfer:
                    if (this.Options.RefID > 0)
                    {
                        model = this.CurrentDbContext.Set<DataModel.Models.OaStampUse>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new OaStampUse();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    model.ProjId = int.Parse(Request.Form["ProjI"]);
                    model.ProjName = Request.Form["ProjIName"];

                    if (model.Id == 0)
                    {
                        if (model.IsWJ == 0)
                        {
                            model.StampReturnSrate = 1;
                        }
                        else
                        {
                            model.StampReturnSrate = 0;
                        }
                        model.MvcDefaultSave(Options.CurrentUser.EmpID);
                        this.CurrentDbContext.Set<DataModel.Models.OaStampUse>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }

                    this.Options.Title =  "用章审批";
                    break;
            }
        }
    }
}
