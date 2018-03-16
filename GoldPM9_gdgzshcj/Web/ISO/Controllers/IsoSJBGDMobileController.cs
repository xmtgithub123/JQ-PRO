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
    public partial class IsoSJBGDMobileController : MobileController
    {
        private DBSql.Iso.IsoSJBGD op = new DBSql.Iso.IsoSJBGD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
