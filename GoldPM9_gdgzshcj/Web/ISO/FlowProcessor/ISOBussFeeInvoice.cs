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
using System.Web.Mvc;

namespace ISO.FlowProcessor
{
    public class ISOBussFeeInvoice : IFlowProcessor
    {
        /// <summary>
        /// 扩展开票 实体
        /// </summary>
        private class TempInvoice : BussFeeInvoice
        {
            public string Persent;
        }

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
            this.Options.Message = string.Format("{0}开票申请单—", model.FormDate.ToString("yyyy-MM-dd"));
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

                var list = this.CurrentDbContext.Set<DataModel.Models.BussFeeInvoice>().Where(m => m.DeleterEmpId == 0 && m.FormTableID == this.Options.RefID).Select(m => m.ProjID).ToList();
                //var IQueryArray = this.CurrentDbContext.Set<DataModel.Models.ProjSub>().Where(m => list.Contains(m.Id.ToString()) && m.DeleterEmpId == 0).Select(m => m.ProjID);
                var listArray = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.DeleterEmpId == 0 && list.Contains(m.ProjId)).ToList();

                foreach (DataModel.Models.DesTaskGroup d in listArray)
                {
                    string logRefHTML = String.Format("[{0}]{1}>合同开票登记,被批准", d.ProjNumber, d.TaskGroupName);
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
            DBSql.Bussiness.BussFeeInvoice _Fee = new DBSql.Bussiness.BussFeeInvoice();
            _Fee.DbContextRepository(this.CurrentDbContext);
            var _OldFactFee = _Fee.GetList(p => p.DeleterEmpId == 0 && p.FormTableID == model.FormID);

            foreach (var item in _OldFactFee)
            {
                item.FlowID = this.Options.FlowID;
                _Fee.Edit(item);
            }
            _Fee.DbContext.SaveChanges();

        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoForm model = new IsoForm();
        public void OnExecuting()
        {

        }

        [ValidateInput(false)]
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
                    int reuslt = 0;
                    List<BussFeeInvoice> _InvoiceList = new List<BussFeeInvoice>();
                    List<TempInvoice> InvoiceList = new List<TempInvoice>();

                    DBSql.Iso.IsoForm _Form = new DBSql.Iso.IsoForm();
                    _Form.DbContextRepository(this.CurrentDbContext);
                    DBSql.Bussiness.BussFeeInvoice _Fee = new DBSql.Bussiness.BussFeeInvoice();
                    _Fee.DbContextRepository(this.CurrentDbContext);


                    if (this.Options.RefID > 0)
                    {
                        model = _Form.Get(this.Options.RefID);
                    }
                    model.MvcSetValue();

                    if (Request.Params["HiddenData"] != null)
                    {
                        InvoiceList = new JavaScriptSerializer().Deserialize<List<TempInvoice>>(HttpUtility.HtmlDecode(Request.Params["HiddenData"].ToString()));
                    }
                    if (model.FormID > 0)
                    {
                        //修改
                        foreach (var item in InvoiceList)
                        {
                            BussFeeInvoice InvoiceM = new BussFeeInvoice();
                            if (item.Id == 0)
                            {
                                InvoiceM.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                            }
                            else
                            {
                                InvoiceM = _Fee.Get(item.Id);
                                InvoiceM.MvcDefaultEdit(this.Options.CurrentUser.EmpID);
                            }
                            InvoiceM.MvcChangeTarget(item);
                            System.Collections.Hashtable ht = new System.Collections.Hashtable();
                            ht.Add("Persent", item.Persent);
                            InvoiceM.TableXml = Common.XmlConvert.HashTableToXml(ht);
                            _InvoiceList.Add(InvoiceM);
                        }

                        model.MvcDefaultEdit(this.Options.CurrentUser.EmpID);

                        _Form.Edit(model);
                        var _OldFactFee = _Fee.GetList(p => p.DeleterEmpId == 0 && p.FormTableID == model.FormID);
                        foreach (var Delitem in _OldFactFee)
                        {
                            if (!_InvoiceList.Select(p => p.Id).Contains(Delitem.Id))
                            {
                                Delitem.MvcDefaultDel(this.Options.CurrentUser.EmpID);
                                _Fee.Edit(Delitem);
                            }
                        }
                        foreach (BussFeeInvoice FeeItem in _InvoiceList)
                        {
                            if (FeeItem.Id == 0)
                            {
                                FeeItem.FormTableID = model.FormID;
                                _Fee.Add(FeeItem);
                            }
                            else
                            {
                                this.CurrentDbContext.Set<DataModel.Models.BussFeeInvoice>().Attach(FeeItem);
                                _Fee.Edit(FeeItem);
                            }
                        }

                        reuslt = model.FormID;

                    }
                    else
                    {
                        //新增
                        foreach (TempInvoice item in InvoiceList)
                        {
                            BussFeeInvoice InvoiceM = new BussFeeInvoice();
                            InvoiceM.MvcChangeTarget(item);
                            InvoiceM.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                            InvoiceM.FlowID = -1;//在流程中判断是0

                            System.Collections.Hashtable ht = new System.Collections.Hashtable();
                            ht.Add("Persent", item.Persent);
                            InvoiceM.TableXml = Common.XmlConvert.HashTableToXml(ht);
                            _InvoiceList.Add(InvoiceM);
                        }
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        model.FormCtlXml = "";
                        model.RefTable = "ContractInvoiceFee";
                        model.RefID = 0;
                        model.FormDate = System.DateTime.Now;
                        model.FormName = "合同开票登记单";

                        _Form.Add(model);
                        _Form.DbContext.SaveChanges();
                        foreach (var Invoice in _InvoiceList)
                        {
                            Invoice.FormTableID = model.FormID;
                            _Fee.Add(Invoice);
                        }
                        reuslt = model.FormID;
                        //保存附件
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(_Fee.DbContext);
                        ba.MoveFile(model.FormID, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);

                        this.Options.RefID = reuslt;
                        this.Options.Title = string.Format("{0}-开票申请单审批", this.Options.CurrentUser.EmpName);
                    }
                    break;
            }
        }
    }
}
