using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Text;
using Aspose.Words;
using System;

namespace ISO.Controllers
{
    public partial class IsoFBSJCHMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_FBCH")));
            var model = new IsoFBSJCH();
            model.TableNumber = "SCX03-02";
            ViewBag.CurrEmpID = userInfo.EmpID;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_FBCH")));
            var model = op.Get(id);
            ViewBag.CurrEmpID = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion
    }
}