using DBSql.PubFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;
using JQ.Web;
using JQ.Util;

namespace OA.FlowProcessor
{
    class OaFileReceiveFlow : IFlowProcessor
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

        public HttpSessionState Session
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

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            this.Options.Message = "发文部门[" + model.OaFileSend + "],文件名称:" + model.OaFileName;
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
        private DataModel.Models.OaFileReceive model = null;
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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaFileReceive>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new OaFileReceive();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    var _HandEmps = Request.Form["HandEmps"];
                    var _departments = Request.Form["department"];
                    model.OaFileGetEmp = TypeHelper.parseString(_HandEmps);
                    model.OaFileGetEmpDept = TypeHelper.parseString(_departments);
                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.OaFileReceive>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }
                    this.Options.Title = "发文部门[" + model.OaFileSend + "],文件名称:" + model.OaFileName;
                    break;
            }
        }
    }
}
