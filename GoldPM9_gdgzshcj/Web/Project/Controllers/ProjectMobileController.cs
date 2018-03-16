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

namespace Project.Controllers
{
    [Description("ProjectMobile")]
    public partial class ProjectMobileController : MobileController
    {
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}