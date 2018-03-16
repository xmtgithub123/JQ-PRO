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
    public partial class TripMobileController : MobileController
    {
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
    }
}