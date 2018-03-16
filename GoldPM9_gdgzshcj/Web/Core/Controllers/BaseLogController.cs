using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
namespace Core.Controllers
{
public class BaseLogController : CoreController
    {
        private DBSql.Sys.BaseLog op = new DBSql.Sys.BaseLog();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("审计日志列表")]
        public ActionResult list()
        {
            return View();
        }


        [Description("应用日志列表")]
        public ActionResult Busslist()
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
                PageModel.SelectOrder = "BaseLogID desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        } 
        #endregion
    }
}
