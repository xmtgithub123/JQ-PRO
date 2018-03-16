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
using Common.Data.Extenstions;


namespace ISO.Controllers
{
    public partial class IsoFormArchiveMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = "['NoEdit']";
            var model = new IsoForm();
            ViewBag.CreatorEmpId = userInfo.EmpID;
            model.FormDate = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.ColAttVal1 = GetRequestValue<string>("Ids");
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['NoEdit']";
            if (model.FlowTime.Year > 1900 && model.CreatorEmpId == userInfo.EmpID)
            {
                ViewBag.permission = "['a']";
            }
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion
    }
}
