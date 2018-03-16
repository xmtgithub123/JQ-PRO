using Common;
using DataModel.Models;
using DBSql;
using JQ.Util;
using JQ.Web;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Engineering.Controllers
{
    public partial class ProjManageMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new ProjManage();

            model.EngineeringID = string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            ViewBag.CreatorEmpId = userInfo.EmpID;
            int empid = TypeHelper.parseInt(Request["empid"]);
            if (empid != 0)
            {
                ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjName;
                ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjNumber;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.ProjName = "";
            }
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ProjManageData projBus = new ProjManageData();
            var model = DTHelper.GetEntity<ProjManage>(projBus.Get(id));

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.ProjName = new DBSql.Project.Project().FirstOrDefault(p => p.Id == model.EngineeringID).ProjName;
            ViewBag.ProjNumber = new DBSql.Project.Project().FirstOrDefault(p => p.Id == model.EngineeringID).ProjNumber;

            return View("info", model);
        }
        #endregion
    }
}
