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
using System;
using Common.Data.Extenstions;
using System.Web;
using System.Data;

namespace Bussiness.Controllers
{
    public partial class BussBiddingInfoMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var _baseDataList = dbBaseData.GetDataSetByEngName("BiddingProgress").ToList();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DataModel.Models.BaseData dmBaseData in _baseDataList)
            {
                var _key = dmBaseData.BaseID.ToString();
                var _value = dmBaseData.BaseName;
                SelectListItem _item = new SelectListItem();
                _item.Text = _value;
                _item.Value = _key;
                items.Add(_item);
            }
            ViewData["Items"] = items;

            var model = new BussBiddingInfo();

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.ReturnDate = DateTime.Now.AddMonths(6);
            string companyType = Request.Params["CompanyType"].ToString();
            string page = "";
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

            model.BiddingNumber = DateTime.Now.ToString("yyyyMMddHHmm");

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var _baseDataList = dbBaseData.GetDataSetByEngName("BiddingProgress").ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DataModel.Models.BaseData dmBaseData in _baseDataList)
            {
                var _key = dmBaseData.BaseID.ToString();
                var _value = dmBaseData.BaseName;
                SelectListItem _item = new SelectListItem();
                _item.Text = _value;
                _item.Value = _key;
                items.Add(_item);
            }
            ViewData["Items"] = items;
            var model = op.Get(id);
            ViewData["BiddingManageInfo1"] = model.BiddingManageID.ToString();
            ViewData["BiddingManageInfo2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.BiddingManageID) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.BiddingManageID).EmpDepID.ToString();
            ViewData["BinddingMangeName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.BiddingManageID).FirstOrDefault().EmpName;

            ViewData["EntryManager1"] = model.EntryManager.ToString();
            ViewData["EntryManager2"] = model.FK_BussBiddingInfo_EntryManager.EmpDepID;
            
            ViewData["Delegator1"] = model.Delegator.ToString();
            ViewData["Delegator2"] = model.FK_BussBiddingInfo_Delegator.EmpDepID;
            ViewData["DelegatorName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.Delegator).FirstOrDefault().EmpName;

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.ReturnDate = model.ReturnDate;

            // 报名时间
            if (model.EntryTime != null && model.EntryTime.ToString().Length > 0)
            {
                ViewBag.EntryTime = model.EntryTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.EntryTime = "";
            }

            // 开标日期
            if (model.BiddingOpeningTime != null && model.BiddingOpeningTime.ToString().Length > 0)
            {
                ViewBag.BiddingOpeningTime = model.BiddingOpeningTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.BiddingOpeningTime = "";
            }

            // 状态时间
            if (model.BidStatusTime != null && model.BidStatusTime.ToString().Length > 0)
            {
                ViewBag.BidStatusTime = model.BidStatusTime.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.BidStatusTime = "";
            }

            string companyType = Request["CompanyType"];
            //string companyType = Request.Params["CompanyType"].ToString();
            string page = "";
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
