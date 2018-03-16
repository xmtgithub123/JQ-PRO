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
using JQ.Util;
using System.Data;
using DataModel.Models;

namespace OA.FlowProcessor
{
    public class OaEquipDealFlow : IFlowProcessor
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

        public HttpSessionState Session
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
                var originalValues = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(m => m.EquipFormID == model.Id && m.EquipFormType == "OaEquipDeal");
                foreach (OaEquipStock oaModel in originalValues)
                {
                    oaModel.EquipActionID = 5;
                }

                this.CurrentDbContext.SaveChanges();
                this.Options.Message += "流程审批通过！";
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }

        private DataModel.Models.OaEquipDeal model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaEquipDeal>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.OaEquipDeal();
                        //SetCreateProperties(model);
                    }
                    model.MvcSetValue();

                    model.RepairReportEmpId = TypeHelper.parseInt(Request.Form["RepairReportEmpId"].Split('-')[0]);
                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.OaEquipDeal>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }

                    OaEquipStock OaEquipStock = this.CurrentDbContext.Set<OaEquipStock>().FirstOrDefault(p => p.EquipFormID == model.Id && p.EquipFormType == "OaEquipDeal");
                    if (OaEquipStock == null)
                    {
                        OaEquipStock = new OaEquipStock();
                        OaEquipStock.EquipActionID = 0;
                        OaEquipStock.EquipFormID = model.Id;
                        OaEquipStock.EquipDateTime = DateTime.Now;
                        OaEquipStock.EquipFormType = "OaEquipDeal";
                        OaEquipStock.EquipNote = "设备处置";
                    }
                    OaEquipStock.MvcDefaultSave(this.Options.CurrentUser);
                    OaEquipStock.EquipID = model.EquipId;
                    OaEquipStock.EquipCount = model.EquipCount;

                    this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Add(OaEquipStock);
                    this.CurrentDbContext.SaveChanges();

                    this.Options.Title = "[" + model.CreationTime.ToString("yyyy-MM-dd") + "]" + model.CreatorEmpName + "的设备处置申请单";
                    break;
            }
        }

    }
}
