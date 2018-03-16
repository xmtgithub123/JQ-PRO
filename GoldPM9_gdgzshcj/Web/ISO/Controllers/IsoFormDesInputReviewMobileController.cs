using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoFormDesInputReviewMobileController : MobileController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();

    }
}
