using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;

namespace Pay.Controllers
{
    [Description("PayBalanceLot")]
    public class PayBalanceLotController : CoreController
    {
        private DBSql.Pay.PayBalanceLot op = new DBSql.Pay.PayBalanceLot();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Pay.PayBalanceEngineering engi = new DBSql.Pay.PayBalanceEngineering();
        #region 批次列表信息
        [Description("批次列表信息")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OldBalanceManage")));
            return View();
        }
        #endregion

        #region 批次列表JSon
        [Description("批次列表JSon")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(p=>p.Id>1,PageModel);
            var targetList = from item in list
                             select new
                             {
                                 item.Id,
                                 item.BalanceLotName,
                                 item.BalanceDate
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion

        [HttpPost]
        public JsonResult modelJson(FormCollection form)
        {
            int Id = TypeHelper.parseInt(form["Id"]);
            DataModel.Models.PayBalanceLot model = op.Get(Id);
            var target = new {
                model.Id,
                model.BalanceLotName,
                model.AllAmount,
                model.AllMoney,
                BalanceDate=model.BalanceDate.ToString("yyyy-MM-dd"),
                model.ManageAmount,
                model.ManageBase,
                model.MoneyPerAmount,
                model.TechAmount,
                model.TechEmpCount,
                Num=engi.GetWaitingBalanceProjectCount()

            };
            return Json(target);
        }
    }
}
