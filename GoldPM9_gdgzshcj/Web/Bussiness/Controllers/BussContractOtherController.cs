using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Data;
namespace Bussiness.Controllers
{
    [Description("BussContractOther")]
    public class BussContractOtherController : CoreController
    {
        private DBSql.Bussiness.BussContractOther op = new DBSql.Bussiness.BussContractOther();
        private DBSql.Bussiness.BussContract conop = new DBSql.Bussiness.BussContract();
        private DBSql.Bussiness.BussContractSub conSubop = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");



        [Description("收款合同列表")]
        public string GetContractJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["cnkeyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = conop.GetQuery(p => p.DeleterEmpId == 0).ToList();

            List<DataModel.Models.BussContract> result = (from item in list
                                                          where item.ConName.Contains(condition)    //条件查询
                                                          select item).ToList<DataModel.Models.BussContract>();

            var a = (from item in result
                     select new
                     {
                         item.Id,
                         item.CustName,
                         item.ConName,
                         item.ConNumber,
                         item.ConDate
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }


        [Description("验证其它合同编号是否存在")]
        public string CheckConNumberExists(FormCollection form)
        {
            int Count = 0;
            int TypeID = TypeHelper.parseInt(Request.Params["TypeID"]);//客户类型
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            string ConNumber = Request.Params["ConNumber"].Trim();
            if (Id == 0)//新增其它合同
            {
                Count = op.GetList(p => p.ConNumber.Trim() == ConNumber && p.ConTypeID == TypeID).Count();
            }
            else//修改其它合同
            {
                Count = op.GetList(p => p.ConNumber == ConNumber && p.ConTypeID == TypeID && p.Id != Id).Count();
            }
            return Count.ToString();
        }

        [Description("付款合同列表")]
        public string GetContractSubJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["cnkeyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = conSubop.GetQuery(p => p.DeleterEmpId == 0).ToList();

            var a = (from item in list
                     where item.ConSubName.Contains(condition)
                     select new
                     {
                         item.Id,
                         CustName = item.ConSubCustId == 0 ? "" : item.FK_BussContractSub_ConSubCustId.CustName,
                         ConName = item.ConSubName,
                         ConNumber = item.ConSubNumber,
                         ConDate = item.ConSubDate,

                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }


        #region 付款列表
        [Description("付款列表(分公司)")]
        public ActionResult listConOtherSub()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContracSubtInfo")));
            return View();
        }

        [Description("付款列表(设计公司)")]
        public ActionResult listConOtherSub_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContracSubtInfo_SJ")));
            return View();
        }

        [Description("付款列表(工程公司)")]
        public ActionResult listConOtherSub_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContracSubtInfo_GC")));
            return View();
        }

        [Description("付款列表(创景公司)")]
        public ActionResult listConOtherSub_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContracSubtInfo_CJ")));
            return View();
        }
        #endregion

        #region 付款列表json
        [Description("付款列表jsonSub")]
        [HttpPost]
        public string jsonSub()
        {

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int ConTypeID = TypeHelper.parseInt(Request.QueryString["ConTypeID"]);
            PageModel.SelectCondtion.Add("ConTypeID", ConTypeID);

            DataTable dt = op.GetContractOtherList(PageModel);
            var data = dt.AsEnumerable().Select(s => new
            {
                Id = s["Id"],
                ConNumber = s["ConNumber"],
                ConrName = s["ConrName"],
                CustName = s["CustNames"],
                ConFee = s["ConFee"],
                ConIsFeeFinished = s["ConIsFeeFinished"],
                FeeMoney = s["FeeMoney"],
                InvoiceMoney = s["InvoiceMoney"],
                NoFeeMoney = Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["FeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["FeeMoney"])),
            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = data
            });
        }
        #endregion


        #region 列表
        [Description("收款合同列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInfo")));
            return View();
        }

