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

namespace ISO.FlowProcessor
{
    public class IsoFormProjDelegateService : IFlowProcessor
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


            //this.Options.Title = "[" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjNumber + "]" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjName + "-项目工代";
            
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                #region 更新项目动态及日志      
                var _isoForm = this.CurrentDbContext.Set<DataModel.Models.IsoForm>().FirstOrDefault(m => m.FormID == this.Options.RefID);

                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _isoForm.ProjID && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>项目工代,被批准", d.ProjNumber, d.TaskGroupName);
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
                    ProjectDynamicDB.AddDynamic(_isoForm.ProjID, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoForm model = null;
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
                        model = this.CurrentDbContext.Set<DataModel.Models.IsoForm>().FirstOrDefault(m => m.FormID == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new IsoForm();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    //var _RecordsEmpId = Request.Form["RecordsEmpId"];
                    //model.RecordsEmpId = TypeHelper.parseInt(_RecordsEmpId.Split('-')[0]);
                    //model.CustomerID = TypeHelper.parseInt(HttpContext.Current.Request.Form["cg"]);
                    //model.InfoSource = Request.Form["InfoSource"];
                    //model.ProjSummarize = Request.Form["ProjSummarize"];
                    if (model.FormID == 0)
                    {
                        model.RefTable = "IsoFormProjDelegateService";
                        model.FormName = "项目工代";
                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.FormID, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.FormID;
                    }

                    Project project = proj.Get(model.ProjID);

                    if (project != null)
                    {
                        this.Options.Title = "[" + project.ProjNumber + "]" + project.ProjName+"—项目工代";
                    }
                    else
                    {
                        this.Options.Title = "未查询到项目，请检查项目是否存在:" + model.ProjID.ToString() + "—项目工代";
                    }

                    //this.Options.Title = "[" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjNumber + "]" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjName;

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(project == null ? 0 : project.Id, null);

                    break;
            }
        }
    }
}
