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
[Description("Vacation")]
public class VacationController : CoreController
    {
        private DBSql.Hr.Vacation op = new DBSql.Hr.Vacation();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        Common.SunClass scDal = new Common.SunClass();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Vacation")));
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

            var Thwere = QueryBuild<DataModel.Models.Vacation>.True();
            List<string> permissionList = PermissionList("Vacation");

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

            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);

            if (userInfo.CompanyID != 0)
            {
            }

            var list = op.GetPagedList(Thwere, PageModel).ToList();
            

            var datalist = new DBSql.Sys.BaseData().GetDataSetByEngName("VacationTypeId");
            var datalist1 = new DBSql.Sys.BaseData().GetDataSetByEngName("department");
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "Vacation") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Id,
                                //EmpName = (from j in op.DbContext.Set<DataModel.Models.HREmployee>().Where(d => d.Id == p.EmpID).Select(nj => new { nj.EmpName}) ,
                                EmpName = p.EmpName,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                Days = p.Days,
                                VacationReason = p.VacationReason,
                                DepName = p.CreatorDepName,//datalist1.FirstOrDefault(d => d.BaseID == int.Parse(p.DepName == "" ? "0" : p.DepName)) == null ? "" : datalist1.FirstOrDefault(d => d.BaseID == int.Parse(p.DepName == "" ? "0" : p.DepName)).BaseName, // p.DepName,
                                VacationType = datalist.FirstOrDefault(d => d.BaseID == int.Parse(p.VacationType))==null? "" : datalist.FirstOrDefault(d => d.BaseID == int.Parse(p.VacationType)).BaseName,//p.VacationType,
                                CreatorEmpId = p.CreatorEmpId,
                                //flowStatus = SetFlowStatus("OaEquipGetFlow", p.Id, p.CreatorEmpId, userInfo.EmpID)
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new { m.Id, m.EmpName, m.StartDate, m.EndDate, m.Days, m.VacationReason, m.DepName, m.VacationType, m.FlowID, m.FlowName, m.FlowStatusID, m.CreatorEmpId, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });
            

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList,
                isExport = permissionList.Contains("export")?1:0
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CreatorEmpId = userInfo.EmpID;
            ViewBag.isExist = new DBSql.Hr.Vacation().GetCount(userInfo.EmpID);
            var model = new Vacation();
            var hrModel = new DBSql.HR.HREmployee().GetList(p => p.SysEmpId == userInfo.EmpID).FirstOrDefault();
            ViewBag.EmpID = hrModel != null ? hrModel.Id : 0;
            ViewBag.EmpName = hrModel != null ? hrModel.EmpName : "请添加员工信息";

            DateTime year = DateTime.Parse(DateTime.Now.Year.ToString() + "/01/01");
            var flow = op.DbContext.Set<Flow>().Where(p => p.FlowRefTable == "Vacation" && p.FlowStatusID == 3);
            var data = op.DbContext.Set<Vacation>().Where(p => p.CreationTime >= year && p.CreatorEmpId == userInfo.EmpID);

            var list = from d in data
                       from f in flow
                       where d.Id == f.FlowRefID
                       select d;
            ViewBag.SumDays = list.ToList().Count > 0 ? list.Select(p => p.Days).Sum() : 0;

            return View("info", model);
        }
        #endregion
    
        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.CreatorEmpId = userInfo.EmpID;

            List<string> permissionList = PermissionList("Vacation");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            var model = op.Get(id);

            DateTime year = DateTime.Parse(DateTime.Now.Year.ToString() + "/01/01");
            var flow = op.DbContext.Set<Flow>().Where(p => p.FlowRefTable == "Vacation" && p.FlowStatusID == 3);
            var data = op.DbContext.Set<Vacation>().Where(p => p.CreationTime >= year && p.CreatorEmpId == model.CreatorEmpId);

            var list = from d in data
                       from f in flow
                       where d.Id == f.FlowRefID
                       select d;
            ViewBag.SumDays = list.ToList().Count > 0 ? list.Select(p => p.Days).Sum() : 0;

            ViewBag.isExist = new DBSql.Hr.Vacation().GetCount(model.CreatorEmpId);
            var hrModel = new DBSql.HR.HREmployee().GetList(p => p.Id == model.EmpID).FirstOrDefault();
            //var hrModel = new DBSql.HR.HREmployee().GetList(p => p.SysEmpId == model.CreatorEmpId).FirstOrDefault();
            ViewBag.EmpID = hrModel.Id;
            ViewBag.EmpName = hrModel.EmpName;
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
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
            var model = new Vacation();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.MvcDefaultSave(userInfo);

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
