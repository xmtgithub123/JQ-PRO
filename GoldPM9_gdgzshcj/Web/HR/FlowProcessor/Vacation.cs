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
    public class Vacation : IFlowProcessor
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
                var op = new DBSql.HR.HREmployee();

                this.Options.Message += "流程审批通过！";
                HREmployee empModel = new DBSql.HR.HREmployee().GetHREmployee(model.EmpID);
                string VacationTypeName = new DBSql.Sys.BaseData().getBaseNameByIds(model.VacationType).Replace(",","");
                if (VacationTypeName == "年休假")
                {
                    empModel.VacationDays1 -= model.Days;
                }

                op.Edit(empModel);
                op.UnitOfWork.SaveChanges();


            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.Vacation model = new DataModel.Models.Vacation();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
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
                        model = this.CurrentDbContext.Set<DataModel.Models.Vacation>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.Vacation();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();
                    model.EmpName = Request.Form["EmpName"];

                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.Vacation>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;

                    }
                    this.CurrentDbContext.SaveChanges();

                    Options.Title = "请假单:" + model.EmpName;
                    break;
            }
        }
    }
}
