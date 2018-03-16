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

namespace Bussiness.FlowProcessor
{
    public class BusinessProjectRecord : IFlowProcessor
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
            this.Options.Message = "项目信息备案[" + model.ProjCode + "]" + model.ProjName;
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

        private DataModel.Models.BussProjInfoRecords model = null;
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
                        model = this.CurrentDbContext.Set<DataModel.Models.BussProjInfoRecords>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new BussProjInfoRecords();
                        SetCreateProperties(model);
                    }
                    
                    model.MvcSetValue();
                    var _RecordsEmpId = Request.Form["RecordsEmpId"];
                    model.RecordsEmpId = TypeHelper.parseInt(_RecordsEmpId.Split('-')[0]);
                    //model.CustID = TypeHelper.parseInt(Request.Form["CustID"]);

                    string companyType = Request.Params["CompanyType"].ToString();
                    switch (companyType)
                    {
                        case "SJ":
                            model.CompanyId = 1;
                            break;
                        case "GC":
                            model.CompanyId = 2;
                            break;
                        case "CJ":
                            model.CompanyId = 3;
                            break;
                        default:
                            model.CompanyId = 0;
                            break;
                    }

                    #region 客户单位保存
                    DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                    addModel.CustName = model.CustName;
                    addModel.CustLinkMan = "";
                    addModel.CustLinkTel = "";
                    addModel.CustLinkMail = "";
                    addModel.EmpModel = this.Options.CurrentUser;
                    int _custId = addModel.AddCust();
                    model.CustID = _custId > 0 ? _custId : 0;
                    #endregion

                    model.InfoSource = Request.Form["InfoSource"];
                    model.ProjSummarize = Request.Form["ProjSummarize"];
                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.BussProjInfoRecords>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }
                    this.Options.Title = "[" + model.ProjCode + "]" + model.ProjName;
                    break;
            }
        }
    }
}
