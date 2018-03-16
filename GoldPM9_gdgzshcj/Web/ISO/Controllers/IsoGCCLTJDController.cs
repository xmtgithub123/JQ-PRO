﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Iso.Controllers
{
[Description("IsoGCCLTJD")]
public class IsoGCCLTJDController : CoreController
    {
        private DBSql.Iso.IsoGCCLTJD op = new DBSql.Iso.IsoGCCLTJD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            ViewBag.permission=PermissionList("ProjectCenterList");
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
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoGCCLTJD") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjID,
                                p.ProjNumber,
                                p.ProjName,
                                p.ProjPhaseName,
                                p.CreationTime,
                                p.CreatorEmpName,
                                p.CreatorEmpId,
                                p.EngGCDD,
                                p.EngCGJFRQ,
                                p.CustLinkManName,
                                p.CustName,
                                p.EngSJR,
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new
                            {
                                m.Id,
                                m.Number,
                                m.ProjID,
                                m.ProjNumber,
                                m.ProjName,
                                m.ProjPhaseName,
                                m.CreationTime,
                                m.CreatorEmpName,
                                m.CreatorEmpId,
                                m.EngGCDD,
                                m.EngCGJFRQ,
                                m.CustLinkManName,
                                m.CustName,
                                m.EngSJR,
                                m.FlowID,
                                m.FlowName,
                                m.FlowStatusID,
                                m.FlowStatusName,
                                m.FlowXml,
                                FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml)
                            });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            var model = new IsoGCCLTJD();
            model.TableNumber = "SCX02-10";

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.CurrEmpID = model.CreatorEmpId;

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoGCCLTJD();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
				op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}