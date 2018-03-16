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
    public partial class SSManageMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new SSManage();

            model.EngineeringID = TypeHelper.parseInt(Request["EngID"]);//string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            ViewBag.CreatorEmpId = userInfo.EmpID;
            int empid = TypeHelper.parseInt(Request["empid"]);
            ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjName;
            ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjNumber;


            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            SSManageData ssBus = new SSManageData();
            var model = DTHelper.GetEntity<SSManage>(ssBus.Get(id));

            ViewBag.CreatorEmpId = model.CreatorEmpId;

            ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjName;
            ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == model.EmpManageId).ProjNumber;

            return View("info", model);
        }
        #endregion
    }
}
