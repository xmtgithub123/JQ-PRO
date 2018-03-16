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
    public partial class VacationMobileController : MobileController
    {
        private DBSql.Hr.Vacation op = new DBSql.Hr.Vacation();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        Common.SunClass scDal = new Common.SunClass();
    }
}
