using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Bussiness.Controllers
{
    public partial class BussProjInfoRecordsMobileController : MobileController
    {
        #region 列表json
        [Description("获取项目备案信息")]
        [HttpPost]
        public string json()
        {
            string type = Request.Params["CompanyType"].ToString();

            List<string> result = new List<string>();
            // emp  个人    // dep 部门    // allview  全部   // alledit  维护
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussProjInfoRecords>.True().And(m => m.DeleterEmpId == 0);
            string refTable = "BussProjInfoRecords";
            switch (type)
            {
                case "GC":
                    result = PermissionList("ProjInfoArch_GC");
                    TWhere = TWhere.And(p => p.CompanyId == 2);
                    refTable = "BussProjInfoRecords_GC";
                    break;
                case "SJ":
                    result = PermissionList("ProjInfoArch_SJ");
                    TWhere = TWhere.And(p => p.CompanyId == 1);
                    refTable = "BussProjInfoRecords_SJ";
                    break;
                case "CJ":
                    result = PermissionList("ProjInfoArch_CJ");
                    TWhere = TWhere.And(p => p.CompanyId == 3);
                    refTable = "BussProjInfoRecords_CJ";
                    break;
                default:
                    result = PermissionList("ProjInfoArch");
                    TWhere = TWhere.And(p => p.CompanyId == 0);
                    refTable = "BussProjInfoRecords";
                    break;
            }


            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result.Contains("dep"))
            {
                TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }

            var dataResult = (from t in op.GetPagedList(TWhere, PageModel)
                              join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == refTable) on t.Id equals t1.FlowRefID into nodes
                              from temp in nodes.DefaultIfEmpty()
                              select new
                              {
                                  t.Id,
                                  t.ProjCode,
                                  t.ProjName,
                                  RecordsEmpId = t.FK_BussProjInfoRecords_RecordsEmpId == null ? 0 : t.FK_BussProjInfoRecords_RecordsEmpId.EmpID,
                                  RecordsEmpName = t.FK_BussProjInfoRecords_RecordsEmpId == null ? "" : t.FK_BussProjInfoRecords_RecordsEmpId.EmpName,
                                  t.RecordTime,
                                  t.PredictStartTime,
                                  t.PredictConcludeTime,
                                  CustomerID = t.FK_BussProjInfoRecords_CustID == null ? 0 : t.FK_BussProjInfoRecords_CustID.Id,
                                  CustomerName = t.FK_BussProjInfoRecords_CustID == null ? "" : t.FK_BussProjInfoRecords_CustID.CustName,
                                  t.InfoSource,
                                  t.ProjSummarize,
                                  t.CreatorEmpId,
                                  t.CreatorEmpName,
                                  FlowID = temp == null ? 0 : temp.Id,
                                  FlowName = temp == null ? "" : temp.FlowName,
                                  FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                  FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                  FlowXml = temp == null ? "" : temp.FlowXml
                              }).ToList().Select(m => new { m.Id, m.ProjCode, m.ProjName, m.RecordsEmpId, m.RecordsEmpName, m.RecordTime, m.PredictStartTime, m.PredictConcludeTime, m.CustomerID, m.CustomerName, m.InfoSource, m.ProjSummarize, m.CreatorEmpId, m.CreatorEmpName, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dataResult
            });
        }
        #endregion

        #region 添加
        [Description("添加项目备案信息")]
        public ActionResult add()
        {
            string type = Request.Params["CompanyType"].ToString();

            var model = new BussProjInfoRecords();
            model.RecordTime = DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            model.RecordsEmpId = userInfo.EmpID;
            ViewBag.RecordsName = 0;
            string page = "";

            switch (type)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewData["RecordsEmpId1"] = model.RecordsEmpId.ToString();
            ViewData["RecordsEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.RecordsEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.RecordsEmpId).EmpDepID.ToString();
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            var recordsName = dbBaseEmployee.GetQuery(x => x.EmpID == model.RecordsEmpId).FirstOrDefault();
            ViewBag.RecordsName = recordsName.EmpName;

            string companyType = Request.Params["CompanyType"].ToString();

            // 备案时间
            if (model.RecordTime != null && model.RecordTime.ToString().Length > 0)
            {
                ViewBag.RecordTime = model.RecordTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.RecordTime = "";
            }

            // 预计启动时间
            if (model.PredictStartTime != null && model.PredictStartTime.ToString().Length > 0)
            {
                ViewBag.PredictStartTime = model.PredictStartTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.PredictStartTime = "";
            }

            // 预计签合同时间
            if (model.PredictConcludeTime != null && model.PredictConcludeTime.ToString().Length > 0)
            {
                ViewBag.PredictConcludeTime = model.PredictConcludeTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.PredictConcludeTime = "";
            }

            string page = "";

            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion
    }
}
