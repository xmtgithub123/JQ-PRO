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
    public class BussContractSub : IFlowProcessor
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
            this.Options.Message = "外委合同编号:" + model.ConSubNumber + ",外委合同名称：" + model.ConSubName;
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
                var _ProjSubIDs = Request.Form["ProjSubIDs"];
                string[] array = _ProjSubIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //string _projIds = string.Join(",", this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Where(m => array.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.ProjID));
                var IQueryArray = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Where(m => array.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.ProjID);
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.DeleterEmpId == 0 && IQueryArray.Contains(m.ProjId)).ToList();

                foreach (DataModel.Models.DesTaskGroup d in list)
                {
                    string logRefHTML = String.Format("[{0}]{1}>付款合同,被批准", d.ProjNumber, d.TaskGroupName);
                    DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
                    log.BaseLogRefTable = "Business";
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
                    ProjectDynamicDB.AddDynamic(d.ProjId, "Business", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }

                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.BussContractSub model = null;
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

                    string companyType = Request.Params["CompanyType"].ToString();
                    int CompanyID = 0;
                    switch (companyType)
                    {
                        case "SJ":
                            CompanyID = 1;
                            break;
                        case "GC":
                            CompanyID = 2;
                            break;
                        case "CJ":
                            CompanyID = 3;
                            break;
                        default:
                            CompanyID = 0;
                            break;
                    }

                    if (this.Options.RefID > 0)
                    {
                        model = this.CurrentDbContext.Set<DataModel.Models.BussContractSub>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.BussContractSub();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    var _ProjSubIDs = Request.Form["ProjSubIDs"];
                    var _CreateEmpId = TypeHelper.parseInt(Request.Form["CreateEmpId"]);
                    var _CustID = TypeHelper.parseInt(Request.Form["ConSubCustId"]);
                    model.CompanyID = CompanyID;


                    #region 客户单位保存
                    BussCustomer customer = new DBSql.Bussiness.BussCustomer().Get(_CustID);
                    if (customer == null)
                    {
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = Request.Form["ConSubCustId"];
                        addModel.TypeID = 1;
                        addModel.CustLinkMan = "";
                        addModel.CustLinkTel = "";
                        addModel.CustLinkMail = "";
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        _CustID = _custId > 0 ? _custId : 0;
                    }
                    #endregion


                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.BussContractSub>().Add(model);
                        this.CurrentDbContext.SaveChanges();

                        DBSql.Bussiness.BussContractSub op = new DBSql.Bussiness.BussContractSub();
                        op.OnlyCreateOrUpdate(model, _ProjSubIDs, _CreateEmpId, _CustID);
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }
                    else
                    {

                        DBSql.Bussiness.BussContractSub op = new DBSql.Bussiness.BussContractSub();
                        op.OnlyCreateOrUpdate(model, _ProjSubIDs, _CreateEmpId, _CustID);
                    }
                    this.Options.Title = "外委合同编号:" + model.ConSubNumber + ",外委合同名称：" + model.ConSubName;

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();

                    string[] array = _ProjSubIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in array)
                    {
                        int proSubID = TypeHelper.parseInt(s);
                        var projSub = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == proSubID);
                        this.Options.ProjectPhases.Add(projSub == null ? 0 : projSub.ProjID, null);
                    }
                    break;
            }
        }
    }
}
