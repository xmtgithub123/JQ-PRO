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
    [Description("IsoStatistics")]
    public class IsoStatisticsController : CoreController
    {
        private DBSql.Iso.IsoCheck op = new DBSql.Iso.IsoCheck();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

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
                if (_child.Key == "CheckItem")
                {
                    queryContext.TextCondtion = _child.Value;
                }
                else if (_child.Key == "PhaseID")
                {
                    queryContext.SelectCondtion.Add("PhaseID", _child.Value);
                }
                else if (_child.Key == "SpecialID")
                {
                    queryContext.SelectCondtion.Add("SpecialID", _child.Value);
                }
                else if (_child.Key == "CheckType")
                {
                    queryContext.SelectCondtion.Add("CheckType", _child.Value);
                }
            }
            using (var dt = op.GetSummaryList(queryContext))
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = queryContext.PageTotleRowCount,
                    rows = dt
                });
            }

            //int pageIndex = queryContext.PageIndex;
            //int pageSize = queryContext.PageSize;

            //DataTable table = SplitDataTable(dt, pageIndex, pageSize);
        }

        public string jsonList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectOrder = "c.Id desc";
            queryContext.SelectCondtion.Add("PhaseID", TypeHelper.parseInt(Request.Params["PhaseID"]));
            queryContext.SelectCondtion.Add("SpecialID", TypeHelper.parseInt(Request.Params["SpecialID"]));
            queryContext.SelectCondtion.Add("CheckType", TypeHelper.parseInt(Request.Params["CheckType"]));
            queryContext.SelectCondtion.Add("ModelIsoCheckID", TypeHelper.parseInt(Request.Params["IsoCheckModelId"]));
            //new DBSql.
            using (var dt = op.GetDetailISOCheckList(queryContext))
            {
                //int pageIndex = queryContext.PageIndex;
                //int pageSize = queryContext.PageSize;
                //DataTable table = SplitDataTable(dt, pageIndex, pageSize);
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = queryContext.PageTotleRowCount,
                    rows = dt
                });
            }
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

        #region 编辑
        [Description("编辑")]
        public ActionResult info()
        {
            ViewBag.PhaseID = TypeHelper.parseInt(Request.Params["PhaseID"]);
            ViewBag.SpecialID = TypeHelper.parseInt(Request.Params["SpecialID"]);
            ViewBag.CheckType = TypeHelper.parseInt(Request.Params["CheckType"]);
            ViewBag.IsoCheckModelId = TypeHelper.parseInt(Request.Params["IsoCheckModelId"]);

            return View("info");
        }
        #endregion

        #region
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            IsoCheck t = new IsoCheck();
            return View();
        }
        #endregion      

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            return View();
        }
        #endregion       

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            //op.Delete(s => id.Contains(s.FileID));
            op.UnitOfWork.SaveChanges();
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
            //op.UnitOfWork.SaveChanges();
            reuslt = model.FileID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FileID, "操作成功") : DoResultHelper.doSuccess(model.FileID, "操作失败");
            return Json(dr);
        }
        #endregion
        #endregion
    }
}
