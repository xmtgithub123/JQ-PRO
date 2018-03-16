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
    public partial class BussProjInfoRecordsMobileController : MobileController
    {
        private DBSql.Bussiness.BussProjInfoRecords op = new DBSql.Bussiness.BussProjInfoRecords();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
    }
}
