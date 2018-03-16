using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System;
using System.Data;
using DataModel;

namespace Project.Controllers
{
    [Description("ProjReport")]
    public class ProjReportController : CoreController
    {
        private DBSql.Project.Project op = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjReportList")));

            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ProjReportList");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            var projectType = 0;

            projectType = TypeHelper.parseInt(Request.Form["ProjectType"]);

            #region
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "ProjVoltID")
                    {
                        PageModel.SelectCondtion.Add("ProjVoltID", _child.Value);
                    }
                    else if (_child.Key == "ProjectType")
                    {
                        projectType = TypeHelper.parseInt(_child.Value);
                    }
                    else if (_child.Key == "ProjectPhase")
                    {
                        PageModel.SelectCondtion.Add("ProjectPhase", _child.Value);
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
            #endregion

            var dt = new DataTable();

            switch (projectType)
            {
                case (int)ProjectType.主网类别:
                    {
                        var _model = baseDataDB.Get((int)ProjectType.主网类别);
                        dt = op.GetProjReportByMajorList(PageModel, _model.BaseOrder);
                    }
                    break;

                case (int)ProjectType.配电类别:
                    {
                        var _model = baseDataDB.Get((int)ProjectType.配电类别);
                        dt = op.GetProjReportByDistributionList(PageModel, _model.BaseOrder);
                    }
                    break;

                case (int)ProjectType.规划可研类:
                    {
                        var _model = baseDataDB.Get((int)ProjectType.规划可研类);
                        dt = op.GetProjReportByPlanningList(PageModel, _model.BaseOrder);
                    }
                    break;
                default:
                    {
                        var _model = baseDataDB.Get((int)ProjectType.其他类);
                        dt = op.GetProjReportByQTList(PageModel, _model.BaseOrder);
                    }
                    break;

            }
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        #endregion



    }
}