        [Description("收款合同列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInfo_SJ")));
            return View();
        }

        [Description("收款合同列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInfo_GC")));
            return View();
        }

        [Description("收款合同列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherContractInfo_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json(Bussiness.Controllers.BussContractController.TempCondition condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }

            int ConTypeID = TypeHelper.parseInt(Request.QueryString["ConTypeID"]);
            PageModel.SelectCondtion.Add("ConTypeID", ConTypeID);
            if (Request.Params["Filter"] != null && Request.Params["Filter"].ToString() != "")
            {
                PageModel.TextCondtion = Request.Params["Filter"].ToString();
            }
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                PageModel.TextCondtion = KeyJQSearch;
            }

            List<string> result = null;
            if (ConTypeID > 0)
            {
                result = PermissionList("OtherContracSubtInfo");
            }
            else
            {
                result = PermissionList("OtherContractInfo");
            }


            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }

            DataTable dt = op.GetContractOtherList(PageModel);
            var data = dt.AsEnumerable().Select(s => new
            {
                Id = s["Id"],
                ConNumber = s["ConNumber"],
                ConrName = s["ConrName"],
                CustName = s["CustName"],
                CustNames = s["CustNames"],
                ConFee = s["ConFee"],
                ConrBalanceFee = s["ConrBalanceFee"],
                ConIsFeeFinished = s["ConIsFeeFinished"].ToString() == "True" ? "已结清" : "未结清",
                SumFeeMoney = s["SumFeeMoney"],
                ConOtherFulfilType = s["ConOtherFulfilType"],
                SumInvoiceMoney = s["SumInvoiceMoney"],
                NoFeeMoney = Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])),
                NoConrBalanceFeeMoney = Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])),
            });


            if (Request.QueryString["Footer"] != null)
            {
                string row = JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = data,
                    footer = ShowFooter(PageModel)
                });
                return row;
            }
            else
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = data
                });
            }
        }
        #endregion

        private object ShowFooter(Common.SqlPageInfo condition)
        {
            condition.PageIndex = 0;
            condition.ToPageData = true;

            DataTable dt = op.GetContractOtherList(condition);
            var data = dt.AsEnumerable().Select(s => new
            {
                Id = s["Id"],
                ConNumber = s["ConNumber"],
                ConrName = s["ConrName"],
                CustName = s["CustName"],
                CustNames = s["CustNames"],
                ConFee = s["ConFee"],
                ConrBalanceFee = s["ConrBalanceFee"],
                ConIsFeeFinished = s["ConIsFeeFinished"].ToString() == "True" ? "已结清" : "未结清",
                SumFeeMoney = s["SumFeeMoney"],
                ConOtherFulfilType = s["ConOtherFulfilType"],
                SumInvoiceMoney = s["SumInvoiceMoney"],
                NoFeeMoney = Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])),
                NoConrBalanceFeeMoney = Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])),
                NoFeeMoneyInfo = TypeHelper.parseInt(s["ConOtherFulfilType"]) == (int)DataModel.ConDealWays.开口 ? Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])) : Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(s["ConrBalanceFee"]) - Common.ExtensionMethods.Value<decimal>(s["SumFeeMoney"])),

            });

            List<object> list = new List<object>();
            object a = new
            {
                Footer = 1,
                ConFee = data.Sum(p => Common.ExtensionMethods.Value<decimal>(p.ConFee)),
                SumFeeMoney = data.Sum(p => Common.ExtensionMethods.Value<decimal>(p.SumFeeMoney)),
                NoFeeMoney= data.Sum(p => Common.ExtensionMethods.Value<decimal>(p.NoFeeMoney)),
                NoFeeMoneyInfo = data.Sum(p => Common.ExtensionMethods.Value<decimal>(p.NoFeeMoneyInfo)),
                SumInvoiceMoney = data.Sum(p => Common.ExtensionMethods.Value<decimal>(p.SumInvoiceMoney)),
                CustName = "合计："
            };
            list.Add(a);
            return list;

        }

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            int ConTypeID = TypeHelper.parseInt(Request.QueryString["ConTypeID"]);
            var model = new BussContractOther();
            model.ConTypeID = ConTypeID;
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpId = userInfo.EmpID;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepName = userInfo.EmpDepName;
            ViewBag.permission = "['add','submit']";

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
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


        #region 添加
        [Description("添加")]
        public ActionResult addSub()
        {
            int ConTypeID = TypeHelper.parseInt(Request.QueryString["ConTypeID"]);
            var model = new BussContractOther();
            model.ConTypeID = ConTypeID;
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpId = userInfo.EmpID;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepName = userInfo.EmpDepName;
            ViewBag.permission = "['add','submit']";


            string companyType = Request.Params["CompanyType"].ToString();
            string page = "infoSub";
            switch (companyType)
            {
                case "SJ":
                    page = "infoSub_sj";
                    break;
                case "GC":
                    page = "infoSub_gc";
                    break;
                case "CJ":
                    page = "infoSub_cj";
                    break;
                default:
                    page = "infoSub";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult editSub(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "infoSub";
            switch (companyType)
            {
                case "SJ":
                    page = "infoSub_sj";
                    break;
                case "GC":
                    page = "infoSub_gc";
                    break;
                case "CJ":
                    page = "infoSub_cj";
                    break;
                default:
                    page = "infoSub";
                    break;
            }

            return View(page, model);
        }
        #endregion


        #region 查看
        [Description("查看")]
        public ActionResult editConOtherFee(int id)
        {
            var model = op.Get(id);

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
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

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
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
            var model = new BussContractOther();
            if (Id > 0)
            {
                model = op.Get(Id);
            }

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

            model.MvcSetValue();

            model.CustID = TypeHelper.parseInt(Request.Form["CustID"]);
            //model.ConID = TypeHelper.parseInt(Request.Form["SelectCnId"]);
            model.ConID = TypeHelper.parseInt(Request.Form["cn"]);


            #region 客户单位保存
            BussCustomer customer = new DBSql.Bussiness.BussCustomer().Get(model.CustID);
            if (customer == null)
            {
                DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                addModel.CustName = Request.Form["CustID"];
                if (model.ConTypeID == 1)
                {
                    addModel.TypeID = 1;
                }
                addModel.CustLinkMan = "";
                addModel.CustLinkTel = "";
                addModel.CustLinkMail = "";
                addModel.CustBankName = Request.Form["CustBankName"];
                addModel.CustBankAccount = Request.Form["CustBankAccount"];
                addModel.EmpModel = userInfo;
                int _custId = addModel.AddCust();
                model.CustID = _custId > 0 ? _custId : 0;
                if (model.CustID == 0)
                {
                    model.CustName = "";
                }
                else
                {
                    model.CustName = addModel.CustName;
                }
            }
            else
            {
                DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                addModel.CustName = customer.CustName;
                addModel.CustLinkMan = "";
                addModel.CustLinkTel = "";
                addModel.CustLinkMail = "";
                addModel.CustBankName = Request.Form["CustBankName"];
                addModel.CustBankAccount = Request.Form["CustBankAccount"];
                addModel.EmpModel = userInfo;
                int _custId = addModel.AddCust();
                model.CustID = _custId > 0 ? _custId : 0;
                if (model.CustID == 0)
                {
                    model.CustName = "";
                }
                else
                {
                    model.CustName = addModel.CustName;
                }

                model.CustName = customer.CustName;
            }
            #endregion

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                //扩展赋值
                model.CreatorEmpName = userInfo.EmpName;
                model.CreationTime = System.DateTime.Now;
                //赋值默认值
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion



        #region 其它合同收款
        [Description("收款")]
        public ActionResult AddConOtherFee(int id)
        {
            var model = op.Get(id);
            if (model.ConTypeID == 0)
            {
                ViewBag.Fee = model.ConOtherFulfilType == (int)DataModel.ConDealWays.开口 ? model.ConFee : model.ConrBalanceFee;
                return View("infoAddConOtherFee", model);
            }
            else
            {
                return View("infoAddConOtherSubFee", model);
            }
        }
        #endregion
        #region 其它合同收款 是否置为收清
        [Description("是否置为收清")]
        [HttpPost]
        public ActionResult editIsFeeFinished()
        {
            var Id = Common.ModelConvert.ConvertToDefault<int>(Request.Form["Id"]);
            var isValue = Common.ModelConvert.ConvertToDefault<bool>(Request.Form["isValue"]);

            var model = new BussContractOther();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            //model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.ConIsFeeFinished = isValue;
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
            }
            //else
            //{
            //    op.Add(model);
            //    op.UnitOfWork.SaveChanges();
            //    reuslt = model.Id;
            //}
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        public JsonResult GetOtherFeeJson(string otherIDs)
        {
            List<DataModel.Models.BussContractOther> _list = new List<BussContractOther>();
            if (otherIDs == null)
            {
                _list.Add(new DataModel.Models.BussContractOther() { Id = -1, ConrName = "不指定合同" });
                return Json(_list.Select(p => new { id = p.Id.ToString(), text = p.ConrName }));
            }
            int[] Id = (from n in otherIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.BussContractOther>.True();
            TWhere = TWhere.And(p => Id.Contains(p.Id));
            if (Id.Count() > 0)
            {
                List<DataModel.Models.BussContractOther> list = new DBSql.Bussiness.BussContractOther().GetList(TWhere).ToList();
                //DataModel.Models.Project _a = new DataModel.Models.Project(){;
                list.Add(new DataModel.Models.BussContractOther() { Id = -1, ConrName = "不指定合同" });
                var Newlist = list.Select(p => new { id = p.Id.ToString(), text = p.ConrName }).OrderBy(p => p.id).ToList();
                return Json(Newlist);
            }
            else
            {
                _list.Add(new DataModel.Models.BussContractOther() { Id = -1, ConrName = "不指定合同" });
                return Json(_list.Select(p => new { id = p.Id.ToString(), text = p.ConrName }));
            }
        }
    }
}
