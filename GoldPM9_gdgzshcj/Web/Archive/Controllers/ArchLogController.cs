using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
namespace Archive.Controllers
{
public class ArchLogController : CoreController
    {
        private DBSql.Sys.BaseLog op = new DBSql.Sys.BaseLog();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("签名日志列表")]
        public ActionResult signlist()
        {
            ViewBag.RefTable = "ArchSign";
            return View();
        }

        [Description("项目日志列表")]
        public ActionResult projlist()
        {
            ViewBag.RefTable = "ArchElecProject";
            return View();
        }

        [Description("案卷日志列表")]
        public ActionResult eleclist()
        {
            ViewBag.RefTable = "ArchElec";
            return View();
        }

        [Description("文件日志列表")]
        public ActionResult filelist()
        {
            ViewBag.RefTable = "ArchElecFile";
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json(string RefTable)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "BaseLogID desc";
            }
            var list = op.GetPagedDynamic(PageModel,s => s.BaseLogRefTable == RefTable).ToList();
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        } 
        #endregion
    }
}
