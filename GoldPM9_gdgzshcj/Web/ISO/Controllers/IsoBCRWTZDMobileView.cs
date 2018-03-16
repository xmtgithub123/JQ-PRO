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
    public partial class IsoBCRWTZDMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = new IsoBCRWTZD();
            model.TableNumber = "SCX02-03";

            DataModel.Models.Project project = new DBSql.Project.Project().Get(GetRequestValue<int>("projId"));

            ViewBag.ProjDeptName = project.FK_Project_ProjDepId.BaseName;
            ViewBag.ColAttType1Name = project.FK_Project_ProjTypeID.BaseName;
            ViewBag.ProjAreaName = project.FK_Project_ProjAreaID.BaseName;

            ViewBag.CompanyName = project.CustName;
            ViewBag.CompanyLinkMan = project.CustLinkMan;
            ViewBag.CompanyTel = project.CustLinkTel;

            ViewBag.CurrEmpID = userInfo.EmpID;

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

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

            var _projDeptName = dbBaseData.Get(_project.ProjDepId);
            ViewBag.ProjDeptName = null != _projDeptName ? _projDeptName.BaseName : "";

            var _colAttType1Name = dbBaseData.Get(_project.ColAttType1);
            ViewBag.ColAttType1Name = null != _colAttType1Name ? _colAttType1Name.BaseName : "";

            var _projAreaName = dbBaseData.Get(_project.ProjAreaID);
            ViewBag.ProjAreaName = null != _projAreaName ? _projAreaName.BaseName : "";

            ViewBag.CompanyName = _project.CustName;
            ViewBag.CompanyLinkMan = _project.CustLinkMan;
            ViewBag.CompanyTel = _project.CustLinkTel;

            ViewBag.CurrEmpID = model.CreatorEmpId;


            return View("info", model);
        }
        #endregion
    }
}
