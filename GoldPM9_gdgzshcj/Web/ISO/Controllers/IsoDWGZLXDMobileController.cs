using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoDWGZLXDMobileController : MobileController
    {
        private DBSql.Iso.IsoDWGZLXD op = new DBSql.Iso.IsoDWGZLXD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
    }
}
