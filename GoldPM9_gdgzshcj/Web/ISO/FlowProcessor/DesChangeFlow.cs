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
using ISO.Controllers;
using DBSql.Project;
using System.Web.SessionState;

namespace ISO.FlowProcessor
{
    public class DesChangeFlow : IFlowProcessor
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

                    string logRefHTML = String.Format("[{0}]{1}>设计变更通知单,被批准", d.ProjNumber, d.TaskGroupName);
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

            //this.Options.Title = "[" + model.FK_IsoForm_ProjID == null ? "" : model.FK_IsoForm_ProjID.ProjNumber + "]" + model.FK_IsoForm_ProjID == null ? "" : model.FK_IsoForm_ProjID.ProjName;
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoForm model = null;
        private DesChange desChange = new DesChange();
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
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        //SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    desChange.MvcSetValue();
                    string xml = desChange.ModelToXml();
                    model.FormCtlXml = xml;
                    if (model.FormID == 0)
                    {
                        model.RefTable = "IsoFormDesChange";
                        model.FormName = "设计变更通知单";

                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.FormID, Options.CurrentUser.EmpID, Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.FormID;
                    }
                    // this.Options.Title = "[" + model.FK_IsoForm_ProjID==null?"": model.FK_IsoForm_ProjID.ProjNumber + "]" + model.FK_IsoForm_ProjID==null?"": model.FK_IsoForm_ProjID.ProjName;
                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjID, null);
                    var projDB = new DBSql.Project.Project();
                    projDB.DbContextRepository(this.CurrentDbContext);
                    var project = projDB.Get(model.ProjID);
                    if (project == null)
                    {
                        this.Options.Title = "设计变更通知单";
                    }
                    else
                    {
                        this.Options.Title = "[" + project.ProjNumber + "]" + project.ProjName + "---设计变更评审";
                    }
                    break;
            }
        }
    }
}
