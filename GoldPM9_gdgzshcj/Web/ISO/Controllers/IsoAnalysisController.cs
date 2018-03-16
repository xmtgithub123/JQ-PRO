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
    [Description("IsoAnalysis")]
    public class IsoAnalysisController : CoreController
    {
        private DBSql.Iso.IsoCheck op = new DBSql.Iso.IsoCheck();
        private DBSql.Design.DesTask DesTaskDB = new DBSql.Design.DesTask();

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
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectOrder = "c.Id desc";
            var queryInfo = Request.Form["queryInfo"];

            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            foreach (Common.Data.Extenstions.Filter item in Filter)
            {
                Common.Data.Extenstions.FilterChilde _child = item.list[0];
                if (_child.Key == "CheckNote")
                {
                    queryContext.TextCondtion = _child.Value;
                }
                else if (_child.Key == "IsExtract")
                {
                    queryContext.SelectCondtion.Add("IsExtract", _child.Value);
                }
                else if (_child.Key == "PhaseID")
                {
                    queryContext.SelectCondtion.Add("PhaseID", _child.Value);
                }
                else if (_child.Key == "SpecialID")
                {
                    queryContext.SelectCondtion.Add("SpecialID", _child.Value);
                }
                else if (_child.Key == "CheckErrTypeID")
                {
                    queryContext.SelectCondtion.Add("CheckErrTypeID", _child.Value);
                }
            }
            var dt = op.GetDetailsList(queryContext);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }



        public DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoFile();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            //op.Delete(s => id.Contains(s.FileID));
            //op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FileID)
        {
            var model = new DataModel.Models.IsoFile();
            //if (FileID > 0)
            //{
            //    model = op.Get(FileID);
            //}
            //model.MvcSetValue();

            int reuslt = 0;
            //if (model.FileID > 0)
            //{
            //    op.Edit(model);
            //}
            //else
            //{
            //    op.Add(model);
            //}
            op.UnitOfWork.SaveChanges();
            reuslt = model.FileID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FileID, "操作成功") : DoResultHelper.doSuccess(model.FileID, "操作失败");
            return Json(dr);
        }
        #endregion

        #region EmployeeAnalysis的json
        [Description("列表")]
        public ActionResult EmployeeAnalysis()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("QualityAnalysis")));
            return View("EmployeeAnalysis");
        }
        #endregion

        #region EmployeeAnalysis的json
        [Description("列表EmployeeAnalysisJson")]
        [HttpPost]
        public string EmployeeAnalysisJSON()
        {
            List<string> result = PermissionList("QualityAnalysis");

            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            if (null != queryInfo)
            {
                List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "StatisticsPeriodType")
                    {
                        queryContext.SelectCondtion.Add("StatisticsPeriodType", _child.Value);
                    }
                    else if (_child.Key == "DeptId")
                    {
                        queryContext.SelectCondtion.Add("QueryDeptId", _child.Value);
                    }
                }
            }

            if (!(result.Contains("allview")|| result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    queryContext.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    queryContext.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }
            
            DataTable dt = DesTaskDB.GetEmployeeAnalysisList(queryContext);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region DeptAnalysis
        [Description("列表")]
        public ActionResult DeptAnalysis()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("QualityAnalysis")));
            return View("DeptAnalysis");
        }
        #endregion

        #region DeptAnalysis的json
        [Description("列表EmployeeAnalysisJson")]
        [HttpPost]
        public string DeptAnalysisJSON()
        {
            List<string> result = PermissionList("QualityAnalysis");

            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            if (null != queryInfo)
            {
                List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "StatisticsPeriodType")
                    {
                        queryContext.SelectCondtion.Add("StatisticsPeriodType", _child.Value);
                    }
                }
            }

            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    queryContext.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    queryContext.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }


            DataTable dt = DesTaskDB.GetDeptAnalysisList(queryContext);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        public ActionResult InfoList(string Ids)
        {
            ViewBag.Ids = Ids;
            return View("InfoList");
        }


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
                    else if (_child.Key == "TaskJudge")
                    {
                        queryContext.SelectCondtion.Add("TaskJudgeId", _child.Value);
                    }
                    else if (_child.Key == "TaskPhaseId")
                    {
                        queryContext.SelectCondtion.Add("TaskPhaseId", _child.Value);
                    }
                }
            }
            var dt = DesTaskDB.GetDesTaskList(queryContext);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = dt
            });
        }
        public JsonResult setExtract()
        {
            var idSet = Request.Form["idSet"] ?? "";
            if (string.IsNullOrEmpty(idSet))
            {
                return Json(new { Result = false });
            }
            op.setExtract(idSet.Trim(','), this.userInfo);
            return Json(new { Result = true });
        }
    }
}
