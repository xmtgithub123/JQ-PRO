using Common;
using DataModel.Models;
using DBSql;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Engineering.Controllers
{
    public partial class EmpManageMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new EmpManage();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //EmpManageData empBusiness = new EmpManageData();
            var model = new DBSql.Engineering.EmpManage().Get(id);
            //var model = DTHelper.GetEntity<EmpManage>(empBusiness.Get(id));
            return View("info", model);
        }
        #endregion
    }
}
