using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
namespace Oa.Controllers
{
[Description("OaEquipDeal")]
public class OaEquipDealController : CoreController
    {
        private DBSql.Oa.OaEquipDeal op = new DBSql.Oa.OaEquipDeal();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
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

            var Thwere = QueryBuild<DataModel.Models.OaEquipDeal>.True();
            List<string> permissionList = PermissionList("EquipDeal");
            if (!permissionList.Contains("dep") && !permissionList.Contains("allview"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
            }
            else
            {
                if (!permissionList.Contains("allview") && permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
                }
            }
            Thwere = Thwere.And(p => p.EquipCount > 0);
            Thwere = Thwere.And(p => p.EquipId> 0);
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(Thwere, PageModel).ToList();
            //var list = op.GetPagedDynamic(PageModel).ToList();

            int EquipOrOffice = 0;
            int.TryParse(Request["EquipOrOffice"], out EquipOrOffice);
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "EquipDeal") on p.Id equals t1.FlowRefID into node
                            join t2 in op.DbContext.Set<DataModel.Models.OaEquipment>() on p.EquipId equals t2.Id into nodes
                            from temp in nodes.DefaultIfEmpty()
                            from temp1 in node.DefaultIfEmpty()
                            where temp.EquipOrOffice == EquipOrOffice
                            select new
                            {
                                Id = p.Id,
                                EquipId = temp == null? "":temp.EquipName,//p.EquipId,
                                DealStatusId = new DBSql.Sys.BaseData().getBaseNameByIds(p.DealStatusId.ToString()) == null ? "0" : new DBSql.Sys.BaseData().getBaseNameByIds(p.DealStatusId.ToString()).Split(',')[0],
                                EquipCount = p.EquipCount,
                                EquipNote = p.EquipNote,
                                RepairReportEmpId = new DBSql.Sys.BaseEmployee().GetEmpName(p.RepairReportEmpId.ToString()),
                                DealDate = p.DealDate,
                                CreatorEmpName = p.CreatorEmpName,
                                p.CreatorEmpId,
                                FlowID = temp1 == null ? 0 : temp1.Id,
                                FlowName = temp1 == null ? "" : temp1.FlowName,
                                FlowStatusID = temp1 == null ? 0 : temp1.FlowStatusID,
                                FlowStatusName = temp1 == null ? "" : temp1.FlowStatusName,
                                FlowXml = temp1 == null ? "" : temp1.FlowXml
                            }).Select(m => new { 
                                m.Id,m.EquipId,
                                m.DealStatusId,
                                m.EquipCount,
                                m.EquipNote,
                                m.RepairReportEmpId,
                                m.DealDate ,
                                m.CreatorEmpName, 
                                m.CreatorEmpId, 
                                m.FlowID, 
                                m.FlowName, 
                                m.FlowStatusID, 
                                m.FlowStatusName, 
                                m.FlowXml, 
                                FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });
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
            var model = new OaEquipDeal();
            model.RepairReportEmpId = userInfo.EmpID;
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
            var RepairReport = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(model.RepairReportEmpId);
            ViewBag.RepairReportDepartId = RepairReport == null ? 0 : RepairReport.EmpDepID;
            ViewBag.Less = new DBSql.Oa.OaEquipStock().GetStateSum1(model.EquipId).ToString();
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {

            int reuslt = 0;
            var flowIds = new DBSql.PubFlow.Flow().GetList(p => p.Id > 0).Where(p => p.FlowRefTable == "EquipDeal" && id.Contains(p.FlowRefID)).Select(p => p.Id);
            var flowNodeExe = new DBSql.PubFlow.FlowNodeExe();
            flowNodeExe.Delete(p => flowIds.Contains(p.FlowID));
            flowNodeExe.UnitOfWork.SaveChanges();
            var flowNode = new DBSql.PubFlow.FlowNode();
            flowNode.Delete(p => flowIds.Contains(p.FlowID));
            flowNode.UnitOfWork.SaveChanges();
            var flow = new DBSql.PubFlow.Flow();
            flow.Delete(p => flowIds.Contains(p.Id));
            flow.UnitOfWork.SaveChanges();

            op.Delete(s => id.Contains(s.Id));
            try
            {
                op.UnitOfWork.SaveChanges();
                reuslt = 1;
            }
            catch { }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipDeal();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.RepairReportEmpId = TypeHelper.parseInt(Request.Form["RepairReportEmpId"].Split('-')[0]);

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
