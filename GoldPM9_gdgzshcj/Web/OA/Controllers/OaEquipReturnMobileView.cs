using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Data;
using System;

namespace OA.Controllers
{
    public partial class OaEquipReturnMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipReturn();
            ViewBag.EquipBackDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //int id1 = int.Parse(new DBSql.Oa.OaEquipReturn().GetRID(id));
            var model = op.Get(id);
            ViewBag.EquipBackDate = model.EquipBackDate.ToString("yyyy-MM-dd");
            return View("info", model);
        }
        #endregion
    }
}
