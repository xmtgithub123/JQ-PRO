using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System;
using System.Linq.Expressions;
using Common.Data.Extenstions;
using System.Xml;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace Design.Controllers
{
    public partial class DesExchMobileController : MobileController
    {
        private DBSql.Design.DesExch op = new DBSql.Design.DesExch();
        private DBSql.Design.DesExchRec rec = new DBSql.Design.DesExchRec();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Design.DesTask desTask = new DBSql.Design.DesTask();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Sys.AllBaseEmployee emp = new DBSql.Sys.AllBaseEmployee();
        private DBSql.Core.ModelExchange exchange = new DBSql.Core.ModelExchange();
        private DBSql.Core.ModelExchangeReceive exchRecModel = new DBSql.Core.ModelExchangeReceive();
        private DBSql.Project.Project project = new DBSql.Project.Project();
    }
}
