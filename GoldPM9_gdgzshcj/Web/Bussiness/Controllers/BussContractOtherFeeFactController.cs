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
    [Description("BussContractOtherFeeFact")]
    public class BussContractOtherFeeFactController : CoreController
    {
        private DBSql.Bussiness.BussContractOtherFeeFact op = new DBSql.Bussiness.BussContractOtherFeeFact();
        private DBSql.Bussiness.BussCustomer customer = new DBSql.Bussiness.BussCustomer();
        private DBSql.Bussiness.BussContractOther opCon = new DBSql.Bussiness.BussContractOther();
        private DBSql.Bussiness.BussContractOtherFeeFact bussdetail = new DBSql.Bussiness.BussContractOtherFeeFact();


        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("合同收款信息(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractFeeFact")));
            return View();
        }

        [Description("合同收款信息(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractFeeFact_SJ")));
            return View();
        }

        [Description("合同收款信息(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractFeeFact_GC")));
            return View();
        }

        [Description("合同收款信息(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractFeeFact_CJ")));
            return View();
        }
        #endregion


        #region 付款列表
        [Description("合同付款列表(分公司)")]
        public ActionResult listOtherSub()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubFeeFact")));
            return View();
        }

        [Description("合同付款列表(设计公司)")]
        public ActionResult listOtherSub_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubFeeFact_SJ")));
            return View();
        }

        [Description("合同付款列表(工程公司)")]
        public ActionResult listOtherSub_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubFeeFact_GC")));
            return View();
        }

        [Description("合同付款列表(创景公司)")]
        public ActionResult listOtherSub_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractSubFeeFact_CJ")));
            return View();
        }
        #endregion


        #region 其它合同收款条件
        [Description("其它合同 收付款列表")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " Id  desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussContractOtherFeeFact>.True();
            var TWhere1 = QueryBuild<DataModel.Models.BussContractOtherFeeFact>.True();
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
                    TWhere1 = TWhere1.And(p => p.CompanyID == 3);
                    break;
                default:
                    TWhere = TWhere.And(p => p.CompanyID == 0);
                    TWhere1 = TWhere1.And(p => p.CompanyID == 0);
                    break;
            }

            int FactTypeID = TypeHelper.parseInt(Request.QueryString["FactTypeID"]);
            TWhere = TWhere.And(p => p.FactTypeID == FactTypeID);
            List<string> result = null;
            if (FactTypeID > 0)
            {
                result = PermissionList("OtherContractSubFeeFact");
                switch (companyType)
                {
                    case "SJ":
                        result = PermissionList("OtherContractSubFeeFact_SJ");
                        break;
                    case "GC":
                        result = PermissionList("OtherContractSubFeeFact_GC");
                        break;
                    case "CJ":
                        result = PermissionList("OtherContractSubFeeFact_CJ");
                        break;
                    default:
                        result = PermissionList("OtherContractSubFeeFact");
                        break;
                }
            }
            else
            {
                result = PermissionList("OtherContractFeeFact");
                switch (companyType)
                {
                    case "SJ":
                        result = PermissionList("OtherContractFeeFact_SJ");
                        break;
                    case "GC":
                        result = PermissionList("OtherContractFeeFact_GC");
                        break;
                    case "CJ":
                        result = PermissionList("OtherContractFeeFact_CJ");
                        break;
                    default:
                        result = PermissionList("OtherContractFeeFact");
                        break;
                }
            }
          
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
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

            TWhere1 = TWhere1.And(p => p.FactTypeID == FactTypeID);
            List<string> result1 = null;
            if (FactTypeID > 0)
            {
                result1 = PermissionList("OtherContractSubFeeFact");
                switch (companyType)
                {
                    case "SJ":
                        result1 = PermissionList("OtherContractSubFeeFact_SJ");
                        break;
                    case "GC":
                        result1 = PermissionList("OtherContractSubFeeFact_GC");
                        break;
                    case "CJ":
                        result1 = PermissionList("OtherContractSubFeeFact_CJ");
                        break;
                    default:
                        result1 = PermissionList("OtherContractSubFeeFact");
                        break;
                }
            }
            else
            {
                result1 = PermissionList("OtherContractFeeFact");
                switch (companyType)
                {
                    case "SJ":
                        result1 = PermissionList("OtherContractFeeFact_SJ");
                        break;
                    case "GC":
                        result1 = PermissionList("OtherContractFeeFact_GC");
                        break;
                    case "CJ":
                        result1 = PermissionList("OtherContractFeeFact_CJ");
                        break;
                    default:
                        result1 = PermissionList("OtherContractFeeFact");
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
            }

            var list1 = op.GetPagedList(TWhere1, PageModel).ToList();

            string KeyJQSearch = Request.Form["KeyJQSearch"];
            var td = from n in list
                     select new
                     {
                         n.Id,
                         ConrName =n.FK_BussContractOtherFeeFact_RefID==null?"": n.FK_BussContractOtherFeeFact_RefID.ConrName,
                         ConNumber = n.FK_BussContractOtherFeeFact_RefID == null ? "" : n.FK_BussContractOtherFeeFact_RefID.ConNumber,
                         CustName = n.FK_BussContractOtherFeeFact_RefID == null ? "" : n.FK_BussContractOtherFeeFact_RefID.CustName,
                         ConFee = n.FK_BussContractOtherFeeFact_RefID == null ? 0 : n.FK_BussContractOtherFeeFact_RefID.ConFee,
                         ConrBalanceFee = n.FK_BussContractOtherFeeFact_RefID == null ? 0 : n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee,
                         ConOtherFulfilType=n.FK_BussContractOtherFeeFact_RefID.ConOtherFulfilType,
                         //FeeMoneySum=  list.Where(p=>p.RefID==1).Select(p=>p.FeeMoney).Sum(),
                         SumFeeMoney = list.Where(p => p.RefID == n.RefID).Select(p => p.FeeMoney).Sum(),
                         NoFeeMoney = (n.FK_BussContractOtherFeeFact_RefID.ConFee - list.Where(p => p.RefID == n.RefID).Select(p => p.FeeMoney).Sum()),
                         NoConrBalanceFeeMoney = (n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee - list.Where(p => p.RefID == n.RefID).Select(p => p.FeeMoney).Sum()),
                         n.FeeMoney,
                         n.FeerDate,
                         n.FeeNote,
                         Scale = n.FK_BussContractOtherFeeFact_RefID.ConFee > 0 ? ((n.FeeMoney + list1.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%" : "",
                         ScaleConrBalanceFee = n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee > 0 ? ((n.FeeMoney + list1.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",
                          
                           Scales =n.FK_BussContractOtherFeeFact_RefID.ConOtherFulfilType== (int)DataModel.ConDealWays.开口? 
                           n.FK_BussContractOtherFeeFact_RefID.ConFee > 0 ? ((n.FeeMoney + list1.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%" : "": n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee > 0 ? ((n.FeeMoney + list1.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : ""
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
            var model = new BussContractOtherFeeFact();
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

            var model = new BussContractOtherFeeFact();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            int FactTypeID = TypeHelper.parseInt(Request.QueryString["FactTypeID"]);
            model.FactTypeID = FactTypeID;
            int reuslt = 0;
            if (model.Id > 0)
            { 
                op.Edit(model);
            }
            else
            {
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
              
                model.MvcDefaultSave(userInfo.EmpID);
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


        [Description("其它合同收款 列表jsonOtherFee")]
        [HttpPost]
        public string jsonOtherFee(FormCollection Tcondtion)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussContractOtherFeeFact>.True();
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
                p.FeerDate,
                p.FeeMoney,
                p.FeeNote,
                p.CreatorEmpName,
                text = p.RefID == 0 ? "未指定合同" : (new DBSql.Bussiness.BussContractOther().Get(p.RefID)).ConrName,
            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        public ActionResult delOtherFeeByIDs(FormCollection condition)
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



        #region 其它合同收款 付款添加
        [Description("其它合同收款 付款添加")]
        public ActionResult addFeeFact()
        {
            var model = new BussContractOtherFeeFact();
            model.FactTypeID = 1;
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;

            string page = "infoOtherSub";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "infoOtherSub_SJ";
                    break;
                case "GC":
                    page = "infoOtherSub_GC";
                    break;
                case "CJ":
                    page = "infoOtherSub_CJ";
                    break;
                default:
                    page = "infoOtherSub";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 其它合同收款 付款编辑
        [Description("其它合同收款 付款编辑")]
        public ActionResult editFeeFact(int id)
        {
            var model = op.Get(id);

            string page = "infoOtherSub";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "infoOtherSub_SJ";
                    break;
                case "GC":
                    page = "infoOtherSub_GC";
                    break;
                case "CJ":
                    page = "infoOtherSub_CJ";
                    break;
                default:
                    page = "infoOtherSub";
                    break;
            }

            return View(page, model);
        }
        #endregion




        [Description("其它合同收款 付款登记添加列表")]
        [HttpPost]
        public string jsonOtherFeeFactAdd(FormCollection Tcondtion)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " Id  desc";
            }
            int FactTypeID = TypeHelper.parseInt(Request.QueryString["FactTypeID"]);
            var TWhere = QueryBuild<DataModel.Models.BussContractOtherFeeFact>.True();
            TWhere = TWhere.And(p => p.FactTypeID == FactTypeID);

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
            var Conlist = op.GetPagedList(p => p.FactTypeID == FactTypeID, PageModel).ToList();

            var listOtherFeeFact = opCon.GetPagedList(p => p.ConTypeID == FactTypeID, PageModel).ToList();
            var td = from n in list
                     join o in listOtherFeeFact on n.RefID equals o.Id
                     select new
                     {
                         n.Id,
                         ConrName = n.FK_BussContractOtherFeeFact_RefID.ConrName,
                         ConNumber = n.FK_BussContractOtherFeeFact_RefID.ConNumber,
                         CustName = n.FK_BussContractOtherFeeFact_RefID.CustName,
                         ConFee = n.FK_BussContractOtherFeeFact_RefID.ConFee,
                         SumFeeMoney = Conlist.Where(p => p.RefID == o.Id).Select(p => p.FeeMoney).Sum() - n.FeeMoney, 
                        // SumFeeMoney = n.FK_BussContractOtherFeeFact_RefID.ConFee - n.FeeMoney,
                         NoFeeMoney = (n.FK_BussContractOtherFeeFact_RefID.ConFee - list.Where(p => p.RefID == o.Id).Select(p => p.FeeMoney).Sum()),
                         n.FeeMoney,
                         n.RefID,
                         n.FeerDate,
                         n.FeeNote,
                         //Scale = (((Conlist.Where(p => p.RefID == o.Id).Select(p => p.FeeMoney).Sum() - n.FeeMoney) + n.FeeMoney) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%",
                         n.CreatorEmpId,
                         n.CreatorEmpName,
                         n.CreationTime,
                         ConOtherFulfilType = n.FK_BussContractOtherFeeFact_RefID.ConOtherFulfilType,
                         ConrBalanceFee = n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee,
                         //Scale = n.FK_BussContractOtherFeeFact_RefID.ConFee > 0 ? ((n.FeeMoney) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%" : "",
                         //ScaleConrBalanceFee = n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee > 0 ? ((n.FeeMoney) / n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",


                         Scale = n.FK_BussContractOtherFeeFact_RefID.ConFee > 0 ? ((n.FeeMoney + Conlist.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%" : "",
                         ScaleConrBalanceFee = n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee > 0 ? ((n.FeeMoney + Conlist.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : "",

                         Scales = n.FK_BussContractOtherFeeFact_RefID.ConOtherFulfilType == (int)DataModel.ConDealWays.开口 ?
                        n.FK_BussContractOtherFeeFact_RefID.ConFee > 0 ? ((n.FeeMoney + Conlist.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConFee * 100).ToString("0.00") + "%" : "" : n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee > 0 ? ((n.FeeMoney + Conlist.Where(p => p.RefID == n.RefID && p.FactTypeID == FactTypeID && n.Id > p.Id).Select(p => p.FeeMoney).Sum()) / n.FK_BussContractOtherFeeFact_RefID.ConrBalanceFee * 100).ToString("0.00") + "%" : ""
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = td
            });
        }



        #region  保存其它合同收款付款
        [Description("保存其它合同收款付款")]
        [HttpPost]
        public ActionResult saveOtherFeeFact(int Id)
        {
            var model = new BussContractOtherFeeFact();
            string JsonRows = Request.Form["JsonRows"];
            List<DBSql.Bussiness.BussContractOtherFeeFactJson> EvalList = new List<DBSql.Bussiness.BussContractOtherFeeFactJson>();
            List<DataModel.Models.BussContractOtherFeeFact> detailList = new List<BussContractOtherFeeFact>();

            model.MvcSetValue();
            int reuslt = 0;
            int FactTypeID = TypeHelper.parseInt(Request.Form["FactTypeID"]);
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

            model.CompanyID = CompanyID;

            var RefTable = op.GetList(p => p.Id > 0).OrderByDescending(p => TypeHelper.parseInt(p.RefTable) ).Select(p => p.RefTable).FirstOrDefault();
            if (string.IsNullOrEmpty(RefTable))
            {
                RefTable = "0";
            }

            if (JsonRows != null && JsonRows != "")
            {
                EvalList = new JavaScriptSerializer().Deserialize<List<DBSql.Bussiness.BussContractOtherFeeFactJson>>(JsonRows);
                foreach (var m in EvalList)
                {
                    DataModel.Models.BussContractOtherFeeFact detail = new BussContractOtherFeeFact();
                    detail.Id = m.ID;
                    detail.RefID = m.RefID;
                    detail.RefTable = RefTableValue;
                    detail.FactTypeID = FactTypeID;
                    detail.FeeMoney = m.FeeMoney;
                    detail.FeerDate = m.FeerDate;
                    detail.CreatorEmpId = m.CreateEmpId == 0 ? userInfo.EmpID : m.CreateEmpId;
                    detail.CreatorEmpName = m.CreateEmpName == "" ? userInfo.EmpName : m.CreateEmpName;
                    detail.CreationTime = m.CreationTime.ToString("yyyy-MM-dd") == "1900-01-01" ? System.DateTime.Now : m.CreationTime;
                    detail.CompanyID = CompanyID;
                    detailList.Add(detail);
                }
                if (Id > 0)
                {
                    List<int> detailID = detailList.Where(p => p.Id != 0 && p.FactTypeID == FactTypeID && p.RefTable == RefTableValue).Select(p => p.Id).ToList();
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
         
    }
}
