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
    public partial class OaEquipGetMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipGet();
            model.EquipGetDate = DateTime.Now;
            model.EquipGetApplyDate = DateTime.Now;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.ActionFlag = GetRequestValue<string>("ActionFlag");
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion
    }
}
