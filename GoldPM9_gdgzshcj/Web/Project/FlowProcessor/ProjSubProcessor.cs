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
using System.Web.Script.Serialization;

namespace Project.FlowProcessor
{
    public class ProjSubProcessor : IFlowProcessor
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

        public HttpSessionState Session
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

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            this.Options.Message = "外委项目编号:" + model.SubNumber + ",外委项目名称:" + model.SubName;
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                #region 更新项目动态及日志      
                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();

                var _projId = TypeHelper.parseInt(Request.Form["projId"]);
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _projId && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>外委项目申请,被批准", d.ProjNumber, d.TaskGroupName);
                    DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
                    log.BaseLogRefTable = "DesTaskGroup";
                    log.BaseLogRefID = (int)d.Id;
                    log.BaseLogRefHTML = logRefHTML;
                    log.BaseLogEmpID = this.Options.CurrentUser.EmpID;
                    log.EmpName = this.Options.CurrentUser.EmpName;
                    log.BaseLogIP = this.Options.CurrentUser.LoginIP;
                    log.BaseLogDate = DateTime.Now;
                    log.BaseLogTypeID = 10;
                    __log.Add(log);
                    __log.UnitOfWork.SaveChanges();

                    // 写入 项目动态
                    ProjectDynamicDB.AddDynamic(_projId, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }
        DataModel.Models.ProjSub model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.ProjSub();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    model.SubEmpId = TypeHelper.parseInt(Request.Form["SubEmpId"].Split('-')[0]);
                    model.ProjID = TypeHelper.parseInt(Request.Form["projId"]);
                    model.ColAttType2 = TypeHelper.parseInt(Request.Form["CustomerID"]);

                    string companyType = Request.Params["CompanyType"].ToString();
                    switch (companyType)
                    {
                        case "SJ":
                            model.CompanyID = 1;
                            break;
                        case "GC":
                            model.CompanyID = 2;
                            break;
                        case "CJ":
                            model.CompanyID = 3;
                            break;
                        default:
                            model.CompanyID = 0;
                            break;
                    }

                    #region 客户单位保存
                    BussCustomer customer = new DBSql.Bussiness.BussCustomer().Get(model.ColAttType2);
                    if (customer == null || model.ColAttType2 == 0)
                    {
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = Request.Form["CustomerID"];
                        addModel.CustLinkMan = "";
                        addModel.CustLinkTel = "";
                        addModel.CustLinkMail = "";
                        addModel.TypeID = 1;
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        model.ColAttType2 = _custId > 0 ? _custId : 0;
                    }
                    #endregion

                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }
                    this.Options.Title = "外委项目编号:" + model.SubNumber + ",外委项目名称:" + model.SubName;                  

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjID, null);

                    break;
            }
        }
    }
}
