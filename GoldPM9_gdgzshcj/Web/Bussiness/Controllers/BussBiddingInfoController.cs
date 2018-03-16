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
    [Description("BussBiddingInfo")]
    public class BussBiddingInfoController : CoreController
    {
        private DBSql.Bussiness.BussBiddingInfo op = new DBSql.Bussiness.BussBiddingInfo();
        private DBSql.Bussiness.BussBiddingInfoPackage dbBiddingInfoPackage = new DBSql.Bussiness.BussBiddingInfoPackage();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("投标信息管理(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidInfo")));
            return View();
        }

        [Description("投标信息管理(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidInfo_SJ")));
            return View();
        }

        [Description("投标信息管理(创景公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidInfo_GC")));
            return View();
        }

        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidInfo_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = new List<string>();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id";
            }
            var TWhere = QueryBuild<DataModel.Models.BussBiddingInfo>.True().And(m => m.DeleterEmpId == 0);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("BidInfo_SJ");
                    TWhere = TWhere.And(p => p.CompanyID == 1);
                    break;
                case "GC":
                    result = PermissionList("BidInfo_GC");
                    TWhere = TWhere.And(p => p.CompanyID == 2);
                    break;
                case "CJ":
                    result = PermissionList("BidInfo_CJ");
                    TWhere = TWhere.And(p => p.CompanyID == 3);
                    break;
                default:
                    result = PermissionList("BidInfo");
                    TWhere = TWhere.And(p => p.CompanyID == 0);
                    break;
            }

            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result.Contains("dep"))
            {
                TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            var list = op.GetPagedList(TWhere, PageModel).ToList();

            var target = (from item in list
                          select new
                          {
                              item.Id,
                              item.BiddingBatch,
                              item.EngineeringName,
                              item.BiddingNumber,
                              BiddingManageID = item.FK_BussBiddingInfo_BiddingManageID == null ? 0 : item.FK_BussBiddingInfo_BiddingManageID.EmpID,
                              BiddingManageName = item.FK_BussBiddingInfo_BiddingManageID == null ? "" : item.FK_BussBiddingInfo_BiddingManageID.EmpName,
                              item.BiddingOpeningTime,

                              CustomerID = item.FK_BussBiddingInfo_CustID == null ? 0 : item.FK_BussBiddingInfo_CustID.Id,
                              CustomerName = item.CustName == null ? "" : item.CustName,
                              CreatorEmpId = item.CreatorEmpId,

                              DeptId = item.BiddingDeptId,
                              DeptName = "",
                              item.CoOrganizer,
                              item.EntryTime,
                              item.EntryManager,
                              item.Delegator,
                              item.ProjManager,
                              IsTemporaryName = (item.IsTemporary == 0 ? "否" : "是"),
                              item.TemporaryMoney,
                              item.TemporaryIsBack,
                              item.BiddingSourse,
                              datas = item.FK_BussBiddingInfoPackage_BussBiddingInfoID.Where(m => m.BussBiddingInfoID == item.Id).ToList().Select(m => new { m.Id, m.PackageNumber, m.FK_BussBiddingInfoPackage_BiddingProgress.BaseName, WinTime = m.WinTime.ToString("yyyy-MM-dd"), CostID = m.FK_BussBiddingCost_BussBiddingInfoPackageID.FirstOrDefault(p => p.BussBiddingInfoPackageID == m.Id) == null ? 0 : m.FK_BussBiddingCost_BussBiddingInfoPackageID.FirstOrDefault(p => p.BussBiddingInfoPackageID == m.Id).Id })
                          }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

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

            ViewData["EntryManager1"] = model.EntryManager.ToString();
            ViewData["EntryManager2"] = model.FK_BussBiddingInfo_EntryManager.EmpDepID;

            ViewData["Delegator1"] = model.Delegator.ToString();
            ViewData["Delegator2"] = model.FK_BussBiddingInfo_Delegator.EmpDepID;

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.ReturnDate = model.ReturnDate;

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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateBussBiddingInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "BussBiddingInfo");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BussBiddingInfo");
                }
                reuslt = 1;
            }
            catch
            {

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult save(int Id)
        {
            var model = new BussBiddingInfo();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            var _BiddingManageID = Request.Form["BiddingManageID"];
            var _BiddingDeptId = Request.Form["EmpDepID"];
            model.BiddingDeptId = "";
            model.BiddingManageID = TypeHelper.parseInt(_BiddingManageID.Split('-')[0]);

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "";
            switch (companyType)
            {
                case "SJ":
                    model.CompanyID = 1;
                    break;
                case "GC":
                    model.CompanyID = 2;
                    break;
                case "CJ":
                    model.CompanyID = 3;
                    break;
                default:
                    model.CompanyID = 0;
                    break;
            }

            int reuslt = 0;
            try
            {
                var strxml = Request.Form["strxml"];
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(strxml);
                op.CreateOrUpdate(model, xmlDocument);
                reuslt = 1;
            }
            catch (Exception ex)
            {
                reuslt = -1;
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string packageTablejson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id ";
            }
            int MainID = TypeHelper.parseInt(Request.Form["Id"]);
            var list = dbBiddingInfoPackage.GetPagedList(p => p.BussBiddingInfoID == MainID, PageModel).ToList();


            var target = from item in list
                         select new
                         {
                             item.Id,
                             item.BussBiddingInfoID,
                             item.PackageNumber,
                             item.WinTime,
                             item.BiddingProgress
                         };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });

        }
        #endregion


        public ActionResult ListInfo(int IsFilter)
        {
            ViewBag.IsFilter = IsFilter;
            return View("ListInfo");
        }
        public int GetBiddingsCount(int Id, string BiddingNumber, string Json)
        {
            Json = HttpUtility.HtmlDecode(Json);
            string[] list = Json.Split(',');
            string str = "";
            foreach (string s in list)
            {
                str += "'" + s + "',";
            }
            int Count = op.GetBiddingsCount(Id, BiddingNumber, str.TrimEnd(','));
            return Count;
        }

        #region 列表json
        [Description("获取投标信息管理列表")]
        [HttpPost]
        public string GetBiddingList()
        {
            List<string> result = new List<string>();
            // emp  个人    // dep 部门    // allview  全部   // alledit  维护
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    result = PermissionList("BidInfo_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    result = PermissionList("BidInfo_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    result = PermissionList("BidInfo_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    result = PermissionList("BidInfo");
                    break;
            }

            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "BiddingOpeningTimeS")
                    {
                        PageModel.SelectCondtion.Add("BiddingOpeningTimeS", _child.Value);
                    }
                    else if (_child.Key == "BiddingOpeningTimeE")
                    {
                        PageModel.SelectCondtion.Add("BiddingOpeningTimeE", _child.Value);
                    }
                    else if (_child.Key == "BidBondPayTimeS")
                    {
                        PageModel.SelectCondtion.Add("BidBondPayTimeS", _child.Value);
                    }
                    else if (_child.Key == "BidBondPayTimeE")
                    {
                        PageModel.SelectCondtion.Add("BidBondPayTimeE", _child.Value);
                    }
                    else if (_child.Key == "BidBondBackTimeS")
                    {
                        PageModel.SelectCondtion.Add("BidBondBackTimeS", _child.Value);
                    }
                    else if (_child.Key == "BidBondBackTimeE")
                    {
                        PageModel.SelectCondtion.Add("BidBondBackTimeE", _child.Value);
                    }
                    else if (_child.Key == "WinTimeS")
                    {
                        PageModel.SelectCondtion.Add("WinTimeS", _child.Value);
                    }
                    else if (_child.Key == "WinTimeE")
                    {
                        PageModel.SelectCondtion.Add("WinTimeE", _child.Value);
                    }
                    else if (_child.Key == "PerformanceBondPayTimeS")
                    {
                        PageModel.SelectCondtion.Add("PerformanceBondPayTimeS", _child.Value);
                    }
                    else if (_child.Key == "PerformanceBondPayTimeE")
                    {
                        PageModel.SelectCondtion.Add("PerformanceBondPayTimeE", _child.Value);
                    }
                    else if (_child.Key == "PerformanceBondBackTimeS")
                    {
                        PageModel.SelectCondtion.Add("PerformanceBondBackTimeS", _child.Value);
                    }
                    else if (_child.Key == "PerformanceBondBackTimeE")
                    {
                        PageModel.SelectCondtion.Add("PerformanceBondBackTimeE", _child.Value);
                    }
                    else if (_child.Key == "BiddingProgress")
                    {
                        PageModel.SelectCondtion.Add("BiddingProgress", _child.Value);
                    }
                }
            }
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "b.Id desc";
            }
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }

            var dt = op.GetBiddingList(PageModel, this.userInfo);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetListInfo()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "BiddingOpeningTimeS")
                    {
                        PageModel.SelectCondtion.Add("BiddingOpeningTimeS", _child.Value);
                    }
                    else if (_child.Key == "BiddingOpeningTimeE")
                    {
                        PageModel.SelectCondtion.Add("BiddingOpeningTimeE", _child.Value);
                    }
                }
            }

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }


            //PageModel.SelectCondtion.Add("IsAudit", 1);
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "b.Id desc";
            }

            var dt = op.GetListInfo(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string FilterBiddingInfoPackage()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id  desc";
            }
            int _parentId = TypeHelper.parseInt(Request.Form["parentId"]);
            int _isFilter = TypeHelper.parseInt(Request.Form["IsFilter"]);
            int _IsProgress = TypeHelper.parseInt(Request.Form["IsProgress"]);

            PageModel.SelectCondtion.Add("IsFilter", _isFilter);
            PageModel.SelectCondtion.Add("Id", _parentId);
            PageModel.SelectCondtion.Add("IsProgress", _IsProgress);

            var dt = op.FilterBiddingInfoPackage(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });

        }
        #endregion
    }
}
