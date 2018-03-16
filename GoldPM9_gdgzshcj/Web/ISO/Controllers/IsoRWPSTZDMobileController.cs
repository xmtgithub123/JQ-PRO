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
using System.Xml;

namespace ISO.Controllers
{
    public partial class IsoRWPSTZDMobileController : MobileController
    {
        private DBSql.Iso.IsoRWPSTZD op = new DBSql.Iso.IsoRWPSTZD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
    }
}
