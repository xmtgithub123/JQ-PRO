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
    public partial class IsoConstructionCoordinationMobileController : MobileController
    {
        private DBSql.Iso.IsoConstructionCoordination op = new DBSql.Iso.IsoConstructionCoordination();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
