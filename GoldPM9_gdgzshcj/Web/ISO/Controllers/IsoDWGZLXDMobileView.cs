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
    public partial class IsoDWGZLXDMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = new IsoDWGZLXD();
            model.TableNumber = "SCX02-10";
            model.SignTime = DateTime.Now;

            var dmBaseData = dbBaseData.GetBaseDataInfo(userInfo.CompanyID);
            ViewBag.CompanyName = null != dmBaseData ? dmBaseData.BaseName : "";
            ViewBag.CompanyTel = null != dmBaseData ? dmBaseData.BaseAtt1 : "";
            ViewBag.DrafterName = userInfo.EmpName;
            ViewBag.AcceptUnitName = "";

            ViewBag.ProjPhaseEmpName = "";
            ViewBag.FProjEmpName = "";

            ViewBag.CurrEmpID = userInfo.EmpID;

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                var _project = dbProject.Get(dtg.ProjId);
                ViewBag.MainProjId = _project.ParentId;
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

            var CompanyName = "";
            var CompanyTel = "";

            var list = new DBSql.Sys.BaseData().GetDataSetByEngName("Company");
            var Spec = list.Where(s => s.BaseAtt2 == "0").ToList();
            if (Spec.Count > 0)
            {
                CompanyName = Spec[0].BaseName;
                CompanyTel = Spec[0].BaseAtt1;
            }

            ViewBag.CompanyName = CompanyName;
            ViewBag.CompanyTel = CompanyTel;

            var _project = dbProject.Get(model.ProjID);
            ViewBag.DrafterName = model.CreatorEmpName;

            ViewBag.MainProjId = _project.ParentId;
            ViewBag.ProjEmpId = _project.ProjEmpId;
            ViewBag.ProjPhaseEmpName = _project.ProjEmpName;
            ViewBag.FProjEmpId = _project.FProjEmpId;
            ViewBag.FProjEmpName = _project.FProjEmpName;
            ViewBag.desTaskGroupId = model.DesTaskGroupId;
            ViewBag.CurrEmpID = model.CreatorEmpId;

            return View("info", model);
        }
        #endregion
    }
}
