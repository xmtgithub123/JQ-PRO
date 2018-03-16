using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using DataModel;
using System.Xml;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace Bussiness.Controllers
{
    [Description("BussTendersInfo")]
    public class BussTendersInfoController : CoreController
    {
        private DBSql.Bussiness.BussTendersInfo op = new DBSql.Bussiness.BussTendersInfo();
        private DBSql.Bussiness.BussTendersInfoDetail dbTendersInfoDetail = new DBSql.Bussiness.BussTendersInfoDetail();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("InviteBidInfo")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("InviteBidInfo");
            // emp  个人    // dep 部门    // allview  全部   // alledit  维护

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussTendersInfo>.True().And(m => m.DeleterEmpId == 0);

            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }

            var list = op.GetPagedList(TWhere, PageModel).ToList();

            var target = (from item in list
                          select new
                          {
                              item.Id,
                              item.TendersNumber,
                              item.TendersName,
                              item.TenderOpenTime,
                              item.TenderProjectType,
                              TenderProjectTypeName = item.FK_BussTendersInfo_TenderProjectType.BaseName,
                              item.TenderState,
                              TenderStateName = item.FK_BussTendersInfo_TenderState.BaseName,
                              item.TenderRegisterTime,
                              item.TenderScope,
                              item.TendersRemarks,
                              datas = item.FK_BussTendersInfoDetail_BussTendersInfoID.Where(m => m.BussTendersInfoID == item.Id).ToList().Select(m => new { m.Id, m.FK_BussTendersInfoDetail_CustomerID.CustName, m.BusinessPoints, m.TechnologyPoints, m.TotalityPoints, m.FK_BussTendersInfoDetail_WinState.BaseName, WinTime = m.WinTime.ToString("yyyy-MM-dd"), CostID = m.FK_BussTendersCost_BussTendersInfoDetailID.FirstOrDefault(p => p.BussTendersInfoDetailID == m.Id) == null ? 0 : m.FK_BussTendersCost_BussTendersInfoDetailID.FirstOrDefault(p => p.BussTendersInfoDetailID == m.Id).Id })
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
            var _baseDataList = dbBaseData.GetDataSetByEngName("BidState").ToList();

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

            var model = new BussTendersInfo();
            model.TenderRegisterTime = DateTime.Now;
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var _baseDataList = dbBaseData.GetDataSetByEngName("BidState").ToList();

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
            ViewBag.CreatorEmpId = model.CreatorEmpId;
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
                op.UpdateBussTendersInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "BussTender");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BussTendersInfo");
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
            var model = new BussTendersInfo();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

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

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        #region combogrid数据获取
        public string json_Search()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion


        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string TenderUnitInfo()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id ";
            }
            int _parentId = TypeHelper.parseInt(Request.Form["Id"]);
            var list = dbTendersInfoDetail.GetPagedList(p => p.BussTendersInfoID == _parentId, PageModel);
            var target = (from item in list
                          select new
                          {
                              item.Id,
                              item.BussTendersInfoID,
                              item.FK_BussTendersInfoDetail_BussTendersInfoID.TendersNumber,
                              item.FK_BussTendersInfoDetail_BussTendersInfoID.TendersName,
                              item.CustomerID,
                              CustName = item.FK_BussTendersInfoDetail_CustomerID != null ? item.FK_BussTendersInfoDetail_CustomerID.CustName : "",
                              item.BusinessPoints,
                              item.TechnologyPoints,
                              item.TotalityPoints,
                              item.WinState,
                              WinStateName = item.FK_BussTendersInfoDetail_WinState != null ? item.FK_BussTendersInfoDetail_WinState.BaseName : "",
                              item.WinTime,
                          }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });

        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string FilterTenderUnitInfo()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "d.Id  desc";
            }
            int _parentId = TypeHelper.parseInt(Request.Form["parentId"]);
            PageModel.SelectCondtion.Add("Filter", "1");
            PageModel.SelectCondtion.Add("Id", _parentId);
            var dt = op.FilterTenderUnitInfo(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });

        }
        #endregion



        public ActionResult ListInfo()
        {
            return View("ListInfo");
        }

        public int GetTendersCount(string TendersNumber, int Id)
        {
            int Count = op.GetTendersCount(TendersNumber, Id);
            return Count;
        }

        #region 筛选列表json
        [Description("筛选列表json")]
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
                    if (_child.Key == "TendersLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "TenderStartTime")
                    {
                        PageModel.SelectCondtion.Add("TenderStartTime", _child.Value);
                    }
                    else if (_child.Key == "TenderEndTime")
                    {
                        PageModel.SelectCondtion.Add("TenderEndTime", _child.Value);
                    }
                }
            }
            PageModel.SelectCondtion.Add("IsAudit", 1);
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
        public string jsonList()
        {
            List<string> result = PermissionList("InviteBidInfo");

            // emp  个人    // dep 部门    // allview  全部   // alledit  维护

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
                    else if (_child.Key == "TenderOpenTimeS")
                    {
                        PageModel.SelectCondtion.Add("TenderOpenTimeS", _child.Value);
                    }
                    else if (_child.Key == "TenderOpenTimeE")
                    {
                        PageModel.SelectCondtion.Add("TenderOpenTimeE", _child.Value);
                    }
                    else if (_child.Key == "TenderState")
                    {
                        PageModel.SelectCondtion.Add("TenderState", _child.Value);
                    }
                }
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "t.Id desc";
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

            var dt = op.jsonList(PageModel, this.userInfo);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion
    }
}