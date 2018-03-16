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
using System.Reflection;

namespace ISO.FlowProcessor
{
    public class IsoFormProjectPublish : IFlowProcessor
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

        List<DataModel.Models.IsoFormNode> FormNodeLists = new List<IsoFormNode>();


        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            this.Options.Message = "[" + model.FormName + "]-设计出版交接单";
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                if (FormNodeLists.Count() > 0)// 审批结束 卷册出版 时间
                {
                    foreach (var formNode in FormNodeLists)
                    {
                        new DBSql.Design.DesTask().SetDateForPrint(formNode.ColAttType1, DateTime.Now);
                        //formNode.ColAttType1  == DesTaskID  
                    }
                }
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
                    var op = new DBSql.Iso.IsoForm();
                    op.DbContextRepository(this.CurrentDbContext);
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
                    //isoForm信息
                    List<DataModel.Models.IsoFormNode> FormNodeList = new List<IsoFormNode>();
                    if (Request.Params["ProjTable_Data"] != null)
                    {
                        string var = Request.Params["ProjTable_Data"].ToString().Replace("Id", "ColAttType1");
                        FormNodeList = new JavaScriptSerializer().Deserialize<List<IsoFormNode>>(var);
                        FormNodeLists = FormNodeList;
                    }


                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml("<Root></Root>");
                    var request = HttpContext.Current.Request;
                    Type type = model.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (var key in request.Form.AllKeys)//获取所有的传输的值
                    {
                        PropertyInfo info = properties.Where(p => p.Name == key).FirstOrDefault();
                        if (info != null)//排除在model中存在的字段
                            continue;
                        string value = request.HttpMethod.ToUpper() == "GET" ? request.QueryString[key] : request.Form[key];//判断传输值的方式
                        if (value != null)
                        {
                            var node = xmlDocument.CreateElement("Item");
                            node.SetAttribute("name", key);
                            node.InnerText = value;
                            xmlDocument.DocumentElement.AppendChild(node);//将值组合成xml
                        }
                    }
                    int reuslt = 0;
                    if (model.FormID > 0)
                    {
                        model.FormCtlXml = xmlDocument.OuterXml;
                        model.MvcDefaultEdit(this.Options.CurrentUser.EmpID);
                        reuslt = op.Edit(model, FormNodeList);
                    }
                    else
                    {

                        model.FormCtlXml = xmlDocument.OuterXml;
                        model.RefTable = "IsoFormProjectPublish";
                        //其他值
                        model.FormDate = System.DateTime.Now;
                        model.FormName = "项目出版登记";
                         
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        //op.Add(model);
                        model.FlowID = this.Options.FlowID;
                        reuslt = op.AddProjectDeliver(model, FormNodeList);
                        op.DbContextRepository(op.DbContext);
                    }
                    this.Options.RefID = reuslt;

                    Project project = new DBSql.Project.Project().Get(model.ProjID);

                    if (project != null)
                    {
                        this.Options.Title = "[" + project.ProjNumber + "]" + project.ProjName + "—项目出版登记"; ;
                    }
                    else
                    {
                        this.Options.Title = "未查询到项目，请检查项目是否存在:" + model.ProjID.ToString()+"—项目出版登记";
                    }

                    //this.Options.Title = "[" + model.FormName + "]";
                    break;
            }
        }
    }
}
