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

namespace ISO.Controllers
{
    public partial class IsoBGSJRWTZDMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = new IsoBGSJRWTZD();
            model.TableNumber = "SCX02-04";

            ViewBag.CompanyName = "";
            ViewBag.CompanyLinkMan = "";
            ViewBag.CompanyTel = "";

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            DataModel.Models.Project project = new DBSql.Project.Project().Get(GetRequestValue<int>("projId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
                model.CompanyID = project.CompanyID;
            }

            ViewBag.CompanyName = project.CustName;
            ViewBag.CompanyLinkMan = project.CustLinkMan;
            ViewBag.CompanyTel = project.CustLinkTel;

            ViewBag.CurrEmpID = userInfo.EmpID;

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = op.Get(id);

            var _project = dbProject.Get(model.ProjID);
            ViewBag.CompanyName = _project.CustName;
            ViewBag.CompanyLinkMan = _project.CustLinkMan;
            ViewBag.CompanyTel = _project.CustLinkTel;
            ViewBag.CurrEmpID = model.CreatorEmpId;

            return View("info", model);
        }
        #endregion
    }
}
