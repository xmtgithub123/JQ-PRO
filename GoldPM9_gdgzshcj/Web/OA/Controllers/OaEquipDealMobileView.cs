using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaEquipDealMobileController : MobileController
    {
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
    }
}
