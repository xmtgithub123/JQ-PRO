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
    
    [Description("PayBalanceChangeHist")]
    public class PayBalanceChangeHistController : CoreController
    {
        private DBSql.Pay.PayBalanceChangeHist op = new DBSql.Pay.PayBalanceChangeHist();
        private DBSql.Pay.PayBalanceUserAccount account = new DBSql.Pay.PayBalanceUserAccount();
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Pay.PayBalanceLot lot = new DBSql.Pay.PayBalanceLot();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
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
                PageModel.SelectOrder = "Id desc";
            }
            int payBalanceAcountId = TypeHelper.parseInt(Request.QueryString["payBalanceAcountId"]);
            var list = op.GetPagedList(p=>p.BalanceEmpAccountID==payBalanceAcountId,PageModel).ToList();
            var targetLsit = from item in list
                             let date=item.CreationTime.ToString("yyyy-MM-dd")
                             select new
                             {
                                 item.Id,
                                 item.BalanceOldMoney,//原来金额
                                 item.BalanceNewMoney,//新金额
                                 item.BalanceChangeNote,//备注信息
                                 date
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetLsit
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new PayBalanceChangeHist();
            return View("info", model);
        }
        #endregion

        #region 编辑历史结算信息
        [Description("编辑历史结算信息")]
        public ActionResult edit(int id)
        {
            var model = account.Get(id);
            ViewBag.Name = emp.Get(model.EmpId) == null ? "" : emp.Get(model.EmpId).EmpName;//姓名
            ViewBag.LotName = lot.Get(model.BalanceLotID) == null ? "" : lot.Get(model.BalanceLotID).BalanceLotName;//批次名称
            ViewBag.LotDate = lot.Get(model.BalanceLotID) == null ? "" : lot.Get(model.BalanceLotID).BalanceDate.ToString("yyyy-MM-dd");
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int id)
        {
            var model = new PayBalanceChangeHist();
            decimal NewMoney = TypeHelper.parseDecimal(Request.Form["BalanceMoney"]);
            string Reason = Request.Form["Reason"];
            try
            {
                model.BalanceChangeNote = Reason;
                model.MvcDefaultSave(userInfo.EmpID);
                int reuslt = 0;
                reuslt = op.PayChangeInfo(id, NewMoney, model);
                doResult = DoResultHelper.doSuccess(reuslt,"操作成功");
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return Json(doResult);
        }
        #endregion

    }
}
