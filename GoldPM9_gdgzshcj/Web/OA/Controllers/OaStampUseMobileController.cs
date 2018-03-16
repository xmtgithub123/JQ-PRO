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
    public partial class OaStampUseMobileController : MobileController
    {
        private DBSql.Oa.OaStampUse op = new DBSql.Oa.OaStampUse();
        private DBSql.Oa.OaStampInfo opOaStampInfo = new DBSql.Oa.OaStampInfo();
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.PubFlow.Flow dbFlow = new DBSql.PubFlow.Flow();

        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
