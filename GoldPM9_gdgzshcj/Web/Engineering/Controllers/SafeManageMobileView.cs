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
    public partial class SafeManageMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new SafeManage();

            model.EngineeringID = TypeHelper.parseInt(Request["EngID"]);//string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            int empid = TypeHelper.parseInt(Request["empid"]);
            ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjName;
            ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjNumber;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            SafeManageData safeBus = new SafeManageData();
            var model = DTHelper.GetEntity<SafeManage>(safeBus.Get(id));
            ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjName;
            ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjNumber;
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            return View("info", model);
        }
        #endregion
    }
}
