using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Data.Entity;
using DBSql.PubFlow;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;

namespace Bussiness.Controllers
{
    public partial class BussBiddingCostMobileController : MobileController
    {
        private DBSql.Bussiness.BussBiddingInfo BiddingInfoDB = new DBSql.Bussiness.BussBiddingInfo();
        private DBSql.Bussiness.BussBiddingInfoPackage BiddingInfoPackageDB = new DBSql.Bussiness.BussBiddingInfoPackage();
        private DBSql.Bussiness.BussBiddingCost op = new DBSql.Bussiness.BussBiddingCost();
        private DBSql.Sys.BaseData BaseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
    }
}
