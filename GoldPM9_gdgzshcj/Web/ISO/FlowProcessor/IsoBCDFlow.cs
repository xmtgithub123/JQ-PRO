using DBSql.PubFlow;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace ISO.FlowProcessor
{
    class IsoBCDFlow : IFlowProcessor
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
                //DataModel.Models.FileCatalog fileCatalog = new DataModel.Models.FileCatalog();
                //DataModel.Models.FileDetail fileDetail = new DataModel.Models.FileDetail();
                //var fileModel = new DBSql.Archive.FileCatalog().GetQuery(p => p.CatalogjId == model.ProjId).FirstOrDefault();
                //int parId = 0;
                //if (fileModel == null)
                //{
                //    fileCatalog.CatalogjId = model.ProjId;
                //    fileCatalog.CatalongName = model.ProjName;
                //    fileCatalog.CatalogType = "Project";
                //    fileCatalog.ParentId = 0;
                //    this.CurrentDbContext.Set<DataModel.Models.FileCatalog>().Add(fileCatalog);
                //    this.CurrentDbContext.SaveChanges();
                //    parId = fileCatalog.Id;
                //}
                //else
                //{
                //    parId = fileModel.Id;
                //}


                //DataModel.Models.FileCatalog dah = new DataModel.Models.FileCatalog();
                //dah.CatalogjId = model.Id;
                //dah.CatalongName = model.JCNumber;
                //dah.CatalogType = "DAH";
                //dah.ParentId = parId;

                //this.CurrentDbContext.Set<DataModel.Models.FileCatalog>().Add(dah);
                //this.CurrentDbContext.SaveChanges();
                //DataTable dt = new DataTable();
                //if (model.ZJNR!="")
                //{
                //    dt = JQ.Util.JavaScriptSerializerUtil.JsonToDataTable(model.ZJNR);
                //}

                //foreach(DataRow row in dt.Rows)
                //{
                //    fileDetail = new DataModel.Models.FileDetail();
                //    fileDetail.FileCatalogId = dah.Id;
                //    fileDetail.FileId = Convert.ToInt32(row["Id"].ToString());
                //    fileDetail.FileName = row["Name"].ToString();
                //    fileDetail.FileUrl = "";
                //    fileDetail.FileType = row["IsoType"].ToString();
                //    this.CurrentDbContext.Set<DataModel.Models.FileDetail>().Add(fileDetail);
                //    this.CurrentDbContext.SaveChanges();
                //}


                //var fileList = new DBSql.Sys.BaseAttach().GetBaseAttachList(model.Id, "IsoBCD");
                //foreach(DataModel.Models.BaseAttach ba in fileList)
                //{
                //    fileDetail = new DataModel.Models.FileDetail();
                //    fileDetail.FileCatalogId = dah.Id;
                //    fileDetail.FileId =Convert.ToInt32(ba.AttachID);
                //    fileDetail.FileName = ba.AttachName;
                //    fileDetail.FileUrl = ba.AttachDir;
                //    fileDetail.FileType = "IsoBCD";
                //    this.CurrentDbContext.Set<DataModel.Models.FileDetail>().Add(fileDetail);
                //    this.CurrentDbContext.SaveChanges();
                //}

                #region 更新项目动态及日志
                var _isoForm = this.CurrentDbContext.Set<DataModel.Models.IsoBCD>().FirstOrDefault(m => m.Id == this.Options.RefID);

                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _isoForm.ProjId && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>设计报出单,已批准", d.ProjNumber, d.TaskGroupName);
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
                    ProjectDynamicDB.AddDynamic(_isoForm.ProjId, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoBCD model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.IsoBCD>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.IsoBCD();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();
                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.IsoBCD>().Add(model);
                    }

                    this.CurrentDbContext.SaveChanges();
                    var ba = new DBSql.Sys.BaseAttach();
                    ba.DbContextRepository(this.CurrentDbContext);
                    ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);

                    string[] ProjectPhasesIds = model.ProjPhaseId.Split(',');
                    List<int> ProjectPhasesId = new List<int>();
                    foreach (string val in ProjectPhasesIds)
                    {
                        if (TypeHelper.parseInt(val) > 0)
                        {
                            ProjectPhasesId.Add(TypeHelper.parseInt(val));
                        }
                    }

                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjId, ProjectPhasesId);

                    this.Options.RefID = model.Id;

                    this.Options.Title = "[" + model.ProjNumber + "]" + model.ProjName + "的设计报出单";
                    break;
            }
        }
    }
}
