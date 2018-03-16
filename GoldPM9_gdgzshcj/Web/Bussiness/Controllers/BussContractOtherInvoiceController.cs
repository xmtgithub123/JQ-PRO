using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
namespace Bussiness.Controllers
{
    [Description("BussContractOtherInvoice")]
    public class BussContractOtherInvoiceController : CoreController
    {
        private DBSql.Bussiness.BussContractOtherInvoice op = new DBSql.Bussiness.BussContractOtherInvoice();
        private DBSql.Bussiness.BussContractOther opCon = new DBSql.Bussiness.BussContractOther();
        private DBSql.Bussiness.BussContractOtherInvoice bussdetail = new DBSql.Bussiness.BussContractOtherInvoice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("合同开票列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInvoice")));
            return View();
        }

        [Description("合同开票列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInvoice_SJ")));
            return View();
        }

        [Description("合同开票列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInvoice_GC")));
            return View();
        }

        [Description("合同开票列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInvoice_CJ")));
            return View();
        }
        #endregion


        #region 付款列表
        [Description("合同收票信息(分公司)")]
        public ActionResult listOtherSub()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubInvoice")));
            return View();
        }

        [Description("合同收票信息(设计公司)")]
        public ActionResult listOtherSub_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubInvoice_SJ")));
            return View();
        }

        [Description("合同收票信息(工程公司)")]
        public ActionResult listOtherSub_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubInvoice_GC")));
            return View();
        }

        [Description("合同收票信息(创景公司)")]
        public ActionResult listOtherSub_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubInvoice_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " Id  desc";
            }
            int ConOtherTypeID = TypeHelper.parseInt(Request.QueryString["ConOtherTypeID"]);

            var TWhere = QueryBuild<DataModel.Models.BussContractOtherInvoice>.True();
            TWhere = TWhere.And(p => p.ConOtherTypeID == ConOtherTypeID);

            var TWhere1 = QueryBuild<DataModel.Models.BussContractOtherInvoice>.True();
            TWhere1 = TWhere1.And(p => p.ConOtherTypeID == ConOtherTypeID);
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    TWhere = TWhere.And(p => p.CompanyID == 1);
                    TWhere1 = TWhere1.And(p => p.CompanyID == 1);
                    break;
                case "GC":
                    TWhere = TWhere.And(p => p.CompanyID == 2);
                    TWhere1 = TWhere1.And(p => p.CompanyID == 2);
                    break;
                case "CJ":
                    TWhere = TWhere.And(p => p.CompanyID == 3);
                    TWhere1 = TWhere1.And(p => p.CompanyID ==3);
                    break;
                default:
                    TWhere = TWhere.And(p => p.CompanyID == 0);
                    TWhere1 = TWhere1.And(p => p.CompanyID == 0);
                    break;
            }

            //var list = op.GetPagedList(p => p.ConOtherTypeID == ConOtherTypeID, PageModel).ToList();

            List<string> result = null;
            if (ConOtherTypeID > 0)
            {
                result = PermissionList("OtherContractSubInvoice");
                switch (companyType)
                {
                    case "SJ":
                        result = PermissionList("OtherContractSubInvoice_SJ");
                        break;
                    case "GC":
                        result = PermissionList("OtherContractSubInvoice_GC");
                        break;
                    case "CJ":
                        result = PermissionList("OtherContractSubInvoice_CJ");
                        break;
                    default:
                        result = PermissionList("OtherContractSubInvoice");
                        break;
                }
            }
            else
            {
                result = PermissionList("OtherContractInvoice");
                switch (companyType)
                {
                    case "SJ":
                        result = PermissionList("OtherContractInvoice_SJ");
                        break;
                    case "GC":
                        result = PermissionList("OtherContractInvoice_GC");
                        break;
                    case "CJ":
                        result = PermissionList("OtherContractInvoice_CJ");
                        break;
                    default:
                        result = PermissionList("OtherContractInvoice");
                        break;
                }
            }

            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                //list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.ConOtherTypeID == ConOtherTypeID, PageModel).ToList();
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                
            }
            var list = op.GetPagedList(TWhere, PageModel).ToList();

            List<string> result1 = null;
            if (ConOtherTypeID > 0)
            {
                result1 = PermissionList("OtherContractSubInvoice");
                switch (companyType)
                {
                    case "SJ":
                        result1 = PermissionList("OtherContractSubInvoice_SJ");
                        break;
                    case "GC":
                        result1 = PermissionList("OtherContractSubInvoice_GC");
                        break;
                    case "CJ":
                        result1 = PermissionList("OtherContractSubInvoice_CJ");
                        break;
                    default:
                        result1 = PermissionList("OtherContractSubInvoice");
                        break;
                }
            }
            else
            {
                result1 = PermissionList("OtherContractInvoice");
                switch (companyType)
                {
                    case "SJ":
                        result1 = PermissionList("OtherContractInvoice_SJ");
                        break;
                    case "GC":
                        result1 = PermissionList("OtherContractInvoice_GC");
                        break;
                    case "CJ":
                        result1 = PermissionList("OtherContractInvoice_CJ");
                        break;
                    default:
                        result1 = PermissionList("OtherContractInvoice");
                        break;
                }
            }

            if (!result1.Contains("allview") && !result1.Contains("dep") && !result1.Contains("emp"))
            {
                TWhere1 = TWhere1.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result1.Contains("allview"))
            {

            }
            else if (result1.Contains("dep"))
            {
                TWhere1 = TWhere1.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result1.Contains("emp"))
            {
                TWhere1 = TWhere1.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                //list1 = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.ConOtherTypeID == ConOtherTypeID, PageModel).ToList();
            }

            var list1= op.GetPagedList(TWhere, PageModel).ToList();

            var td = from n in list
                     select new
                     {
                         n.Id,
                         ConrName = n.FK_BussContractOtherInvoice_RefID == null ? "" : n.FK_BussContractOtherInvoice_RefID.ConrName,
                         ConNumber = n.FK_BussContractOtherInvoice_RefID == null ? "" : n.FK_BussContractOtherInvoice_RefID.ConNumber,
                         CustName = n.FK_BussContractOtherInvoice_RefID == null ? "" : n.FK_BussContractOtherInvoice_RefID.CustName,
                         ConFee = n.FK_BussContractOtherInvoice_RefID == null ? 0 : n.FK_BussContractOtherInvoice_RefID.ConFee,
                         ConrBalanceFee = n.FK_BussContractOtherInvoice_RefID == null ? 0 : n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee,
                         ConOtherFulfilType = n.FK_BussContractOtherInvoice_RefID.ConOtherFulfilType,
                         InvoiceMoneySum = list.Where(p => p.RefID == n.RefID).Select(p => p.InvoiceMoney).Sum(),// list.Where(p => p.RefID == o.Id).Select(p => p.InvoiceMoney).Sum() - n.InvoiceMoney,
                         NoInvoiceMoney = (n.FK_BussContractOtherInvoice_RefID.ConFee - list.Where(p => p.RefID == n.RefID).Select(p => p.InvoiceMoney).Sum()),
                         NoConrBalanceFeeMoney = (n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee - list.Where(p => p.RefID == n.RefID).Select(p => p.InvoiceMoney).Sum()),
                         n.InvoiceMoney,
                         n.InvoiceDate,
                         n.InvoiceNote,
                         Scale = n.FK_BussContractOtherInvoice_RefID.ConFee > 0 ? ((n.InvoiceMoney + list1.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%" : "",

                         ScaleConrBalanceFee = n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee > 0 ? ((n.InvoiceMoney + list1.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",

                         Scales = n.FK_BussContractOtherInvoice_RefID.ConOtherFulfilType == (int)DataModel.ConDealWays.开口 ?
                         n.FK_BussContractOtherInvoice_RefID.ConFee > 0 ? ((n.InvoiceMoney + list1.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%" : "" :
                          n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee > 0 ? ((n.InvoiceMoney + list1.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : ""
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = td
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussContractOtherInvoice();
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussContractOtherInvoice();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            int ConOtherTypeID = TypeHelper.parseInt(Request.QueryString["ConOtherTypeID"]);
            model.ConOtherTypeID = ConOtherTypeID;
            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                var RefTable = op.GetList(p => p.Id > 0).OrderByDescending(p => TypeHelper.parseInt(p.RefTable)).Select(p => p.RefTable).FirstOrDefault();
                if (string.IsNullOrEmpty(RefTable))
                {
                    RefTable = "0";
                }
                else
                {
                    RefTable = TypeHelper.parseString(TypeHelper.parseInt(RefTable) + 1); ;
                }
                model.RefTable = RefTable;
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion




        [Description("其它合同收款 开票登记列表jsonOtherInvoice")]
        [HttpPost]
        public string jsonOtherInvoice(FormCollection Tcondtion)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussContractOtherInvoice>.True();
            if (Tcondtion["ConOtherID"] != null)
            {
                int ConOtherID = Common.ExtensionMethods.Value<int>(Tcondtion["ConOtherID"]);
                TWhere = TWhere.And(p => p.RefID == ConOtherID);
            }
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(TWhere, PageModel).ToList().Select(p => new
            {
                p.Id,
                p.RefID,
                p.InvoiceDate,
                p.InvoiceMoney,
                p.InvoiceNote,
                p.CreatorEmpName,
                text = p.RefID == 0 ? "未指定合同" : (new DBSql.Bussiness.BussContractOther().Get(p.RefID)).ConrName,
            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });

        }




        public ActionResult delOtherInvoiceIDs(FormCollection condition)
        {
            string ids = condition["ids"].ToString();
            int[] id = (from n in ids.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            if (id.Length == 0) return Json(null);
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }




        #region  保存其它合同开票收票
        [Description("保存其它合同开票收票")]
        [HttpPost]
        public ActionResult saveOtherInvoice(int Id)
        {
            var model = new BussContractOtherInvoice();
            string JsonRows = Request.Form["JsonRows"];
            List<DBSql.Bussiness.BussContractOtherInvoiceJson> EvalList = new List<DBSql.Bussiness.BussContractOtherInvoiceJson>();
            List<DataModel.Models.BussContractOtherInvoice> detailList = new List<BussContractOtherInvoice>();

            model.MvcSetValue();
            int reuslt = 0;
            int ConOtherTypeID = TypeHelper.parseInt(Request.Form["ConOtherTypeID"]);
            var RefTableValue = Request.Form["RefTable"];


            string companyType = Request.Params["CompanyType"].ToString();
            int CompanyID = 0;
            switch (companyType)
            {
                case "SJ":
                    CompanyID = 1;
                    break;
                case "GC":
                    CompanyID = 2;
                    break;
                case "CJ":
                    CompanyID = 3;
                    break;
                default:
                    CompanyID = 0;
                    break;
            }

            var RefTable = op.GetList(p => p.Id > 0).OrderByDescending(p => TypeHelper.parseInt(p.RefTable)).Select(p => p.RefTable).FirstOrDefault();
           
            if (string.IsNullOrEmpty(RefTable))
            {
                RefTable = "0";
            }

            if (JsonRows != null && JsonRows != "")
            {
                EvalList = new JavaScriptSerializer().Deserialize<List<DBSql.Bussiness.BussContractOtherInvoiceJson>>(JsonRows);
                foreach (var m in EvalList)
                {
                    DataModel.Models.BussContractOtherInvoice detail = new BussContractOtherInvoice();
                    detail.Id = m.ID;
                    detail.RefID = m.RefID;
                    detail.RefTable = RefTableValue;
                    detail.ConOtherTypeID = ConOtherTypeID;
                   
                    detail.InvoiceMoney = m.InvoiceMoney;
                    detail.InvoiceDate = m.InvoiceDate;
                    detail.CreatorEmpId = m.CreateEmpId == 0 ? userInfo.EmpID : m.CreateEmpId;
                    detail.CreatorEmpName = m.CreateEmpName == "" ? userInfo.EmpName : m.CreateEmpName;
                    detail.CreationTime = m.CreationTime.ToString("yyyy-MM-dd") == "1900-01-01" ? System.DateTime.Now : m.CreationTime;
                    detail.CompanyID = CompanyID;
                    detailList.Add(detail);
                }
                if (Id > 0)
                {
              List<int> detailID = detailList.Where(p => p.Id != 0 && p.ConOtherTypeID == ConOtherTypeID && p.RefTable == RefTableValue).Select(p => p.Id).ToList();
                    bussdetail.Delete(p => !detailID.Contains(p.Id) && p.RefTable == RefTableValue);
                }

                foreach (var m in detailList)
                {
                    if (string.IsNullOrEmpty(RefTableValue))
                    {
                        m.MvcDefaultSave(userInfo.EmpID);
                        m.RefTable = TypeHelper.parseString(TypeHelper.parseInt(RefTable) + 1); ;
                        bussdetail.Add(m);
                    }
                    else
                    {
                        m.RefTable = RefTableValue;
                        if (m.Id > 0)
                        {
                            m.MvcDefaultEdit(userInfo.EmpID);
                            bussdetail.Edit(m);
                        }
                        else
                        {
                            m.MvcDefaultSave(userInfo.EmpID);
                            bussdetail.Add(m);
                        }
                    } 
                }
            }
            bussdetail.UnitOfWork.SaveChanges();
            reuslt = 1;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 付款收票添加
        [Description("付款收票添加")]
        public ActionResult addInvoice()
        {
            var model = new BussContractOtherInvoice();
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;

            model.ConOtherTypeID = 1;

            string page = "infoOtherSub";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "infoOtherSub_sj";
                    break;
                case "GC":
                    page = "infoOtherSub_gc";
                    break;
                case "CJ":
                    page = "infoOtherSub_cj";
                    break;
                default:
                    page = "infoOtherSub";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 付款收票编辑
        [Description("付款收票编辑")]
        public ActionResult editInvoice(int id)
        {
            var model = op.Get(id);
            string page = "infoOtherSub";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "infoOtherSub_sj";
                    break;
                case "GC":
                    page = "infoOtherSub_gc";
                    break;
                case "CJ":
                    page = "infoOtherSub_cj";
                    break;
                default:
                    page = "infoOtherSub";
                    break;
            }

            return View(page, model);
        }
        #endregion

        [Description("其它合同收款 开票登记添加列表jsonOtherInvoice")]
        [HttpPost]
        public string jsonOtherInvoiceAdd(FormCollection Tcondtion)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " Id  desc";
            }
            int ConOtherTypeID = TypeHelper.parseInt(Request.QueryString["ConOtherTypeID"]);
            var TWhere = QueryBuild<DataModel.Models.BussContractOtherInvoice>.True();
            TWhere = TWhere.And(p => p.ConOtherTypeID == ConOtherTypeID);

            int ID = TypeHelper.parseInt(Request.QueryString["ID"]);
            if (ID > 0)
            {
                if (Tcondtion["ConOtherID"] != null)
                {
                    int ConOtherID = Common.ExtensionMethods.Value<int>(Tcondtion["ConOtherID"]);
                    TWhere = TWhere.And(p => p.RefID == ConOtherID);
                }

                var RefTable = Request.QueryString["RefTable"];
                if (RefTable != "")
                {
                    TWhere = TWhere.And(p => p.RefTable == RefTable);
                }
                else
                {
                    int refID = TypeHelper.parseInt(Request.QueryString["ConOtherID"]);
                    if (refID != 0)
                    {
                        TWhere = TWhere.And(p => p.RefID == refID && p.RefTable == "");
                    }
                }
            }
            else
            {
                TWhere = TWhere.And(p => p.Id == 0);
            }
             

            var list = op.GetPagedList(TWhere, PageModel).ToList();
            var Conlist = op.GetPagedList(p => p.ConOtherTypeID == ConOtherTypeID, PageModel).ToList();


            var listOtherInvoice = opCon.GetPagedList(p => p.ConTypeID == ConOtherTypeID, PageModel).ToList();
            var td = from n in list
                     join o in listOtherInvoice on n.RefID equals o.Id
                     select new
                     {
                         n.Id,
                         ConrName = n.FK_BussContractOtherInvoice_RefID.ConrName,
                         ConNumber = n.FK_BussContractOtherInvoice_RefID.ConNumber,
                         CustName = n.FK_BussContractOtherInvoice_RefID.CustName,
                         ConFee = n.FK_BussContractOtherInvoice_RefID.ConFee,
                         //InvoiceMoneySum = list.Where(p => p.RefID == o.Id).Select(p => p.InvoiceMoney).Sum() - n.InvoiceMoney,
                         SumInvoiceMoney = Conlist.Where(p => p.RefID == o.Id).Select(p => p.InvoiceMoney).Sum() - n.InvoiceMoney, 
                         NoInvoiceMoney = (n.FK_BussContractOtherInvoice_RefID.ConFee - list.Where(p => p.RefID == o.Id).Select(p => p.InvoiceMoney).Sum()),
                         n.InvoiceMoney,
                         n.RefID,
                         n.InvoiceDate,
                         n.InvoiceNote,
                         //Scale = (((Conlist.Where(p => p.RefID == o.Id).Select(p => p.InvoiceMoney).Sum() - n.InvoiceMoney) + n.InvoiceMoney) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%",
                         n.CreatorEmpId,
                         n.CreatorEmpName,
                         n.CreationTime,
                         ConOtherFulfilType = n.FK_BussContractOtherInvoice_RefID.ConOtherFulfilType,
                         ConrBalanceFee = n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee,
                         //Scale = n.FK_BussContractOtherInvoice_RefID.ConFee > 0 ? ((n.InvoiceMoney) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%" : "",

                         //ScaleConrBalanceFee = n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee > 0 ? ((n.InvoiceMoney) / n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",

                         Scale = n.FK_BussContractOtherInvoice_RefID.ConFee > 0 ? ((n.InvoiceMoney + Conlist.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%" : "",

                         ScaleConrBalanceFee = n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee > 0 ? ((n.InvoiceMoney + Conlist.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",


                         Scales = n.FK_BussContractOtherInvoice_RefID.ConOtherFulfilType == (int)DataModel.ConDealWays.开口 ?
                         n.FK_BussContractOtherInvoice_RefID.ConFee > 0 ? ((n.InvoiceMoney + Conlist.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConFee * 100).ToString("0.00") + "%" : "" :
                          n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee > 0 ? ((n.InvoiceMoney + Conlist.Where(p => p.RefID == n.RefID && p.ConOtherTypeID == ConOtherTypeID && n.Id > p.Id).Select(p => p.InvoiceMoney).Sum()) / n.FK_BussContractOtherInvoice_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : ""
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = td
            });
        }
    }
}
