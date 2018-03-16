using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;

namespace ISO.Controllers
{
    public partial class IsoGCCLTJDMobileController : MobileController
    {
        private DBSql.Iso.IsoGCCLTJD op = new DBSql.Iso.IsoGCCLTJD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
