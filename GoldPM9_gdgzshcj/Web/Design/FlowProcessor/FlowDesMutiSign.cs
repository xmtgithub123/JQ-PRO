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
namespace Design.FlowProcessor
{
    public class FlowDesMutiSign : IFlowProcessor
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
            var _desMutiSign = this.CurrentDbContext.Set<DataModel.Models.DesMutiSign>().FirstOrDefault(m => m.Id == this.Options.RefID);
            var project = this.CurrentDbContext.Set<DataModel.Models.Project>().FirstOrDefault(m => m.Id == _desMutiSign.ProjId && m.DeleterEmpId == 0);
            this.Options.Message = "[" + project.ProjNumber + "]" + project.ProjName + ",会签单-" + _desMutiSign.MutiSignTitle;

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
                var grouplist = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == model.ProjID && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in grouplist)
                {

                    string logRefHTML = String.Format("[{0}]{1}>会签单,被批准", d.ProjNumber, d.TaskGroupName);
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
                    ProjectDynamicDB.AddDynamic(model.ProjID, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion
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
                    if (Common.ExtensionMethods.Value<int>(Request.Form["Id"]) == 0)
                    {
                        DataModel.Models.DesMutiSign model = new DesMutiSign();
                        model.MvcSetValue();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);

                        DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(model.TaskId);
                        model.ProjId = _TaskModel.ProjId;
                        model.MutiSignPhaseId = _TaskModel.TaskPhaseId;
                        model.MutiSignSpecId = _TaskModel.TaskSpecId;
                        model.MutiSignSpecName = (new DBSql.Sys.BaseData().Get(model.MutiSignSpecId)).BaseName;
                        model.FlowId = this.Options.FlowID;
                        var attachlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DataModel.Models.DesMutiSignAttach>>(Request.Params["AttachIDDate"].ToString());

                        var reclist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DataModel.Models.DesMutiSignRec>>(Request.Params["RecEmpData"].ToString());
                        if (reclist.Count == 0)
                        {
                            throw new Common.JQException("保存失败，缺少会签人！");
                        }
                        int reuslt = new DBSql.Design.DesMutiSign().InsertDesMutiSign(model, attachlist, reclist);
                        this.Options.RefID = reuslt;
                        this.Options.ProjectIDs.Clear();
                        this.Options.ProjectIDs.Add(model.ProjId);

                        this.Options.Title = "[会签单]";

                        this.Options.ProjectIDs.Clear();
                        this.Options.ProjectPhases.Clear();
                        this.Options.ProjectPhases.Add(model.ProjId, null);
                    }
                    else
                    {

                    }

                    break;
            }
        }

    }
}
