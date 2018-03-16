using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System;

namespace ISO.Controllers
{
    public partial class IsoFormProjSubPFMobileController : MobileController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
        private DBSql.Project.ProjSub dbProjSub = new DBSql.Project.ProjSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
