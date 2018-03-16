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

namespace Oa.Controllers
{
[Description("OaEquipGet")]
public class OaEquipGetController : CoreController
    {
        private DBSql.Oa.OaEquipGet op = new DBSql.Oa.OaEquipGet();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("EquipmentApply")));
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
                PageModel.SelectOrder = "Id desc";
            }

            var Thwere = QueryBuild<DataModel.Models.OaEquipGet>.True();
            List<string> permissionList = PermissionList("EquipmentApply");

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

            int EquipOrOffice = 0;
            int.TryParse(Request["EquipOrOffice"], out EquipOrOffice);
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);


            var list = op.GetPagedList(Thwere, PageModel).ToList();

            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaEquipGetFlow") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Id,
                                EquipGetCustomer = p.EquipGetCustomer,
                                EquipGetMenoy = p.EquipGetMenoy,
                                EquipGetApplyDate = p.EquipGetApplyDate,
                                CreatorEmpName = p.CreatorEmpName,
                                EquipGetDate = p.EquipGetDate,
                                EquipGetNote = p.EquipGetNote,
                                CreatorEmpId = p.CreatorEmpId,
                                //flowStatus = SetFlowStatus("OaEquipGetFlow", p.Id, p.CreatorEmpId, userInfo.EmpID)
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new { m.Id, m.CreatorEmpName, m.CreatorEmpId, m.EquipGetCustomer, m.EquipGetMenoy, m.EquipGetApplyDate, m.EquipGetDate, m.EquipGetNote, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            var result = (from m in showList
                          join t2 in op.DbContext.Set<OaEquipStock>() on m.Id equals t2.EquipFormID
                          into nodes1
                          from t3 in nodes1.DefaultIfEmpty()
                          select new
                          {
                              m.Id,
                              m.CreatorEmpName,
                              m.CreatorEmpId,
                              m.EquipGetCustomer,
                              m.EquipGetMenoy,
                              m.EquipGetApplyDate,
                              m.EquipGetDate,
                              m.EquipGetNote,
                              m.FlowID,
                              m.FlowName,
                              m.FlowStatusID,
                              m.FlowStatusName,
                              m.FlowXml,
                              m.FlowTurnedEmpIDs,
                              t3.EquipID,
                              FormEquipType = t3.EquipFormType
                          }).Where(p=>p.FormEquipType == "OaEquipGet").Distinct();

            var result1 = (from m in result
                          join t1 in op.DbContext.Set<OaEquipment>() on m.EquipID equals t1.Id into nodes1
                          from t2 in nodes1.DefaultIfEmpty()
                          where t2.EquipOrOffice == EquipOrOffice
                           select new {
                              m.Id,
                              m.CreatorEmpName,
                              m.CreatorEmpId,
                              m.EquipGetCustomer,
                              m.EquipGetMenoy,
                              m.EquipGetApplyDate,
                              m.EquipGetDate,
                              m.EquipGetNote,
                              m.FlowID,
                              m.FlowName,
                              m.FlowStatusID,
                              m.FlowStatusName,
                              m.FlowXml,
                              m.FlowTurnedEmpIDs
                          }).Distinct();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = result1
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipGet();
            model.EquipGetDate = DateTime.Now;
            model.EquipGetApplyDate = DateTime.Now;
            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.ActionFlag = GetRequestValue<string>("ActionFlag");
            var model = op.Get(id);
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;

            var flowIds = new DBSql.PubFlow.Flow().GetList(p => p.Id > 0).Where(p => p.FlowRefTable == "OaEquipGetFlow" && id.Contains(p.FlowRefID)).Select(p => p.Id);
            var flowNodeExe = new DBSql.PubFlow.FlowNodeExe();
            flowNodeExe.Delete(p => flowIds.Contains(p.FlowID));
            flowNodeExe.UnitOfWork.SaveChanges();
            var flowNode = new DBSql.PubFlow.FlowNode();
            flowNode.Delete(p => flowIds.Contains(p.FlowID));
            flowNode.UnitOfWork.SaveChanges();
            var flow = new DBSql.PubFlow.Flow();
            flow.Delete(p => flowIds.Contains(p.Id));
            flow.UnitOfWork.SaveChanges();

            op.DbContext.Set<OaEquipGet>().RemoveRange(op.DbContext.Set<OaEquipGet>().Where(p => id.Contains(p.Id)));
            op.DbContext.Set<OaEquipStock>().RemoveRange(op.DbContext.Set<OaEquipStock>().Where(p => p.EquipFormType == "OaEquipGet" && id.Contains(p.EquipFormID)));
            reuslt = op.DbContext.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipGet();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
				op.Edit(model);
            }
            else
            {
                op.MvcDefaultSave(userInfo);
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
