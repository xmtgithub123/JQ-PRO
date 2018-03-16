using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace Bussiness.Controllers
{
    [Description("BussTendersCost")]
    public class BussTendersCostController : CoreController
    {
        private DBSql.Bussiness.BussTendersCost op = new DBSql.Bussiness.BussTendersCost();
        private DBSql.Bussiness.BussTendersInfoDetail TendersInfoDetailDB = new DBSql.Bussiness.BussTendersInfoDetail();
        private DBSql.Bussiness.BussTendersInfo TendersInfoDB = new DBSql.Bussiness.BussTendersInfo();
        private DBSql.Bussiness.BussCustomer dbCustomer = new DBSql.Bussiness.BussCustomer();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("InviteBidFee")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("InviteBidFee");

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
                    else if (_child.Key == "WinState")
                    {
                        PageModel.SelectCondtion.Add("WinState", _child.Value);
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
       
            var dt = op.GetTendersCostList(PageModel);


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
            var model = new BussTendersCost();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var _detail = TendersInfoDetailDB.Get(model.BussTendersInfoDetailID);
            var _cust = dbCustomer.Get(_detail.CustomerID);
            var _Info = TendersInfoDB.Get(model.BussTendersInfoID);
            ViewBag.CustName = _cust.CustName;
            ViewBag.TendersName = _Info.TendersName;
            ViewBag.TendersNumber = _Info.TendersNumber;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;

            try
            {
                op.UpdateTendersCostList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "BussTendersCost");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BussTendersCost");
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
        public ActionResult save(int Id)
        {
            var model = new BussTendersCost();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.BussTendersInfoID = TypeHelper.parseInt(Request.Form["TendersId"]);
            model.BussTendersInfoDetailID = TypeHelper.parseInt(Request.Form["TendersInfoID"]);
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
