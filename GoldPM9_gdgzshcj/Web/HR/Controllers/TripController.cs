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

namespace Hr.Controllers
{
[Description("Trip")]
public class TripController : CoreController
    {
        private DBSql.Hr.Trip op = new DBSql.Hr.Trip();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        Common.SunClass scDal = new Common.SunClass();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Trip")));
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
            var Thwere = QueryBuild<DataModel.Models.Trip>.True();
            List<string> permissionList = PermissionList("Trip");
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
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(Thwere, PageModel).ToList();
            var datalist = new DBSql.Sys.BaseData().GetDataSetByEngName("TripTypeId");
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "Trip") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Id,
                                EmpName = p.EmpName,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                Days = p.Days,
                                TripReason = p.TripReason,
                                TripReceive = p.TripReceive,
                                TripType = p.TripType,
                                p.IsBX,
                                CreatorEmpId = p.CreatorEmpId,
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new { m.Id, m.EmpName, m.StartDate, m.EndDate, m.Days, m.TripReason, m.TripType, m.TripReceive,m.IsBX, m.FlowID, m.FlowName, m.FlowStatusID, m.CreatorEmpId, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList,
                isExport = permissionList.Contains("export") ? 1 : 0
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CreatorEmpId = userInfo.EmpID;
            var model = new Trip();
            model.ApplicationDate = DateTime.Now;
            var hrModel = new DBSql.HR.HREmployee().GetList(p => p.SysEmpId == userInfo.EmpID).FirstOrDefault();
            ViewBag.EmpID = hrModel != null ? hrModel.Id : 0;
            ViewBag.EmpName = hrModel != null ? hrModel.EmpName : "请添加员工信息";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            List<string> permissionList = PermissionList("Trip");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            var model = op.Get(id);
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            var hrModel = new DBSql.HR.HREmployee().GetList(p => p.Id == model.EmpID).FirstOrDefault();
            ViewBag.EmpID = hrModel.Id;
            ViewBag.EmpName = hrModel.EmpName;
            return View("info", model);
        }

        public ActionResult editBack(int id)
        {
            List<string> permissionList = PermissionList("Trip");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            var model = op.Get(id);
            ViewBag.CreatorEmpId = userInfo.EmpID;
            return View("backInfo", model);
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
            var model = new Trip();
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
