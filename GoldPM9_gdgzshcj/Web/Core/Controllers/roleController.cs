using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using Common.Data.Extenstions;
using DataModel.Models;
using System.Collections.Generic;
using System;

namespace Core.Controllers
{
    [Description("角色管理")]
    public class roleController : CoreController
    {
        private DoResult doResult = DoResultHelper.doError("未知原因");


        [Description("角色主界面")]
        public ActionResult info()
        {
            return View();
        }


        [Description("根据获取菜单json数据")]
        public string getMenuArrByRoleID(int roleID)
        {
            var TWhere = QueryBuild<DataModel.Models.BaseMenuPermissionByRole>.True();
            var list = new DBSql.Sys.BaseMenuPermissionByRole().GetList(TWhere.And(p => p.BaseMenuPermissionByRoleID == roleID)).ToList();
            if (list.isNotEmpty())
            {
                var re = from a in list
                         select new
                           {
                               menuid=a.BaseMenuPermissionByRoleValue
                           };

                return JavaScriptSerializerUtil.getJson(re);
            }
            return "";        
        }
    }
}
