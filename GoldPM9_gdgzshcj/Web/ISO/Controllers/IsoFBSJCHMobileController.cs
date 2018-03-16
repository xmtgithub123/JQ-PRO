using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Text;
using Aspose.Words;
using System;

namespace ISO.Controllers
{
    public partial class IsoFBSJCHMobileController : MobileController
    {
        private DBSql.Iso.IsoFBSJCH op = new DBSql.Iso.IsoFBSJCH();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
