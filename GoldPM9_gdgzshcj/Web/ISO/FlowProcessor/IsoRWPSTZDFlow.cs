using DBSql.PubFlow;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Xml;

namespace ISO.FlowProcessor
{
    class IsoRWPSTZDFlow : IFlowProcessor
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
                var _isoForm = this.CurrentDbContext.Set<DataModel.Models.IsoRWPSTZD>().FirstOrDefault(m => m.Id == this.Options.RefID);

                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _isoForm.ProjID && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>任务评审通知单,被批准", d.ProjNumber, d.TaskGroupName);
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

        private DataModel.Models.IsoRWPSTZD model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.IsoRWPSTZD>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.IsoRWPSTZD();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();

                    model.ProjID = TypeHelper.parseInt(Request.Form["projId"]);
                    model.ProjPhaseId = Request.Form["ProjPhaseId"].ToString();

                    model.DesTaskGroupId = TypeHelper.parseInt(Request.Form["desTaskGroupId"]);
                    model.CompanyID = TypeHelper.parseInt(Request.Form["CompanyID"]);

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml("<Root></Root>");

                    #region

                    var node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "itemsA");
                    node.InnerText = "0";
                    var cbo11 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["stritemsA"]));
                    if (cbo11 != null && cbo11 != "")
                    {
                        node.InnerText = cbo11;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);                   

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents1");
                    node.InnerText = "";
                    var AppraisalContents1 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents1"]));
                    if (AppraisalContents1 != null && AppraisalContents1 != "")
                    {
                        node.InnerText = AppraisalContents1;

                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "itemsB");
                    node.InnerText = "0";
                    var cbo21 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["stritemsB"]));
                    if (cbo21 != null && cbo21 != "")
                    {
                        node.InnerText = cbo21;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                 

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents2");
                    node.InnerText = "";
                    var AppraisalContents2 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents2"]));
                    if (AppraisalContents2 != null && AppraisalContents2 != "")
                    {
                        node.InnerText = AppraisalContents2;

                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "itemsC");
                    node.InnerText = "0";

                    var cbo31 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["stritemsC"]));
                    if (cbo31 != null && cbo31 != "")
                    {
                        node.InnerText = cbo31;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                 
                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents3");
                    node.InnerText = "";
                    var AppraisalContents3 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents3"]));
                    if (AppraisalContents3 != null && AppraisalContents3 != "")
                    {
                        node.InnerText = AppraisalContents3;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "itemsD");
                    node.InnerText = "0";
                    var cbo41 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["stritemsD"]));
                    if (cbo41 != null && cbo41 != "")
                    {
                        node.InnerText = cbo41;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);


                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents4");
                    node.InnerText = "";

                    var AppraisalContents4 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents4"]));
                    if (AppraisalContents4 != null && AppraisalContents4 != "")
                    {
                        node.InnerText = AppraisalContents4;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "itemsE");
                    node.InnerText = "0";
                    var cbo51 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["stritemsE"]));
                    if (cbo51 != null && cbo51 != "")
                    {
                        node.InnerText = cbo51;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);


                  

                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents5");
                    node.InnerText = "";
                    var AppraisalContents5 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents5"]));
                    if (AppraisalContents5 != null && AppraisalContents5 != "")
                    {

                        node.InnerText = AppraisalContents5;
                    }
                    xmlDocument.DocumentElement.AppendChild(node);


                    node = xmlDocument.CreateElement("Item");
                    node.SetAttribute("name", "AppraisalContents6");
                    node.InnerText = "";

                    var AppraisalContents6 = TypeHelper.parseString(HttpUtility.HtmlDecode(Request.Form["AppraisalContents6"]));
                    if (AppraisalContents6 != null && AppraisalContents6 != "")
                    {
                        node.InnerText = AppraisalContents6;                        
                    }
                    xmlDocument.DocumentElement.AppendChild(node);

                    #endregion





                    model.AppraisalContens = xmlDocument.OuterXml;


                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.IsoRWPSTZD>().Add(model);
                    }

                    this.CurrentDbContext.SaveChanges();

                    var ba = new DBSql.Sys.BaseAttach();
                    ba.DbContextRepository(this.CurrentDbContext);
                    ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);



                    List<int> list = new List<int>();
                    list.Add(TypeHelper.parseInt(model.ProjPhaseId));

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjID, list);

                    this.Options.RefID = model.Id;
                    this.Options.Title = "[" + model.ProjNumber + "]" + model.ProjNumber + "的任务评审通知单";
                    break;
            }
        }
    }
}
