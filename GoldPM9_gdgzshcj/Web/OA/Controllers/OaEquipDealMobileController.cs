using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaEquipDealMobileController : MobileController
    {
        private DBSql.Oa.OaEquipDeal op = new DBSql.Oa.OaEquipDeal();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
