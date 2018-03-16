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

namespace Bussiness.FlowProcessor
{
    public class BusinessTender : IFlowProcessor
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
            this.Options.Message = "招标编号:" + model.TendersNumber + ",招标名称:" + model.TendersName;
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

        public void OnExecuting()
        {

        }
        DataModel.Models.BussTendersInfo model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.BussTendersInfo>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new BussTendersInfo();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.BussTendersInfo>().Add(model);
                        this.CurrentDbContext.SaveChanges();

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }

                    this.Options.RefID = model.Id;
                    XmlDocument xmlDocument = new XmlDocument();
                    var strxml = HttpUtility.HtmlDecode(HttpContext.Current.Request.Form["strxml"]);
                    if (!string.IsNullOrEmpty(strxml))
                    {
                        xmlDocument.LoadXml(strxml ?? "");
                    }
                    var source = this.CurrentDbContext.Set<DataModel.Models.BussTendersInfoDetail>().Where(m => m.BussTendersInfoID == this.Options.RefID).ToList();
                    RecuriseCreateOrUpdate(xmlDocument.DocumentElement, 0, source, "", model.Id);
                    RecuriseDelete(source, xmlDocument.DocumentElement);


                    this.Options.Title = "招标编号:" + model.TendersNumber + ",招标名称:" + model.TendersName;
                    break;
            }
        }

        private void RecuriseCreateOrUpdate(XmlElement xmlElement, int parentID, List<DataModel.Models.BussTendersInfoDetail> source, string path, int mainTableID)
        {
            var items = xmlElement.SelectNodes("Item");
            foreach (XmlElement item in items)
            {
                var id = TypeHelper.parseInt(item.GetAttribute("Id"));
                if (id < 0)
                {
                    //插入
                    var data = new DataModel.Models.BussTendersInfoDetail()
                    {
                        BussTendersInfoID = mainTableID,
                        CustomerID = TypeHelper.parseInt(item.GetAttribute("CustomerID")),
                        BusinessPoints = TypeHelper.parseDecimal(item.GetAttribute("BusinessPoints")),
                        TechnologyPoints = TypeHelper.parseDecimal(item.GetAttribute("TechnologyPoints")),
                        TotalityPoints = TypeHelper.parseDecimal(item.GetAttribute("TotalityPoints")),
                        WinState = TypeHelper.parseInt(item.GetAttribute("WinState")),
                        WinTime = TypeHelper.parseDateTime(item.GetAttribute("WinTime") == "" ? "1900-01-01" : item.GetAttribute("WinTime")),
                    };
                    this.CurrentDbContext.Set<DataModel.Models.BussTendersInfoDetail>().Add(data);
                    this.CurrentDbContext.SaveChanges();
                    id = data.Id;
                }
                else if (id > 0)
                {
                    var data = this.CurrentDbContext.Set<DataModel.Models.BussTendersInfoDetail>().FirstOrDefault(m => m.Id == id);
                    if (data == null)
                    {
                        continue;
                    }
                    data.CustomerID = TypeHelper.parseInt(item.GetAttribute("CustomerID"));
                    data.BusinessPoints = TypeHelper.parseDecimal(item.GetAttribute("BusinessPoints"));
                    data.TechnologyPoints = TypeHelper.parseDecimal(item.GetAttribute("TechnologyPoints"));
                    data.TotalityPoints = TypeHelper.parseDecimal(item.GetAttribute("TotalityPoints"));
                    data.WinState = TypeHelper.parseInt(item.GetAttribute("WinState"));
                    data.WinTime = TypeHelper.parseDateTime(item.GetAttribute("WinTime") == "" ? "1900-01-01" : item.GetAttribute("WinTime"));
                    this.CurrentDbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    this.CurrentDbContext.SaveChanges();
                    id = data.Id;
                }
                RecuriseCreateOrUpdate(item, id, source, path.ToString() + id.ToString() + "/", mainTableID);
            }
        }
        private void RecuriseDelete(List<DataModel.Models.BussTendersInfoDetail> sources, XmlElement xmlElement)
        {
            var allItems = xmlElement.GetElementsByTagName("Item");
            foreach (var source in sources)
            {
                var isIn = false;
                foreach (XmlElement item in allItems)
                {
                    if (TypeHelper.parseInt(item.GetAttribute("Id")) == source.Id)
                    {
                        isIn = true;
                        break;
                    }
                }
                if (!isIn)
                {
                    this.CurrentDbContext.Set<DataModel.Models.BussTendersInfoDetail>().Remove(source);
                }
            }
        }
    }
}
