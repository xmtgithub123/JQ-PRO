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
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussBiddingCost();

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            //var _detail = BiddingInfoPackageDB.Get(model.BussBiddingInfoPackageID);
            var _Info = BiddingInfoDB.Get(model.BussBiddingInfoID);
            ViewBag.BiddingBatch = _Info.BiddingBatch;
            ViewBag.BiddingNumber = _Info.BiddingNumber;
            //ViewBag.PackageNumber = _detail.PackageNumber;
            //ViewBag.WinTime = _detail.WinTime;
            //var _basedata = BaseDataDB.Get(_detail.BiddingProgress);
            //ViewBag.ProgressName = _basedata.BaseName;

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion
    }
}
