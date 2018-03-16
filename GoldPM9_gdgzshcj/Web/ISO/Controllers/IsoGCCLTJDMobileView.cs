using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;

namespace ISO.Controllers
{
    public partial class IsoGCCLTJDMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            var model = new IsoGCCLTJD();
            model.TableNumber = "SCX02-10";

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.CurrEmpID = model.CreatorEmpId;

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

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
