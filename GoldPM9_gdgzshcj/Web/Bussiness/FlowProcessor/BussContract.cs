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
using System.Web.Script.Serialization;

namespace Bussiness.FlowProcessor
{
    public class BussContract : IFlowProcessor
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
            this.Options.Message = "合同编号:" + model.ConNumber + ",合同名称" + model.ConName;
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

                List<DataModel.Models.BusProjContractRelation> ProList = new List<BusProjContractRelation>();
                if (Request.Params["ProjTable_Data"] != null && Request.Params["ProjTable_Data"].ToString() != "")
                {
                    ProList = new JavaScriptSerializer().Deserialize<List<BusProjContractRelation>>(Request.Params["ProjTable_Data"].ToString());
                }

                if (ProList.Count > 0)
                {
                    string[] array = new string[ProList.Count];
                    int i = 0;
                    foreach (DataModel.Models.BusProjContractRelation m in ProList)
                    {
                        array[i] = m.ProjID.ToString();
                        i++;
                    }

                    var IQueryArray = this.CurrentDbContext.Set<DataModel.Models.Project>().Where(m => array.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.Id);
                    var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.DeleterEmpId == 0 && IQueryArray.Contains(m.ProjId)).ToList();

                    foreach (DataModel.Models.DesTaskGroup d in list)
                    {
                        string logRefHTML = String.Format("[{0}]{1}>收款合同,被批准", d.ProjNumber, d.TaskGroupName);
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
                }
                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.BussContract model = new DataModel.Models.BussContract();
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
                    var op = new DBSql.Bussiness.BussContract();
                    op.DbContextRepository(this.CurrentDbContext);

                    string companyType = Request.Params["CompanyType"].ToString();
                    int companyID = 0;
                    switch (companyType)
                    {
                        case "SJ":
                            companyID = 1;
                            break;
                        case "GC":
                            companyID = 2;
                            break;
                        case "CJ":
                            companyID = 3;
                            break;
                        default:
                            companyID = 0;
                            break;
                    }

                    //var model = new DataModel.Models.BussContract();
                    if (this.Options.RefID > 0)
                    {
                        model = this.CurrentDbContext.Set<DataModel.Models.BussContract>().FirstOrDefault(p => p.Id == this.Options.RefID);
                    }
                    model.CompanyID = companyID;
                    model.MvcSetValue();
                    //项目信息
                    List<DataModel.Models.BusProjContractRelation> ProList = new List<BusProjContractRelation>();
                    if (Request.Params["ProjTable_Data"] != null && Request.Params["ProjTable_Data"].ToString() != "")
                    {
                        ProList = new JavaScriptSerializer().Deserialize<List<BusProjContractRelation>>(Request.Params["ProjTable_Data"].ToString());
                    }

                    int reuslt = 0;
                    if (model.Id > 0)
                    {
                        model.MvcDefaultEdit(this.Options.CurrentUser.EmpID);
                        #region 客户单位保存
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = model.CustName;
                        addModel.CustLinkMan = model.CustLinkManName;
                        addModel.CustLinkTel = model.CustLinkManTel;
                        addModel.CustLinkMail = model.CustLinkManWeb;
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        model.CustID = _custId > 0 ? _custId : 0;
                        #endregion
                        reuslt = op.Edit(model, ProList);
                    }
                    else
                    {
                        List<DataModel.Models.BussFeePlan> planList = new List<BussFeePlan>();
                        if (Request.Params["PlanTable_Data"] != null)
                        {
                            planList = new JavaScriptSerializer().Deserialize<List<BussFeePlan>>(Request.Params["PlanTable_Data"].ToString());
                            foreach (var planModel in planList)
                            {
                                planModel.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                                planModel.ProjID = planModel.ProjID == -1 ? 0 : planModel.ProjID;
                            }
                        }
                        //扩展赋值
                        model.CreateEmpName = this.Options.CurrentUser.EmpName;
                        model.CreateDate = System.DateTime.Now;
                        //赋值默认值
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        model.FlowID = this.Options.FlowID;

                        #region 客户单位保存
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = model.CustName;
                        addModel.CustLinkMan = model.CustLinkManName;
                        addModel.CustLinkTel = model.CustLinkManTel;
                        addModel.CustLinkMail = model.CustLinkManWeb;
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        model.CustID = _custId > 0 ? _custId : 0;
                        #endregion
                        reuslt = op.Add(model, planList, ProList);

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(op.DbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                    }

                    #region 更改主合同的变更金额
                    var model_zht = op.Get(model.FatherID);
                    var model_bght = op.GetList(p => p.FatherID == model.FatherID && p.ColAttVal1 == "1").OrderByDescending(p => p.ConDate).FirstOrDefault();
                    if (model_zht != null && model_bght != null)
                    {
                        model_zht.BGConFee = model_bght.ConFee;
                        op.Edit(model_zht);
                    }
                    else if(model_zht != null && model_bght == null)
                    {
                        model_zht.BGConFee = 0;
                        op.Edit(model_zht);
                    }
                    #endregion

                    this.Options.RefID = reuslt;
                    this.Options.Title = string.Format("[{0}]{1}—收款合同审批", model.ConNumber, model.ConName);

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    if (null != ProList && ProList.Count > 0)
                    {
                        foreach (var BusProjContractRelation in ProList)
                        {
                            this.Options.ProjectPhases.Add(BusProjContractRelation.ProjID, null);
                        }
                    }
                    break;
            }
        }
    }
}
