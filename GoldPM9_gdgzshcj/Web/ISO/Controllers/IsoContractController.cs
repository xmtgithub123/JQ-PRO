using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using System.Xml;

namespace ISO.Controllers
{
    public class IsoContractController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        /// <summary>
        /// 合同结清状态
        /// </summary>
        private class ConSettleItem
        {
            public int ConIsFeeFinished;
            public string Name;
        }

        /// <summary>
        /// 扩展实际收款 实体
        /// </summary>
        private class ConFeeTemp
        {
            public int Id;
            public int ProjID;
            public int ConID;
            public decimal FeeMoney;
            public DateTime FeeDate;
            public string Persent;
            public int ConIsFeeFinished;
        }

        /// <summary>
        /// 扩展开票 实体
        /// </summary>
        private class TempInvoice : BussFeeInvoice
        {
            public string Persent;
        }

        /// <summary>
        /// 新增 收款表单--
        /// </summary>
        /// <returns></returns>
        // GET: IsoContract
        public ActionResult FeeFact()
        {
            List<ConSettleItem> Item = new List<ConSettleItem>();
            Item.Add(new ConSettleItem() { ConIsFeeFinished = 0, Name = "未结清" });
            Item.Add(new ConSettleItem() { ConIsFeeFinished = 1, Name = "已结清" });
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(Item);
            var model = new IsoForm();
            model.CreatorEmpName = userInfo.EmpName;
            model.CreationTime = System.DateTime.Now;
            return View("FeeFact", model);
        }

        /// <summary>
        /// 编辑 收款表单
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            List<ConSettleItem> Item = new List<ConSettleItem>();
            Item.Add(new ConSettleItem() { ConIsFeeFinished = 0, Name = "未结清" });
            Item.Add(new ConSettleItem() { ConIsFeeFinished = 1, Name = "已结清" });
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(Item);

