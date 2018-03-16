using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Core.Controllers
{
    [Description("V_SystemTableFKey")]
    public class V_SystemTableFKeyController : CoreController
    {
        private DBSql.PubFunction.V_SystemTableFKey op = new DBSql.PubFunction.V_SystemTableFKey();
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
                PageModel.SelectOrder = "FKName desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FKName)
        {
            var model = new V_SystemTableFKey();
            int reuslt = 0;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FKName, "操作成功") : DoResultHelper.doSuccess(model.FKName, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
