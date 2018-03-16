using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System;

namespace OA.Controllers
{
    public partial class OaCarMobileController : MobileController
    {

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaCarUse();
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            model.DateLeavePlan = System.DateTime.Now;
            model.DateArrivePlan = System.DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarApply")));
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var FlowNodeID = GetRequestValue<int>("flowNodeID");
            var model = op.Get(id);

            var carOp = new DBSql.OA.OaCar();
            var empOp = new DBSql.Sys.BaseEmployee();
            var LeaderempInfo = empOp.Get(model.UseLeaderEmp);
            model.CarID = model.CarID == 0 ? -1 : model.CarID;
            var carModel = carOp.Get(model.CarID);
            var FlowNode = new DataModel.Models.FlowNode();

            ViewData["UseLeaderEmpName"] = new DBSql.Sys.BaseEmployee().GetEmpName(model.UseLeaderEmp.ToString());
            ViewBag.UseLeaderEmpName = new DBSql.Sys.BaseEmployee().GetEmpName(model.UseLeaderEmp.ToString());

            if (model.DateLeavePlan != null && model.DateLeavePlan.ToString().Length > 0)
            {
                ViewBag.LeavePlan = model.DateLeavePlan;
            }
            else
            {
                ViewBag.LeavePlan = "";
            }

            if (model.DateArrivePlan != null && model.DateArrivePlan.ToString().Length > 0)
            {
                ViewBag.ArrivePlan = model.DateArrivePlan;
            }
            else
            {
                ViewBag.ArrivePlan = "";
            }


            var IsSelectCar = 0;
            try
            {
                string sName = GetRequestValue<string>("sName");
                if (sName.IndexOf("退回") > -1)
                    IsSelectCar = 0;
                else
                    IsSelectCar = 1;
            }
            catch (Exception ex)
            {
                IsSelectCar = 1;
            }

            ViewBag.IsSelectCar = IsSelectCar;
            try
            {
                ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            }
            catch
            {
                ViewData["CarInfo"] = "";
            }

            ViewBag.LempInfo = LeaderempInfo.EmpID.ToString();
            ViewData["EditType"] = "SetCar";
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.permission = JavaScriptSerializerUtil.getJson(PermissionList("CarApply"));
            return View("info", model);
        }
        #endregion
    }
}
