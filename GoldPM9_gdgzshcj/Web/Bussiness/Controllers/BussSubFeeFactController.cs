using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Data;

namespace Bussiness.Controllers
{
    [Description("BussSubFeeFact")]
    public class BussSubFeeFactController : CoreController
    {
        private DBSql.Bussiness.BussSubFeeFact op = new DBSql.Bussiness.BussSubFeeFact();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Iso.IsoForm IsoFormDB = new DBSql.Iso.IsoForm();

        #region 列表
        [Description("付款合同费用列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee")));
            return View();
        }

        [Description("付款合同费用列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee_SJ")));
            return View();
        }

        [Description("付款合同费用列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee_GC")));
            return View();
        }

        [Description("付款合同费用列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee")));

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id  desc";
            }
            var list = op.GetPagedList(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussSubFeeFact();
            ViewBag.permission = "['add','submit']";

            string CompanyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (CompanyType)
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

            string CompanyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (CompanyType)
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
            int reuslt = 0;
            try
            {
                IsoFormDB.UpdateIsoFormSubFeeFactList(id, this.userInfo);
                IsoFormDB.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "SubFeeFact");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoForm");
                }
                reuslt = 1;
            }
            catch
            {
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussSubFeeFact();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = -1;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
                reuslt = model.Id;
            }
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetJsonList()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "id desc";
            }

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact");
                    break;
            }

            int FormTableID = TypeHelper.parseInt(Request.Form["SubFeeFactId"]);

            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectOrder = "SubFeeFactDate desc";
            PageModel.SelectCondtion.Add("SubFeeFactId", FormTableID);
            DataTable dt = op.GetSubFeeFactList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        public string JsonSubFact()
        {

            List<string> result = PermissionList("ContractSubFee");

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");

            string CompanyType = Request.Params["CompanyType"].ToString();
            switch (CompanyType)
            {
                case "SJ":
                    result = PermissionList("ContractSubFee_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_SJ");
                    break;
                case "GC":
                    result = PermissionList("ContractSubFee_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_GC");
                    break;
                case "CJ":
                    result = PermissionList("ContractSubFee_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact_CJ");
                    break;
                default:
                    result = PermissionList("ContractSubFee");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "SubFeeFact");
                    break;
            }


            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                }
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

            DataTable dt = op.GetSubFeeFactList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
    }
}
