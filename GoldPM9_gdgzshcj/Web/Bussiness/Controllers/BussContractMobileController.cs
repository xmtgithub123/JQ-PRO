using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;
namespace Bussiness.Controllers
{
    public partial class BussContractMobileController : MobileController
    {
        private DBSql.Bussiness.BussContract op = new DBSql.Bussiness.BussContract();
        private DBSql.Bussiness.BussFeePlan BFP = new DBSql.Bussiness.BussFeePlan();
        private DBSql.Bussiness.BussFeeFact BF = new DBSql.Bussiness.BussFeeFact();
        private DBSql.Bussiness.BussFeeInvoice BFI = new DBSql.Bussiness.BussFeeInvoice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
