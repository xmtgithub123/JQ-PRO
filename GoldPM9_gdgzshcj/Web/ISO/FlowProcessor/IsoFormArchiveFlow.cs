using System;
using System.Data.Entity;
using System.Linq;
using DataModel.Models;
using DBSql.PubFlow;
using System.Web;
using JQ.Web;
using ISO.Controllers;
using System.Web.SessionState;

namespace ISO.FlowProcessor
{
    public class IsoFormArchiveFlow : IFlowProcessor
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
                model.FlowTime = DateTime.Now;
                this.CurrentDbContext.SaveChanges();
            }
        }

        public void OnExecuted(bool isSuccess)
        {
        }

        private DataModel.Models.IsoForm model = null;
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
                        model.FlowID = this.Options.FlowID;
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    if (model.FormID == 0)
                    {
                        model.RefTable = "IsoFormArchive";
                        model.FormName = "档案申请单";
                        model.FormDate = DateTime.Now;
                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        this.Options.RefID = model.FormID;
                    }

                    ArchElecProject archElecProject = new DBSql.Archive.ArchElecProject().Get(model.ProjID); ;
                    if (archElecProject != null)
                    {
                        Project project = new DBSql.Project.Project().Get(archElecProject.ProjectId);
                        if (project != null)
                        {
                            this.Options.Title = "[" + project.ProjNumber + "]" + project.ProjName;
                        }else
                        {
                            this.Options.Title = "未查询到项目，请检查项目是否存在:" + archElecProject.ProjectId.ToString()+"—档案申请审批";
                        }
                        
                    }
                    else
                    {
                        this.Options.Title = "未查询到归档项目，请检查项目是否存在:" + model.ProjID.ToString() + "—档案申请审批";
                    }
                    break;
            }
        }
    }
}
