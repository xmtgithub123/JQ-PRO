using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataModel.Models;
using DBSql.PubFlow;
using JQ.Web;
using System.Xml;
using JQ.Util;
using System.Web.SessionState;

namespace Hr.FlowProcessor
{
    public class TripBack : IFlowProcessor
    {
        public DbContext CurrentDbContext
        {
            get;
            set;
        }

        public FlowWareOptions Options
        {
            get;
            set;
        }

        public HttpRequest Request
        {
            get;
            set;
        }

        public Action<object> SetCreateProperties
        {
            get;
            set;
        }

        public Action<object> SetModifyProperties
        {
            get;
            set;
        }

        public HttpSessionState Session
        {
            get;
            set;
        }

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                model = this.CurrentDbContext.Set<DataModel.Models.Trip>().FirstOrDefault(m => m.Id == this.Options.RefID);

                model.IsBX = 1;

                SetModifyProperties(model);

            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.Trip model = new DataModel.Models.Trip();
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
                        model = this.CurrentDbContext.Set<DataModel.Models.Trip>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.Trip();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();

                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.Trip>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;

                    }
                    Options.Title = "出差报销单:" + model.EmpName;
                    break;
            }
        }
    }
}
