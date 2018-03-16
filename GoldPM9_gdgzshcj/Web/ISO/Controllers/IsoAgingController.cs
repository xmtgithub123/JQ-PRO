using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace Iso.Controllers
{
    [Description("IsoAging")]
    public class IsoAgingController : CoreController
    {
        private DBSql.Design.DesTask op = new DBSql.Design.DesTask();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        #region Aging的json
        [Description("列表AgingJson")]
        [HttpPost]
        public string AgingJson()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("TimeAnalysis")));

            List<string> result = PermissionList("TimeAnalysis");

            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            if (null != queryInfo)
            {
                List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "TextCondtion")
                    {
                        queryContext.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "DeptId")
                    {
                        queryContext.SelectCondtion.Add("QueryDeptId", _child.Value);
                    }
                }
            }

            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                queryContext.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                queryContext.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                queryContext.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }

            DataTable dt = op.GetAgingAnalysisList(queryContext);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 列表
        [Description("列表")]
        public ActionResult infoList()
        {
            return View("infoList");
        }
        #endregion


        [Description("列表json")]
        [HttpPost]
        public string GeTaskListJson()
        {


            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectOrder = "d.Id desc";
            var queryInfo = Request.Form["queryInfo"];
            queryContext.SelectCondtion.Add("DesTaskIds", Request.Form["DesTaskIds"]);
            if (null != queryInfo)
            {
                List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "TextCondtion")
                    {
                        queryContext.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "TaskPhaseId")
                    {
                        queryContext.SelectCondtion.Add("TaskPhaseId", _child.Value);
                    }
                }
            }


            var dt = op.GetDesTaskList(queryContext);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }
    }
}
