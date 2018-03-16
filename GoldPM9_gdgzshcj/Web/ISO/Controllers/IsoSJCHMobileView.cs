using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;
using Aspose.Words;
using System.Text;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoSJCHMobileController : MobileController 
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            var model = new IsoSJCH();

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            model.TableNumber = "SCX02-03";
            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);

            if (model != null)
            {
                ViewBag.CurrEmpID = model.CreatorEmpId;
            }
            else
            {
                ViewBag.CurrEmpID = userInfo.EmpID;
            }

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            return View("info", model);
        }
        #endregion
    }
}
