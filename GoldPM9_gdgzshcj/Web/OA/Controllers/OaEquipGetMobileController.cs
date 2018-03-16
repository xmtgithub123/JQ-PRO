using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.Linq.Expressions;
using System.Text;
using Common.Data.Extenstions;

namespace OA.Controllers
{
    public partial class OaEquipGetMobileController : MobileController
    {
        private DBSql.Oa.OaEquipGet op = new DBSql.Oa.OaEquipGet();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
