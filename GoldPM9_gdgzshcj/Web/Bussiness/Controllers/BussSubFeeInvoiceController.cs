using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Bussiness.Controllers
{
[Description("BussSubFeeInvoice")]
public class BussSubFeeInvoiceController : CoreController
    {
        private DBSql.Bussiness.BussSubFeeInvoice op = new DBSql.Bussiness.BussSubFeeInvoice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Iso.IsoForm IsoFormDB = new DBSql.Iso.IsoForm();

        #region 列表
        [Description("付款开票信息列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee")));
            return View();
        }

        [Description("付款开票信息列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee_SJ")));
            return View();
        }

        [Description("付款开票信息列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubFee_GC")));
            return View();
        }

        [Description("付款开票信息列表(创景公司)")]
        public ActionResult list_cj()
        {
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
                PageModel.SelectOrder = "Id desc";
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
            var model = new BussSubFeeInvoice();
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
            int reuslt = 0;
            try
            {
                IsoFormDB.UpdateIsoFormSubInvoiceFactList(id, this.userInfo);
                IsoFormDB.UnitOfWork.SaveChanges();

        
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
            var model = new BussSubFeeInvoice();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.Id > 0)
            {
				op.Edit(model);
            }
            else
            {
                op.Add(model);
				reuslt = model.Id ;
            }
			op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        public string GetJsonList()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "id desc";
            }
            int FormTableID = TypeHelper.parseInt(Request.Form["FormTableID"]);

            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectCondtion.Add("FormTable", "MustTable");
            PageModel.SelectOrder = "FormTableID desc,subFeeInvoiceDate desc";
            PageModel.SelectCondtion.Add("FormTableID", FormTableID);
            DataTable dt = op.GetSubFeeInvoiceList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        public string JsonSubInvoice()
        {        

            List<string> result = PermissionList("ContractSubFee");

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectCondtion.Add("FormTable", "MustTable");
            PageModel.SelectOrder = "FormTableID desc,subFeeInvoiceDate desc";

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

            DataTable dt = op.GetSubFeeInvoiceList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
    }
}
