using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Bussiness.Controllers
{
    public partial class BussContractSubMobileController : MobileController
    {
        private DBSql.Bussiness.BussContractSub op = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Project.ProjSub opProjSub = new DBSql.Project.ProjSub();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
    }
}
