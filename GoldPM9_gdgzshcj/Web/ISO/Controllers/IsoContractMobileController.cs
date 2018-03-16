using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using System.Xml;

namespace ISO.Controllers
{
    public partial class IsoContractMobileController : MobileController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
    }
}