            var model = op.Get(id);
            return View("FeeFact", model);
        }

        /// <summary>
        /// 保存 收款表单
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult save(int FormID)
        {
            int reuslt = 0;

            List<BussFeeFact> _FeeList = new List<BussFeeFact>();
            List<BussContract> _ConList = new List<BussContract>();
            List<ConFeeTemp> _FactFee = new List<ConFeeTemp>();
            var EF = new DBSql.EFRepository<IsoForm>();

            DBSql.Iso.IsoForm _Form = new DBSql.Iso.IsoForm();
            _Form.DbContextRepository(EF.UnitOfWork, EF.DbContext);
            DBSql.Bussiness.BussContract _Con = new DBSql.Bussiness.BussContract();
            _Con.DbContextRepository(EF.UnitOfWork, EF.DbContext);
            DBSql.Bussiness.BussFeeFact _Fee = new DBSql.Bussiness.BussFeeFact();
            _Fee.DbContextRepository(EF.UnitOfWork, EF.DbContext);

            var model = new IsoForm();
            if (FormID > 0)
            {
                model = _Form.Get(FormID);
            }
            model.MvcSetValue();

            if (Request.Params["HiddenData"] != null)
            {
                _FactFee = new JavaScriptSerializer().Deserialize<List<ConFeeTemp>>(Request.Params["HiddenData"].ToString());

            }
            if (model.FormID > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                foreach (ConFeeTemp item in _FactFee)
                {
                    var ItemFee = new BussFeeFact();
                    if (item.Id == 0)
                    {
                        ItemFee.MvcDefaultSave(userInfo.EmpID);
                    }
                    else
                    {
                        ItemFee = _Fee.Get(item.Id);
                        //ItemFee.Id = item.Id;
                        ItemFee.MvcDefaultEdit(userInfo.EmpID);
                    }
                    ItemFee.ProjID = item.ProjID;
                    ItemFee.ConID = item.ConID;
                    ItemFee.FeeMoney = item.FeeMoney;
                    ItemFee.FeeDate = item.FeeDate;
                    ItemFee.ProjID = ItemFee.ProjID == -1 ? 0 : ItemFee.ProjID;
                    Hashtable ht = new Hashtable();
                    ht.Add("Percent", item.Persent);
                    ht.Add("ConIsFeeFinished", item.ConIsFeeFinished);
                    ItemFee.TableXml = Common.XmlConvert.HashTableToXml(ht);
                    _FeeList.Add(ItemFee);
                    var ConItem = new DataModel.Models.BussContract();
                    if (_Con != null)
                    {
                        ConItem = _Con.Get(item.ConID);
                        ConItem.ConIsFeeFinished = item.ConIsFeeFinished == 0 ? false : true;
                        _ConList.Add(ConItem);
                    }
                }
                #region 修改数据库操作
                try
                {
                    EF.UnitOfWork.BeginTransaction();
                    _Form.Edit(model);
                    var _OldFactFee = _Fee.GetList(p => p.DeleterEmpId == 0 && p.FormTableID == model.FormID);
                    foreach (var Delitem in _OldFactFee)
                    {
                        if (!_FeeList.Select(p => p.Id).Contains(Delitem.Id))
                        {
                            Delitem.MvcDefaultDel(userInfo.EmpID);
                            _Fee.Edit(Delitem);
                        }
                    }
                    foreach (BussFeeFact FeeItem in _FeeList)
                    {
                        if (FeeItem.Id == 0)
                        {
                            FeeItem.FormTableID = model.FormID;
                            _Fee.Add(FeeItem);
                        }
                        else
                        {
                            _Fee.Edit(FeeItem);
                        }
                    }

                    foreach (var item in _ConList)
                    {
                        _Con.Edit(item);
                    }
                    EF.UnitOfWork.CommitTransaction();
                    reuslt = model.FormID;
                }
                catch (Exception e)
                {
                    EF.UnitOfWork.RollBackTransaction();
                    reuslt = -1;
                }

                #endregion
            }
            else
            {
                foreach (ConFeeTemp item in _FactFee)
                {
                    var ItemFee = new BussFeeFact();
                    //ItemFee.MvcSetValue();
                    ItemFee.MvcDefaultSave(userInfo.EmpID);
                    ItemFee.ProjID = item.ProjID;
                    ItemFee.ConID = item.ConID;
                    ItemFee.FeeMoney = item.FeeMoney;
                    ItemFee.FeeDate = item.FeeDate;
                    ItemFee.ProjID = ItemFee.ProjID == -1 ? 0 : ItemFee.ProjID;
                    Hashtable ht = new Hashtable();
                    ht.Add("Percent", item.Persent);
                    ht.Add("ConIsFeeFinished", item.ConIsFeeFinished);
                    ItemFee.TableXml = Common.XmlConvert.HashTableToXml(ht);
                    _FeeList.Add(ItemFee);
                    var ConItem = new DataModel.Models.BussContract();
                    if (_Con != null)
                    {
                        ConItem = _Con.Get(item.ConID);
                        ConItem.ConIsFeeFinished = item.ConIsFeeFinished == 0 ? false : true;
                        _ConList.Add(ConItem);
                    }
                }

                model.MvcDefaultSave(userInfo.EmpID);
                model.FormCtlXml = "";
                model.RefTable = "ContractFactFee";
                model.RefID = 0;
                model.FormDate = System.DateTime.Now;
                model.FormName = "合同收款登记单";
                #region 数据库操作
                try
                {
                    EF.UnitOfWork.BeginTransaction();
                    _Form.Add(model);
                    _Form.DbContext.SaveChanges();
                    foreach (BussFeeFact item in _FeeList)
                    {
                        item.FormTableID = model.FormID;
                        _Fee.Add(item);
                    }
                    foreach (var item in _ConList)
                    {
                        _Con.Edit(item);
                    }
                    EF.UnitOfWork.CommitTransaction();
                    reuslt = model.FormID;

                    //保存附件
                    var ba = new DBSql.Sys.BaseAttach();
                    ba.DbContextRepository(_Fee.DbContext);
                    ba.MoveFile(model.FormID, userInfo.EmpID, userInfo.EmpName);

                }
                catch
                {
                    EF.UnitOfWork.RollBackTransaction();
                    reuslt = -1;
                }
                #endregion

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        /// <summary>
        /// 新增 开票表单
        /// </summary>
        /// <returns></returns>
        public ActionResult FeeInvoice()
        {
            var _InvoiceType = new DBSql.Sys.BaseData().GetDataSetByEngName("InvoiceType");
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(_InvoiceType.Select(p => new { p.BaseID, p.BaseName }));

            var model = new IsoForm();
            model.CreatorEmpName = userInfo.EmpName;
            model.CreationTime = System.DateTime.Now;

            string page = "FeeInvoice";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "FeeInvoice_SJ";
                    break;
                case "GC":
                    page = "FeeInvoice_GC";
                    break;
                case "CJ":
                    page = "FeeInvoice_CJ";
                    break;
                default:
                    page = "FeeInvoice";
                    break;
            }

            return View(page, model);
        }

        public ActionResult EditInvoice(int id)
        {
            var _InvoiceType = new DBSql.Sys.BaseData().GetDataSetByEngName("InvoiceType");
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(_InvoiceType.Select(p => new { p.BaseID, p.BaseName }));

            var model = op.Get(id);

            string page = "FeeInvoice";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "FeeInvoice_sj";
                    break;
                case "GC":
                    page = "FeeInvoice_gc";
                    break;
                case "CJ":
                    page = "FeeInvoice_cj";
                    break;
                default:
                    page = "FeeInvoice";
                    break;
            }

            return View(page, model);
        }

        /// <summary>
        /// 保存 开票申请单
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult save_Invoice(int FormID)
        {
            int reuslt = 0;

            List<BussFeeInvoice> _InvoiceList = new List<BussFeeInvoice>();
            List<TempInvoice> InvoiceList = new List<TempInvoice>();

            var EF = new DBSql.EFRepository<IsoForm>();
            DBSql.Iso.IsoForm _Form = new DBSql.Iso.IsoForm();
            _Form.DbContextRepository(EF.UnitOfWork, EF.DbContext);
            DBSql.Bussiness.BussFeeInvoice _Fee = new DBSql.Bussiness.BussFeeInvoice();
            _Fee.DbContextRepository(EF.UnitOfWork, EF.DbContext);

            var model = new IsoForm();
            if (FormID > 0)
            {
                model = _Form.Get(FormID);
            }
            model.MvcSetValue();


            if (Request.Params["HiddenData"] != null)
            {
                InvoiceList = new JavaScriptSerializer().Deserialize<List<TempInvoice>>(Request.Params["HiddenData"].ToString());
            }
            if (FormID > 0)
            {
                //修改
                foreach (var item in InvoiceList)
                {
                    BussFeeInvoice InvoiceM = new BussFeeInvoice();
                    if (item.Id == 0)
                    {
                        InvoiceM.MvcDefaultSave(userInfo.EmpID);
                    }
                    else
                    {
                        InvoiceM = _Fee.Get(item.Id);
                        InvoiceM.MvcDefaultEdit(userInfo.EmpID);
                    }
                    InvoiceM.MvcChangeTarget(item);
                    Hashtable ht = new Hashtable();
                    ht.Add("Persent", item.Persent);
                    InvoiceM.TableXml = Common.XmlConvert.HashTableToXml(ht);
                    _InvoiceList.Add(InvoiceM);
                }

                model.MvcDefaultEdit(userInfo.EmpID);
                try
                {
                    EF.UnitOfWork.BeginTransaction();
                    _Form.Edit(model);
                    var _OldFactFee = _Fee.GetList(p => p.DeleterEmpId == 0 && p.FormTableID == model.FormID);
                    foreach (var Delitem in _OldFactFee)
                    {
                        if (!_InvoiceList.Select(p => p.Id).Contains(Delitem.Id))
                        {
                            Delitem.MvcDefaultDel(userInfo.EmpID);
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
                            EF.DbContext.Set<DataModel.Models.BussFeeInvoice>().Attach(FeeItem);
                            _Fee.Edit(FeeItem);
                        }
                    }
                    EF.UnitOfWork.CommitTransaction();
                    reuslt = model.FormID;
                }
                catch (Exception ex)
                {
                    EF.UnitOfWork.RollBackTransaction();
                    reuslt = -1;
                }
            }
            else
            {
                //新增
                foreach (TempInvoice item in InvoiceList)
                {
                    BussFeeInvoice InvoiceM = new BussFeeInvoice();
                    InvoiceM.MvcChangeTarget(item);
                    InvoiceM.MvcDefaultSave(userInfo.EmpID);

                    Hashtable ht = new Hashtable();
                    ht.Add("Persent", item.Persent);
                    InvoiceM.TableXml = Common.XmlConvert.HashTableToXml(ht);
                    _InvoiceList.Add(InvoiceM);
                }
                model.MvcDefaultSave(userInfo.EmpID);
                model.FormCtlXml = "";
                model.RefTable = "ContractInvoiceFee";
                model.RefID = 0;
                model.FormDate = System.DateTime.Now;
                model.FormName = "合同开票登记单";

                try
                {
                    EF.UnitOfWork.BeginTransaction();
                    _Form.Add(model);
                    _Form.DbContext.SaveChanges();
                    foreach (var Invoice in _InvoiceList)
                    {
                        Invoice.FormTableID = model.FormID;
                        _Fee.Add(Invoice);
                    }
                    EF.UnitOfWork.CommitTransaction();
                    reuslt = model.FormID;
                }
                catch
                {
                    EF.UnitOfWork.RollBackTransaction();
                    reuslt = -1;
                }

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }


        public ActionResult AddSubFeeFact()
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeFact";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeFact_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeFact_GC";
                    break;
                case "CJ":
                    permission = PermissionList("ContractSubFee_CJ");
                    page = "SubFeeFact_CJ";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeFact";
                    break;
            }

            var model = new IsoForm();
            ViewBag.AllowSave = (permission.Contains("edit") || permission.Contains("alledit")) ? "1" : "0";



            return View(page, model);
        }
        public ActionResult EditSubFeeFact(int id)
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeFact";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeFact_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeFact_GC";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeFact";
                    break;
            }

            var model = op.Get(id);
            ViewBag.AllowSave = (permission.Contains("edit") || permission.Contains("alledit")) ? "1" : "0";
            return View(page, model);
        }

        public ActionResult AddSubFeeInvoice()
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeInvoice";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeInvoice_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeInvoice_GC";
                    break;
                case "CJ":
                    permission = PermissionList("ContractSubFee_CJ");
                    page = "SubFeeInvoice_CJ";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeInvoice";
                    break;
            }

            var _baseDataList = dbBaseData.GetDataSetByEngName("InvoiceType").ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DataModel.Models.BaseData dmBaseData in _baseDataList)
            {
                var _key = dmBaseData.BaseID.ToString();
                var _value = dmBaseData.BaseName;
                SelectListItem _item = new SelectListItem();
                _item.Text = _value;
                _item.Value = _key;
                items.Add(_item);
            }
            ViewData["Items"] = items;

            if (permission.Contains("edit") || permission.Contains("alledit"))
            {
                ViewBag.ExportPermission = "['submit', 'close']";

            }
            else
            {
                ViewBag.ExportPermission = "['close']";

            }
            var model = new IsoForm();
            return View(page, model);
        }
        public ActionResult EditSubFeeInvoice(int id)
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeInvoice";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeInvoice_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeInvoice_GC";
                    break;
                case "CJ":
                    permission = PermissionList("ContractSubFee_CJ");
                    page = "SubFeeInvoice_GC";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeInvoice";
                    break;
            }

            var _baseDataList = dbBaseData.GetDataSetByEngName("InvoiceType").ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DataModel.Models.BaseData dmBaseData in _baseDataList)
            {
                var _key = dmBaseData.BaseID.ToString();
                var _value = dmBaseData.BaseName;
                SelectListItem _item = new SelectListItem();
                _item.Text = _value;
                _item.Value = _key;
                items.Add(_item);
            }
            ViewData["Items"] = items;

            if (permission.Contains("edit") || permission.Contains("alledit"))
            {
                ViewBag.ExportPermission = "['submit', 'close']";

            }
            else
            {
                ViewBag.ExportPermission = "['close']";

            }

            var model = op.Get(id);
            return View(page, model);
        }

        [Description("保存SubFeeFact")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveSubFeeFact(int FormID)
        {
            int reuslt = 0;
            var model = new IsoForm();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            model.FormCtlXml = "";
            model.RefTable = "ContractSubFeeFact";
            model.RefID = 0;
            model.FormDate = System.DateTime.Now;
            model.FormName = "付款合同付款登记";
            Common.ModelConvert.MvcDefaultSave<DataModel.Models.IsoForm>(model, userInfo);
            try
            {
                var strxml = Request.Form["strxml"];
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(strxml);
                op.CreateOrUpdateBySubFeeFact(model, xmlDocument, userInfo);
                reuslt = 1;
            }
            catch
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }


        [Description("保存SubFeeInvoice")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveSubFeeInvoice(int FormID)
        {
            int reuslt = 0;
            var model = new IsoForm();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            model.FormCtlXml = "";
            model.RefTable = "ContractSubFeeInvoice";
            model.RefID = 0;
            model.FormDate = System.DateTime.Now;
            model.FormName = "付款合同收票登记";
            Common.ModelConvert.MvcDefaultSave<DataModel.Models.IsoForm>(model, userInfo);
            try
            {
                var strxml = Request.Form["strxml"];
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(strxml);
                op.CreateOrUpdateBySubInvoice(model, xmlDocument, userInfo);
                reuslt = 1;
            }
            catch
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
    }
}