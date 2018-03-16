using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System;

namespace HR.Controllers
{
    public partial class TripMobileController : MobileController
    {
        private DBSql.Hr.Trip op = new DBSql.Hr.Trip();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        Common.SunClass scDal = new Common.SunClass();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
    }
}
