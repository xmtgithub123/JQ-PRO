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
using System.Collections;

namespace Project.Controllers
{
    public partial class ProjSubMobileController : MobileController
    {
        private DBSql.Project.ProjSub op = new DBSql.Project.ProjSub();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        private DBSql.Bussiness.BussContractSub bussContractSub = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
