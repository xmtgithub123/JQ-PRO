using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System ;
namespace Core.Controllers
{
    [Description("BaseRestDay")]
    public class BaseRestDayController : CoreController
    {
        private DBSql.Sys.BaseRestDay op = new DBSql.Sys.BaseRestDay();
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
                PageModel.SelectOrder = "BaseDayID desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData()
        {
            DateTime startTime;
            if (!DateTime.TryParse(Request.QueryString["startTime"], out startTime))
            {
                return Json("[]");
            }
            DateTime endTime;
            if (!DateTime.TryParse(Request.QueryString["endTime"], out endTime))
            {
                return Json("[]");
            }
            return Json(op.GetList(startTime, endTime), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save()
        {
            try
            {
                DateTime startTime, endTime;

                startTime = JQ.Util.TypeParse.parse<DateTime>(Request.QueryString["startTime"]);
                endTime = JQ.Util.TypeParse.parse<DateTime>(Request.QueryString["endTime"]);

                if ((endTime < startTime))
                {
                    throw new Common.JQException("开始时间不得小于结束时间");
                }
                var maxday = (endTime - startTime).Days  ;
                if (maxday == 0) maxday = 1;
                for (int i = 0; i < maxday; i++)
                {
                    var BaseDay = startTime.AddDays(i);
                    var reuslt = op.ResetBaseDay(BaseDay);
                }
                return Json(DoResultHelper.doSuccess("操作成功"));
            }
            catch (Common.JQException jqe)
            {
                return Json(DoResultHelper.doSuccess(jqe.Message, "操作失败"));
            }
            catch
            {
                return Json(DoResultHelper.doError("操作失败"));
            }
        }
        #endregion
    }
}
