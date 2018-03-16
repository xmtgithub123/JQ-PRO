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
    [Description("BussBiddingCost")]
    public class BussBiddingCostController : CoreController
    {
        private DBSql.Bussiness.BussBiddingInfo BiddingInfoDB = new DBSql.Bussiness.BussBiddingInfo();
        private DBSql.Bussiness.BussBiddingInfoPackage BiddingInfoPackageDB = new DBSql.Bussiness.BussBiddingInfoPackage();
        private DBSql.Bussiness.BussBiddingCost op = new DBSql.Bussiness.BussBiddingCost();
        private DBSql.Sys.BaseData BaseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("投标费用管理(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFee")));
            return View();
        }

        [Description("投标费用管理(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFee_SJ")));
            return View();
        }

        [Description("投标费用管理(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFee_GC")));
            return View();
        }

        [Description("投标费用管理(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFee_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetCostList()
        {
            List<string> result = new List<string>();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    result = PermissionList("BidFee_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    result = PermissionList("BidFee_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    result = PermissionList("BidFee_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    result = PermissionList("BidFee");
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
                    else if (_child.Key == "WinTimeS")
                    {
                        PageModel.SelectCondtion.Add("WinTimeS", _child.Value);
                    }
                    else if (_child.Key == "WinTimeE")
                    {
                        PageModel.SelectCondtion.Add("WinTimeE", _child.Value);
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
                    else if (_child.Key == "BidBondBack")
                    {
                        PageModel.SelectCondtion.Add("BidBondBack", _child.Value);
                    }
                    else if (_child.Key == "PerformanceBondBack")
                    {
                        PageModel.SelectCondtion.Add("PerformanceBondBack", _child.Value);
                    }
                }
            }
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "c.Id desc";
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
            var dt = BiddingInfoDB.GetCostList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        #endregion

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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateBussBiddingCostList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "BussBiddingCost");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BussBiddingCost");
                }
                reuslt = 1;
            }
            catch {

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussBiddingCost();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.BussBiddingInfoID = TypeHelper.parseInt(Request.Form["BiddingId"]);
            model.BussBiddingInfoPackageID = TypeHelper.parseInt(Request.Form["BiddingInfoID"]);
            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;

                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }

            }
         
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
