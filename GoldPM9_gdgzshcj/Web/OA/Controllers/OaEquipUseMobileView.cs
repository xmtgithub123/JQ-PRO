using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaEquipUseMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipUse();
            int EquipOrOffice = 0;
            if (GetRequestValue<int>("EquipOrOffice") != null)
            {
                EquipOrOffice = GetRequestValue<int>("EquipOrOffice");
            }
            if (EquipOrOffice != 1)
            {
                ViewBag.permission = "['add','submit']";
                return View("info", model);
            }
            else
            {
                ViewBag.permission = "['add','submit']";
                return View("infoOaUsage", model);
            }
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            List<string> permissionList = PermissionList("EquipmentBorrow");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;

            if (model.EquipOrOffice != 1)
            {
                return View("info", model);
            }
            else
            {
                return View("infoOaUsage", model);
            }
        }
        #endregion
    }
}
