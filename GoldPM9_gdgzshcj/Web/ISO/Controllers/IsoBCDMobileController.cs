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
using System;

namespace ISO.Controllers
{
    public partial class IsoBCDMobileController : MobileController
    {
        private DBSql.Iso.IsoBCD op = new DBSql.Iso.IsoBCD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
