using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoGCDZKCTJDJZMobileController : MobileController
    {
        private DBSql.Iso.IsoGCDZKCTJDJZ op = new DBSql.Iso.IsoGCDZKCTJDJZ();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
