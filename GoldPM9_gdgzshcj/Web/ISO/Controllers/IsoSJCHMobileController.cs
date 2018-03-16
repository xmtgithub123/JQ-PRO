using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;
using Aspose.Words;
using System.Text;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoSJCHMobileController : MobileController
    {
        private DBSql.Iso.IsoSJCH op = new DBSql.Iso.IsoSJCH();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
