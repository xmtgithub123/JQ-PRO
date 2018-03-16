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
        private DBSql.Oa.OaEquipReturn op = new DBSql.Oa.OaEquipReturn();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
