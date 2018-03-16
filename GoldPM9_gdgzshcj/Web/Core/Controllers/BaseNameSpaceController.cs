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
[Description("BaseNameSpace")]
public class BaseNameSpaceController : CoreController
    {
        private DBSql.Sys.BaseNameSpace op = new DBSql.Sys.BaseNameSpace();
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
                PageModel.SelectOrder = "TableNames desc";
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
        public ActionResult save(int TableNames)
        {
            DoResult dr =  DoResultHelper.doSuccess("操作成功") ;
            return Json(dr);
        } 
        #endregion
    }
}
