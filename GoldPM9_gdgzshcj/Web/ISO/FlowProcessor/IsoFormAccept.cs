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
using System.Xml;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ISO.FlowProcessor
{
    public class IsoFormAccept : IFlowProcessor
    {
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

        public HttpSessionState Session
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
            var _projectSub = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == model.ProjID);
            this.Options.Message = "[" + _projectSub.SubNumber + "]" + _projectSub.SubName + "-项目外委验收";
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
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _projectSub.ProjID && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>项目外委验收,被批准", d.ProjNumber, d.TaskGroupName);
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
                    ProjectDynamicDB.AddDynamic(_projectSub.ProjID, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
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
                        model.FormDate = DateTime.Now;
                        DataModel.Models.FlowModel dmFlowMode = this.CurrentDbContext.Set<DataModel.Models.FlowModel>().FirstOrDefault(m => m.ModelRefTable == "IsoFormAccept");
                        if (null != dmFlowMode)
                        {
                            model.FormTypeID = dmFlowMode.Id;
                            model.FormName = dmFlowMode.ModelName;
                        }
                    }

                    model.MvcSetValue();
                    model.ProjID = TypeHelper.parseInt(Request.Form["ProjSubIDs"]);
                    var _projectSub = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == model.ProjID);

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml("<Root></Root>");
                    var YSQKJL = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strYSQKJL"]));
                    if (YSQKJL != null && YSQKJL != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "YSQKJL");
                        node.InnerText = YSQKJL;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var PJJY = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strPJJY"]));
                    if (PJJY != null && PJJY != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "PJJY");
                        node.InnerText = PJJY;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var CJYSBM = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strCJYSBM"]));
                    if (CJYSBM != null && CJYSBM != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "CJYSBM");
                        node.InnerText = CJYSBM;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var CJYSRY = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strCJYSRY"]));
                    if (CJYSRY != null && CJYSRY != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "CJYSRY");
                        node.InnerText = CJYSRY;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var YSRMQZ = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strYSRMQZ"]));
                    if (YSRMQZ != null && YSRMQZ != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "YSRMQZ");
                        node.InnerText = YSRMQZ;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var YLDYJ = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strYLDYJ"]));
                    if (YLDYJ != null && YLDYJ != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "YLDYJ");
                        node.InnerText = YLDYJ;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    var YSZZ = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["strYSZZ"]));
                    if (YSZZ != null && YSZZ != "")
                    {
                        var node = xmlDocument.CreateElement("Item");
                        node.SetAttribute("name", "YSZZ");
                        node.InnerText = YSZZ;
                        xmlDocument.DocumentElement.AppendChild(node);
                    }
                    model.FormCtlXml = xmlDocument.OuterXml;
                    if (model.FormID == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.FormID, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);

                        this.Options.RefID = model.FormID;
                    }
                    this.Options.Title = "[" + _projectSub.SubNumber + "]" + _projectSub.SubName + "-项目外委验收";

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(_projectSub.ProjID, null);
                    break;
            }
        }
    }
}
