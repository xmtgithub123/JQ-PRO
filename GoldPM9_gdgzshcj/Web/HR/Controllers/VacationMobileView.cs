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

namespace HR.Controllers
{
    public partial class VacationMobileController : MobileController
    {
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
    }
}