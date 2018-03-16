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
    public class BussBiddingInfo : IFlowProcessor
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
            this.Options.Message = "投标编号:" + model.BiddingNumber + ",项目名称:" + model.EngineeringName;
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
                var mess = new DBSql.OA.OaSendMess();
                mess.EmpID = this.Options.CurrentUser.EmpID;
                mess.RecEmpID = new List<int>() { model.Delegator };
                mess.MessTitle = "投标文件制作:项目名称" + model.EngineeringName;
                mess.MessLinkTitle = "投标文件制作";
                string companyType = "";
                switch (model.CompanyID)
                {
                    case 1:
                        companyType = "SJ";
                        break;
                    case 2:
                        companyType = "GC";
                        break;
                    case 3:
                        companyType = "CJ";
                        break;
                    default:
                        companyType = "info";
                        break;
                }

                mess.MessLinkUrl = string.Format("Bussiness/BussBiddingInfo/Edit?id={0}&CompanyType={1}", model.Id,companyType);
                mess.MessRefID = model.Id;
                mess.MessRefTable = "BussBiddingInfo";
                mess.SendMess();
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }
        DataModel.Models.BussBiddingInfo model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfo>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.BussBiddingInfo();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    var _BiddingManageID = Request.Form["BiddingManageID"] == null ? "0-0" : Request.Form["BiddingManageID"];
                    var _BiddingDeptId = Request.Form["EmpDepID"] == null ? "" : Request.Form["EmpDepID"];
                    var _CoOrganizer = Request.Form["CoOrganizer"]==null?"": Request.Form["CoOrganizer"];
                    var _EntryManager = Request.Form["EntryManager"]==null?"0-0": Request.Form["EntryManager"];
                    var _Delegator = Request.Form["Delegator"]==null? "0-0" : Request.Form["Delegator"];

                    model.BiddingDeptId = _BiddingDeptId;
                    model.BiddingManageID = TypeHelper.parseInt(_BiddingManageID.Split('-')[0]);
                    model.CoOrganizer = _CoOrganizer;
                    model.EntryManager = TypeHelper.parseInt(_EntryManager.Split('-')[0]);
                    model.Delegator = TypeHelper.parseInt(_Delegator.Split('-')[0]);

                    #region 客户单位保存

                    DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                    addModel.CustName = model.CustName;
                    addModel.CustLinkMan = "";
                    addModel.CustLinkTel = "";
                    addModel.CustLinkMail = "";
                    addModel.EmpModel = this.Options.CurrentUser;
                    int _custId = addModel.AddCust();
                    model.CustID = _custId > 0 ? _custId : 0;

                    #endregion

                    string companyType = Request.Params["CompanyType"].ToString();
                    switch (companyType)
                    {
                        case "SJ":
                            model.CompanyID = 1;
                            break;
                        case "GC":
                            model.CompanyID = 2;
                            break;
                        case "CJ":
                            model.CompanyID = 3;
                            break;
                        default:
                            model.CompanyID = 0;
                            break;
                    }

                    if (model.Id == 0)
                    {
                        this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfo>().Add(model);
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
                    //var source = this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfoPackage>().Where(m => m.BussBiddingInfoID == this.Options.RefID).ToList();
                    //RecuriseCreateOrUpdate(xmlDocument.DocumentElement, 0, source, "", model.Id);
                    //RecuriseDelete(source, xmlDocument.DocumentElement);

                    this.Options.Title = "投标编号:" + model.BiddingNumber + ",项目名称:" + model.EngineeringName;
                    break;
            }
        }

        private void RecuriseCreateOrUpdate(XmlElement xmlElement, int parentID, List<DataModel.Models.BussBiddingInfoPackage> source, string path, int mainTableID)
        {
            var items = xmlElement.SelectNodes("Item");
            foreach (XmlElement item in items)
            {
                var id = TypeHelper.parseInt(item.GetAttribute("Id"));
                if (id < 0)
                {
                    //插入
                    var data = new DataModel.Models.BussBiddingInfoPackage()
                    {
                        BussBiddingInfoID = mainTableID,
                        PackageNumber = item.GetAttribute("PackageNumber"),
                        BiddingProgress = TypeHelper.parseInt(item.GetAttribute("BiddingProgress")),
                        WinTime = TypeHelper.parseDateTime(item.GetAttribute("WinTime") == "" ? "1900-01-01" : item.GetAttribute("WinTime")),
                    };
                    this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfoPackage>().Add(data);
                    this.CurrentDbContext.SaveChanges();
                    id = data.Id;
                }
                else if (id > 0)
                {
                    var data = this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfoPackage>().FirstOrDefault(m => m.Id == id);
                    if (data == null)
                    {
                        continue;
                    }
                    data.PackageNumber = item.GetAttribute("PackageNumber");
                    data.BiddingProgress = TypeHelper.parseInt(item.GetAttribute("BiddingProgress"));
                    data.WinTime = TypeHelper.parseDateTime(item.GetAttribute("WinTime") == "" ? "1900-01-01" : item.GetAttribute("WinTime"));
                    this.CurrentDbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    this.CurrentDbContext.SaveChanges();
                    id = data.Id;
                }
                RecuriseCreateOrUpdate(item, id, source, path.ToString() + id.ToString() + "/", mainTableID);
            }
        }
        private void RecuriseDelete(List<DataModel.Models.BussBiddingInfoPackage> sources, XmlElement xmlElement)
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
                    this.CurrentDbContext.Set<DataModel.Models.BussBiddingInfoPackage>().Remove(source);
                }
            }
        }
    }
}
