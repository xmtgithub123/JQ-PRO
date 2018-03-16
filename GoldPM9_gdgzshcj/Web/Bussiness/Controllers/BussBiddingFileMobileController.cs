using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Data.Entity;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using DataModel;

namespace Bussiness.Controllers
{
    public partial class BussBiddingFileMobileController : MobileController
    {
        private DBSql.Bussiness.BussBiddingInfo op = new DBSql.Bussiness.BussBiddingInfo();
        private DBSql.Bussiness.BussBiddingInfoPackage dbBiddingInfoPackage = new DBSql.Bussiness.BussBiddingInfoPackage();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        DBSql.Project.Project dbProject = new DBSql.Project.Project();

        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
