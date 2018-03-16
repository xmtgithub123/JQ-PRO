using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System;

namespace OA.Controllers
{
    public partial class OaCarMobileController : MobileController
    {
        private DBSql.OA.OaCarUse op = new DBSql.OA.OaCarUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.PubFlow.FlowNode opNode = new DBSql.PubFlow.FlowNode();


    }
}
