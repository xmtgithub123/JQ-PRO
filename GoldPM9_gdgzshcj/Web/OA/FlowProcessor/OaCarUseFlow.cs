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
using System.Data;

namespace OA.FlowProcessor
{
    class OaCarUseFlow : IFlowProcessor
    {
        private DBSql.OA.OaCarUse op = new DBSql.OA.OaCarUse();

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

        public void OnExecuting()
        {

        }

        private DataModel.Models.OaCarUse model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaCarUse>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new OaCarUse();
                        model.UseApplyDatetime = System.DateTime.Now;
                        SetCreateProperties(model);
                    }
                    string leavePlan = Request["DateLeavePlan"].ToString() + ":00:00";
                    string arrPlan = Request["DateArrivePlan"].ToString() + ":00:00";
                    model.MvcSetValue();
                    try
                    {
                        model.DateLeavePlan = DateTime.Parse(leavePlan);
                        model.DateArrivePlan = DateTime.Parse(arrPlan);
                    }
                    catch { }
                    try
                    {
                        string leave = Request["DateLeave"].ToString() + ":00:00";
                        string arr = Request["DateArrive"].ToString() + ":00:00";
                        model.DateLeave = DateTime.Parse(leave);
                        model.DateArrive = DateTime.Parse(arr);
                    }
                    catch { }
                    var FormType = Request.Params["hFormType"].ToString();
                    try
                    {

                        if (FormType == "SetCar")
                        {
                            string sCarID = "0";
                            sCarID = Request.Params["hCarID"].ToString();
                          
                            model.CarID = int.Parse(sCarID);
                        }
                    }
                    catch
                    {
                        model.CarID = 0;
                    }
                    var _HandEmps = Request.Form["HandEmps"];
                    var _departments = Request.Form["department"];
                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.OaCarUse>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }
                    this.Options.Title = "[" + model.UsePurpose + "]:" + "出车申请";
                    break;
            }
        }
    }
}
