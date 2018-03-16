using DBSql.PubFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;
using JQ.Web;
using System.Xml;
using JQ.Util;

namespace ISO.FlowProcessor
{
    public class SubFeeFact : IFlowProcessor
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
            if (isoForm == null)
            {
                return;
            }
            this.Options.Message = "付款合同付款";
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

                var list = this.CurrentDbContext.Set<DataModel.Models.BussSubFeeFact>().Where(m => m.DeleterEmpId == 0 && m.FormTableID == this.Options.RefID).Select(m => m.ProjSubId.ToString()).ToList();
                var IQueryArray = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Where(m => list.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.ProjID);
                var listArray = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.DeleterEmpId == 0 && IQueryArray.Contains(m.ProjId)).ToList();

                foreach (DataModel.Models.DesTaskGroup d in listArray)
                {
                    string logRefHTML = String.Format("[{0}]{1}>付款申请单,被批准", d.ProjNumber, d.TaskGroupName);
                    DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
                    log.BaseLogRefTable = "Cost";
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
                    ProjectDynamicDB.AddDynamic(d.ProjId, "Cost", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }

                #endregion
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

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
                    Save();
                    break;
            }
        }

        private DataModel.Models.IsoForm isoForm;
        private void Save()
        {

            var isoFormDB = this.CurrentDbContext.Set<DataModel.Models.IsoForm>();
            if (this.Options.RefID == 0)
            {
                isoForm = new DataModel.Models.IsoForm();
                isoForm.CreationTime = DateTime.Now;
                isoForm.MvcDefaultSave(this.Options.CurrentUser);
                isoForm.FormDate = DateTime.Now;
                this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(isoForm);
            }
            else
            {
                isoForm = isoFormDB.FirstOrDefault(m => m.FormID == this.Options.RefID);
            }
            isoForm.MvcSetValue();
            isoForm.FormCtlXml = "";
            isoForm.RefTable = "ContractSubFeeFact";
            isoForm.RefID = 0;
            this.Options.Title = "付款合同费用登记审批流程";
            this.CurrentDbContext.SaveChanges();
            if (this.Options.RefID == 0)
            {
                this.Options.RefID = isoForm.FormID;
                var ba = new DBSql.Sys.BaseAttach();
                ba.DbContextRepository(this.CurrentDbContext);
                ba.MoveFile(isoForm.FormID, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
            }
            var strxml = Request.Form["strxml"];
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(strxml);
            }
            catch { }
            CreateOrUpdateBySubFeeFact(isoForm, xmlDocument);


            this.Options.ProjectIDs.Clear();
            this.Options.ProjectPhases.Clear();
            var list = this.CurrentDbContext.Set<DataModel.Models.BussSubFeeFact>().Where(m => m.DeleterEmpId == 0 && m.FormTableID == this.Options.RefID).Select(m => m.ProjSubId.ToString()).ToList();
            var IQueryArray = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Where(m => list.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.ProjID);
            var listArray = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.DeleterEmpId == 0 && IQueryArray.Contains(m.ProjId)).ToList();
            this.Options.ProjectIDs.Clear();
            this.Options.ProjectPhases.Clear();
            foreach (DataModel.Models.DesTaskGroup d in listArray)
            {
                this.Options.ProjectPhases.Add(TypeHelper.parseInt(d.Id), null);
            }
        }

        public void CreateOrUpdateBySubFeeFact(DataModel.Models.IsoForm model, XmlDocument xml)
        {
            var subFeeFactDb = this.CurrentDbContext.Set<DataModel.Models.BussSubFeeFact>();
            var source = subFeeFactDb.Where(m => m.FormTableID == model.FormID).ToList();
            RecuriseCreateOrUpdate(xml.DocumentElement, source, "", model.FormID, subFeeFactDb);
            RecuriseDelete(source, xml.DocumentElement, subFeeFactDb);
            this.CurrentDbContext.SaveChanges();
        }

        private void RecuriseCreateOrUpdate(XmlElement xmlElement, List<DataModel.Models.BussSubFeeFact> source, string path, int mainTableID, DbSet<DataModel.Models.BussSubFeeFact> subFeeFactDb)
        {
            var items = xmlElement.SelectNodes("Item");
            foreach (XmlElement item in items)
            {
                var id = JQ.Util.TypeParse.parse<int>(item.GetAttribute("Id"));
                if (id < 0)
                {
                    //插入
                    var data = new DataModel.Models.BussSubFeeFact()
                    {
                        FormTableID = mainTableID,
                        ProjSubId = JQ.Util.TypeParse.parse<int>(item.GetAttribute("ProjSubId")),
                        ConSubId = JQ.Util.TypeParse.parse<int>(item.GetAttribute("ConSubId")),
                        SubFeeFactMoney = JQ.Util.TypeParse.parse<decimal>(item.GetAttribute("SubFeeFactMoney")),
                        SubFeeFactDate = JQ.Util.TypeParse.parse<DateTime>(item.GetAttribute("SubFeeFactDate"), Convert.ToDateTime("1900-01-01")),
                        SubFeePlanEmpId = this.Options.CurrentUser.EmpID,
                        SKDW = item.GetAttribute("SKDW"),
                        SubFeeFactNote = "",
                        SubXml = item.OuterXml
                    };
                    Common.ModelConvert.MvcDefaultSave<DataModel.Models.BussSubFeeFact>(data, this.Options.CurrentUser);
                    subFeeFactDb.Add(data);
                }
                else if (id > 0)
                {
                    var data = source.FirstOrDefault(m => m.Id == id);
                    if (data == null)
                    {
                        continue;
                    }
                    data.FormTableID = mainTableID;
                    data.ProjSubId = JQ.Util.TypeParse.parse<int>(item.GetAttribute("ProjSubId"));
                    data.ConSubId = JQ.Util.TypeParse.parse<int>(item.GetAttribute("ConSubId"));
                    data.SubFeeFactMoney = JQ.Util.TypeParse.parse<decimal>(item.GetAttribute("SubFeeFactMoney"));
                    data.SubFeeFactDate = JQ.Util.TypeParse.parse<DateTime>(item.GetAttribute("SubFeeFactDate"), Convert.ToDateTime("1900-01-01"));
                    data.SubFeePlanEmpId = this.Options.CurrentUser.EmpID;
                    data.SubFeeFactNote = "";
                    data.SubXml = item.OuterXml;
                    data.SKDW = item.GetAttribute("SKDW");
                    Common.ModelConvert.MvcDefaultSave<DataModel.Models.BussSubFeeFact>(data, this.Options.CurrentUser);
                }
            }
        }

        private void RecuriseDelete(List<DataModel.Models.BussSubFeeFact> sources, XmlElement xmlElement, DbSet<DataModel.Models.BussSubFeeFact> subFeeFactDb)
        {
            var allItems = xmlElement.GetElementsByTagName("Item");
            foreach (var source in sources)
            {
                var isIn = false;
                foreach (XmlElement item in allItems)
                {
                    if (JQ.Util.TypeParse.parse<int>(item.GetAttribute("Id")) == source.Id)
                    {
                        isIn = true;
                        break;
                    }
                }
                if (!isIn)
                {
                    subFeeFactDb.Remove(source);
                }
            }
        }

    }
}