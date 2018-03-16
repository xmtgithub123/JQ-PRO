using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.Linq.Expressions;
using System.Text;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaStampUseMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CustName = "";
            var model = new OaStampUse();
            model.StampUseDate = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";
            model.StampUseDate = DateTime.Now;
            ViewBag.StampType = "";

            ViewBag.isExist = new DBSql.Hr.Vacation().GetCount(userInfo.EmpID);


            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var staModel = new DBSql.Oa.OaStampInfo().Get(model.StampID);
            List<string> permissionList = PermissionList("OaSealApply");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            ViewBag.ProjName = model.ProjName;
            ViewBag.ProjID = model.ProjId;
            var projModel = new DBSql.Project.Project().Get(model.ProjId);
            if (projModel != null)
            {
                ViewBag.ProjNumber = projModel.ProjNumber;
            }
            else
            {
                ViewBag.ProjNumber = "";
            }

            ViewBag.StampType = staModel.FK_OaStampInfo_StampTypeID.BaseName;
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion
    }
}
