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
    public class ProjProgressAdjust : IFlowProcessor
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
            string ProjNumber = Request.Form["ProjNumber"];
            string ProjName = Request.Form["ProjName"];

            this.Options.Message = "项目进度调整单[" + ProjNumber + "]" + ProjName;

            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
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
                        DataModel.Models.FlowModel dmFlowMode = this.CurrentDbContext.Set<DataModel.Models.FlowModel>().FirstOrDefault(m => m.ModelRefTable == "IsoFormProjProgressAdjust");
                        if (null != dmFlowMode)
                        {
                            model.FormTypeID = dmFlowMode.Id;
                            model.FormName = dmFlowMode.ModelName;
                        }
                    }
                    model.MvcSetValue();

                    model.FormReason = TypeHelper.parseString(Request.Form["FormReason"]);
                    model.FormNote = TypeHelper.parseString(Request.Form["FormNote"]);
                    model.ProjID = TypeHelper.parseInt(Request.Form["projid"]);
                    model.EngID = TypeHelper.parseInt(Request.Form["engid"]);
                    model.PhaseID = TypeHelper.parseInt(Request.Form["phaseid"]);
                    model.SpecID = TypeHelper.parseInt(Request.Form["specid"]);
                    model.TaskGroupID = TypeHelper.parseInt(Request.Form["taskgroupid"]);
                    model.TaskID = TypeHelper.parseInt(Request.Form["taskid"]);

                    string ProjNumber = Request.Form["ProjNumber"];
                    string ProjName = Request.Form["ProjName"];
                    string PhaseName = Request.Form["PhaseName"];

                    string ProjEmpName = Request.Form["ProjEmpName"];
                    string DatePlanStart = Request.Form["DatePlanStart"];
                    string DatePlanFinish = Request.Form["DatePlanFinish"];
                    string DraftDatePlanStart = Request.Form["DraftDatePlanStart"];
                    string DraftDatePlanFinish = Request.Form["DraftDatePlanFinish"];
                    #region FormCtlXml
                    XmlDocument xdoc = new XmlDocument();
                    XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes");
                    XmlElement rootXml = xdoc.CreateElement("root");

                    #region ProjNumber
                    XmlElement ele = xdoc.CreateElement("ProjNumber");
                    ele.InnerText = ProjNumber;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region ProjName
                    ele = xdoc.CreateElement("ProjName");
                    ele.InnerText = ProjName;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region PhaseName
                    ele = xdoc.CreateElement("PhaseName");
                    ele.InnerText = PhaseName;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region  ProjEmpName
                    ele = xdoc.CreateElement("ProjEmpName");
                    ele.InnerText = ProjEmpName;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region DatePlanStart
                    ele = xdoc.CreateElement("DatePlanStart");
                    ele.InnerText = DatePlanStart;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region DatePlanFinish
                    ele = xdoc.CreateElement("DatePlanFinish");
                    ele.InnerText = DatePlanFinish;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region DraftDatePlanStart
                    ele = xdoc.CreateElement("DraftDatePlanStart");
                    ele.InnerText = DraftDatePlanStart;
                    rootXml.AppendChild(ele);
                    #endregion

                    #region  DraftDatePlanFinish
                    ele = xdoc.CreateElement("DraftDatePlanFinish");
                    ele.InnerText = DraftDatePlanFinish;
                    rootXml.AppendChild(ele);
                    #endregion

                    xdoc.AppendChild(rootXml);
                    #endregion
                    model.FormCtlXml = xdoc.OuterXml;
                    model.ColAttDate1 = TypeHelper.parseDateTime(Request.Form["DatePlanFinish"]);
                    model.ColAttDate2 = TypeHelper.parseDateTime(Request.Form["DraftDatePlanFinish"]);
                    if (model.FormID == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        this.Options.RefID = model.FormID;
                    }

                    this.Options.Title = "[" + ProjNumber + "]" + ProjName + "-项目进度调整单";
                    break;
            }
        }


    }
}
