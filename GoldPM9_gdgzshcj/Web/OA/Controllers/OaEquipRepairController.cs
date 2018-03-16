using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Oa.Controllers
{
[Description("OaEquipRepair")]
public class OaEquipRepairController : CoreController
    {
        private DBSql.Oa.OaEquipRepair op = new DBSql.Oa.OaEquipRepair();
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
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(PageModel).Select(p=>new {
                Id=p.Id,
                EquipName = p.FK_OaEquipRepair_EquipID.EquipName,
                RepairMeoney =p.RepairMeoney,
                RepairDate=p.RepairDate,
                RepairReportEmpId=p.RepairReportEmpId,
                RepairFlowEmpId=p.RepairFlowEmpId,
                RepairNote= p.RepairNote,
                FixDate = p.FixDate,
                EquipId = p.FK_OaEquipRepair_EquipID.Id
            }).ToList();


            var emp = new DBSql.Sys.BaseEmployee().GetList(p => p.EmpIsDeleted == false).Select(p => new {
                EmpId=p.EmpID,
                EmpName=p.EmpName
            });

            int EquipOrOffice = 0;
            int.TryParse(Request["EquipOrOffice"], out EquipOrOffice);
            var showList = from a in list
                           join b in emp on a.RepairReportEmpId equals b.EmpId
                           join c in emp on a.RepairFlowEmpId equals c.EmpId
                           join t1 in op.DbContext.Set<OaEquipment>() on a.EquipId equals t1.Id
                           where t1.EquipOrOffice == EquipOrOffice
                           select new
                           {
                               Id = a.Id,
                               EquipName = a.EquipName,
                               RepairMeoney = a.RepairMeoney,
                               RepairDate = a.RepairDate,
                               RepairReportEmpName =b.EmpName,
                               RepairFlowEmpName = c.EmpName,
                               RepairNote = a.RepairNote,
                               FixDate = a.FixDate
                           };
            
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
            var model = new OaEquipRepair();
            model.RepairReportEmpId = userInfo.EmpID;
            model.RepairDate = DateTime.Now;
            model.RepairFlowEmpId = 0;
            ViewBag.RepairReportDepartId = userInfo.EmpDepID.ToString();
            ViewBag.RepairFlowDepartId = 0;
            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.mName = new DBSql.Oa.OaEquipment().FirstOrDefault(p => p.Id == model.EquipID) == null ? "" : new DBSql.Oa.OaEquipment().FirstOrDefault(p => p.Id == model.EquipID).EquipName;
            var RepairReport = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(model.RepairReportEmpId);
            var RepairFlow = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(model.RepairFlowEmpId);
            ViewBag.RepairReportDepartId = RepairReport.EmpDepID;
            ViewBag.RepairFlowDepartId= RepairFlow.EmpDepID;
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.DbContext.Set<OaEquipRepair>().RemoveRange(op.DbContext.Set<OaEquipRepair>().Where(p => id.Contains(p.Id)));
            op.DbContext.Set<OaEquipStock>().RemoveRange(op.DbContext.Set<OaEquipStock>().Where(p => p.EquipFormType == "OaEquipStock" && id.Contains(p.EquipFormID)));
            reuslt=op.DbContext.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipRepair();
            var OaEquipStock = new OaEquipStock();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.RepairReportEmpId = TypeHelper.parseInt(Request.Form["RepairReportEmpId"].Split('-')[0]);
            model.RepairFlowEmpId = TypeHelper.parseInt(Request.Form["RepairFlowEmpId"].Split('-')[0]);
            int reuslt = 0;                      
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo);
				op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                op.Add(model);
                op.DbContext.SaveChanges();

                OaEquipStock.MvcDefaultSave(userInfo);
                OaEquipStock.EquipActionID = 4;
                OaEquipStock.EquipCount = 1;
                OaEquipStock.EquipFormID = model.Id;
                OaEquipStock.EquipDateTime = DateTime.Now;
                OaEquipStock.EquipFormType = "OaEquipRepair";
                OaEquipStock.EquipID = model.EquipID;
                OaEquipStock.EquipNote = "设备维修";
                op.DbContext.Set<OaEquipStock>().Add(OaEquipStock);
            }

            op.DbContext.SaveChanges();

            var ba = new DBSql.Sys.BaseAttach();
            ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);

            reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
