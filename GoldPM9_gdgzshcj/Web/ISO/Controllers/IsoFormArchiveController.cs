using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    /// <summary>
    /// 设计输入评审单（共用IsoForm）
    /// </summary>
    public class IsoFormArchiveController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjDelegate")));
            ViewBag.EmpId = userInfo.EmpID;
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
                PageModel.SelectOrder = "FormID desc";
            }

            var Thwere = QueryBuild<DataModel.Models.IsoForm>.True();

            Thwere = Thwere.And(p => p.RefTable == "IsoFormArchive");
            //var list = op.GetPagedDynamic(PageModel,Thwere).ToList();
            var td = op.GetPagedList(Thwere, PageModel).ToList();
            var list = (from p in td
                        join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoFormArchive") on p.FormID equals t1.FlowRefID into nodes
                        from temp in nodes.DefaultIfEmpty()
                        select new
                        {
                            p.FormID,
                            p.FormDate,
                            p.FormName,
                            CreationTime = p.CreationTime.ToString("yyyy-MM-dd"),//登记日期
                            p.CreatorEmpName,//登记人
                            p.CreatorEmpId,
                            p.FormReason,  //申请原因
                            p.FormNote,  //备注
                            p.FlowTime,
                            ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                            ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                            ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                            ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,
                            //FlowStatus = GetFlowStatusId(p.FormID, p.CreatorEmpName)
                            FlowID = temp == null ? 0 : temp.Id,
                            FlowName = temp == null ? "" : temp.FlowName,
                            FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                            FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                            FlowXml = temp == null ? "" : temp.FlowXml
                        }).Select(m => new { m.FormID, m.CreationTime, m.CreatorEmpName, m.CreatorEmpId, m.FormReason, m.FormNote, m.ProjName, m.ProjNumber, m.ProjPhaseIds, m.ProjEmpName, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, m.FlowTime,m.FormDate,m.FormName,FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = "['NoEdit']";
            var model = new IsoForm();
            ViewBag.CreatorEmpId = userInfo.EmpID;
            model.FormDate = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.ColAttVal1 = GetRequestValue<string>("Ids");
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['NoEdit']";
            if (model.FlowTime.Year > 1900 && model.CreatorEmpId == userInfo.EmpID)
            {
                ViewBag.permission = "['a']";
            }
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.FormID));
            op.UnitOfWork.SaveChanges();
            new DBSql.PubFlow.Flow().Delete(id, "IsoFormArchive");
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FormID)
        {
            var model = new IsoForm();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            int reuslt = 0;
            if (model.FormID > 0)
            {
                op.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
            }
            else
            {
                model.RefTable = "IsoFormArchive";
                model.FormName = "档案申请表";
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}