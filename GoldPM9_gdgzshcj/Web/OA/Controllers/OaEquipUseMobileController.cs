using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaEquipUseMobileController : MobileController
    {
        private DBSql.Oa.OaEquipUse op = new DBSql.Oa.OaEquipUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.PubFlow.Flow dbFlow = new DBSql.PubFlow.Flow();
    }
}
