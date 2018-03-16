using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;
using System.Data;
using Common;
using DBSql;

namespace Bussiness.Controllers
{
    [Description("BussProjInfoRecords")]
    public class BussProjInfoRecordsController : CoreController
    {
        private DBSql.Bussiness.BussProjInfoRecords op = new DBSql.Bussiness.BussProjInfoRecords();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();

        #region 列表
        [Description("项目信息备案(分公司)列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoArch")));
            return View();
        }

        [Description("项目信息备案(工程公司)列表")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoArch_GC")));
            return View();
        }

        [Description("项目信息备案(设计公司)列表")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoArch_SJ")));
            return View();
        }

        [Description("项目信息备案(创景公司)列表")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoArch_CJ")));
            return View();
        }

        #endregion

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

            string companyType = Request.Params["CompanyType"].ToString();

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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateBussProjInfoRecordsList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "BussProjInfoRecords");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BusinessInfoRecord");
                }
                reuslt = 1;
            }
            catch (Exception)
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussProjInfoRecords();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            var _RecordsEmpId = Request.Form["RecordsEmpId"];
            model.RecordsEmpId = TypeHelper.parseInt(_RecordsEmpId.Split('-')[0]);
            model.CustID = TypeHelper.parseInt(Request.Form["cg"]);
            model.InfoSource = Request.Form["InfoSource"];
            model.ProjSummarize = Request.Form["ProjSummarize"];

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    model.CompanyId = 1;
                    break;
                case "GC":
                    model.CompanyId = 2;
                    break;
                case "CJ":
                    model.CompanyId = 3;
                    break;
                default:
                    model.CompanyId = 0;
                    break;
            }

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                //model.State = 1;
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);

        }
        #endregion

        [Description("禁用备案信息")]
        [HttpPost]
        public ActionResult inactive(int id, int isDel)
        {
            var model = op.Get(id);
            //model.State = isDel;
            int reuslt = op.UpdateBussProjInfoRecordsInfo(model);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }

        public int GetProjCodeCount(string ProjCode, int Id)
        {
            int Count = op.GetProjCodeCount(ProjCode, Id);
            return Count;
        }

        #region 记事

        [Description("记事列表")]
        public ActionResult notelist()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;

            return View();
        }

        [Description("列表json")]
        [HttpPost]
        public string notejson()
        {
            int BussProjInfoRecordsId = string.IsNullOrEmpty(Request["BussProjInfoRecordsId"]) ? 0 : Convert.ToInt32(Request["BussProjInfoRecordsId"]);
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            BussProjNoteData noteBusiness = new BussProjNoteData();
            DataTable list = noteBusiness.GetNoteList(PageModel.PageSize, PageModel.PageIndex, PageModel.PredicateValue != null ? PageModel.PredicateValue[0].ToString() : "", "Id DESC", BussProjInfoRecordsId, out recordcount);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }

        [Description("添加记事")]
        public ActionResult noteadd()
        {
            var model = new DataModel.Models.ProjIRNote();
            return View("noteinfo", model);
        }

        [Description("编辑记事")]
        public ActionResult noteedit(int id)
        {
            BussProjNoteData noteBusiness = new BussProjNoteData();
            DataModel.Models.ProjIRNote model = DTHelper.GetEntity<DataModel.Models.ProjIRNote>(noteBusiness.GetNote(id));
            return View("noteinfo", model);
        }

        [Description("删除记事")]
        public ActionResult notedel(int[] id)
        {
            BussProjNoteData noteBusiness = new BussProjNoteData();
            int reuslt = noteBusiness.DeleteNote(id);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        [Description("保存")]
        [HttpPost]
        public ActionResult notesave(int Id)
        {
            BussProjNoteData noteBusiness = new BussProjNoteData();
            ProjIRNote model = new ProjIRNote();

            if (Id > 0)
            {
                model = DTHelper.GetEntity<ProjIRNote>(noteBusiness.GetNote(Id));
            }
            model.MvcSetValue();

            model.CreateId = userInfo.EmpID;
            model.CreateName = userInfo.EmpName;

            int reuslt = 0;
            if (Id > 0)
            {
                reuslt = noteBusiness.UpdateNote(model);
            }
            else
            {
                reuslt = model.Id = noteBusiness.InsertNote(model);
            }

            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion
    }
}
