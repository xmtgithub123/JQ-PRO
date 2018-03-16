using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Data.Entity;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using DataModel;

namespace Bussiness.Controllers
{
    public partial class BussBiddingFileMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new Project();

            ViewData["ProjEmpId1"] = "0";
            ViewData["ProjEmpId2"] = "0";

            ViewData["BusinessEmpId1"] = "0";
            ViewData["BusinessEmpId2"] = "0";


            ViewData["TechnologyEmpId1"] = "0";
            ViewData["TechnologyEmpId2"] = "0";

            model.DateCreate = DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = dbProject.Get(id);
            var _Bidding = op.Get(model.ColAttType7);
            //var _Package = dbBiddingInfoPackage.Get(model.ColAttType8);

            ViewBag.BiddingBatch = _Bidding.BiddingBatch;
            ViewBag.BiddingNumber = _Bidding.BiddingNumber;
            ViewBag.EngineeringName = _Bidding.EngineeringName;
            //ViewBag.PackageNumber = _Package.PackageNumber;

            ViewData["ProjEmpId1"] = model.ProjEmpId.ToString();
            ViewData["ProjEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId).EmpDepID.ToString();

            ViewData["BusinessEmpId1"] = model.ColAttType9.ToString();
            ViewData["BusinessEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType9) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType9).EmpDepID.ToString();

            ViewData["ProjEmpName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.ProjEmpId).FirstOrDefault().EmpName;
            ViewData["BusinessEmpName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.ColAttType9).FirstOrDefault().EmpName;


            ViewData["TechnologyEmpId1"] = model.ColAttType10.ToString();
            ViewData["TechnologyEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType10) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType10).EmpDepID.ToString();
            ViewBag.CreatorEmpId = model.CreatorEmpId;


            // 计划开始时间
            if (model.DatePlanStart != null && model.DatePlanStart.ToString().Length > 0)
            {
                ViewBag.DatePlanStart = model.DatePlanStart.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.DatePlanStart = "";
            }

            // 计划完成时间
            if (model.DatePlanFinish != null && model.DatePlanFinish.ToString().Length > 0)
            {
                ViewBag.DatePlanFinish = model.DatePlanFinish.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.DatePlanFinish = "";
            }

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion
    }
}
