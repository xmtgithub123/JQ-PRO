using Common;
using DataModel.Models;
using DBSql;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace Engineering.Controllers
{
    [Description("WDManage")]
    public class WDManageController : CoreController
    {
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 详情
        [Description("详情")]
        public ActionResult info()
        {
            ViewBag.EngineeringID = string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            int projid = string.IsNullOrEmpty(Request["ProjID"]) ? 0 : Convert.ToInt32(Request["ProjID"]);
            ViewBag.ProjID = DTHelper.GetEntity<DataModel.Models.Project>(new EmpManageData().GetProject(projid)).ParentId;

            return View();
        }
        #endregion
    }
}
