using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System;
using System.Data;

namespace Bussiness.Controllers
{
    [Description("BussContractSub")]
    public class BussContractSubController : CoreController
    {
        private DBSql.Bussiness.BussContractSub op = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Project.ProjSub opProjSub = new DBSql.Project.ProjSub();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubInfo")));
            return View();
        }

        [Description("列表")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubInfo_SJ")));
            return View();
        }

        [Description("列表")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubInfo_GC")));
            return View();
        }

        [Description("创景公司列表")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractSubInfo_CJ")));
            return View();
        }

        [Description("付款合同列表")]
        public ActionResult conSubList()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ContractSubInfo");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            var projectID = Request.Form["projectID"].ToString().Split('x');
            var engineeringNumber = Request.Form["EngineeringNumber"] == null ? "" : Request.Form["EngineeringNumber"].ToString();
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("ContractSubInfo_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_SJ");
                    break;
                case "GC":
                    result = PermissionList("ContractSubInfo_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_GC");
                    break;
                case "CJ":
                    result = PermissionList("ContractSubInfo_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_CJ");
                    break;
                default:
                    result = PermissionList("ContractSubInfo");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub");
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
                    else if (_child.Key == "ConSubDateS")
                    {
                        PageModel.SelectCondtion.Add("ConSubDateS", _child.Value);
                    }
                    else if (_child.Key == "ConSubDateE")
                    {
                        PageModel.SelectCondtion.Add("ConSubDateE", _child.Value);
                    }
                    else if (_child.Key == "ArchNumber")
                    {
                        PageModel.SelectCondtion.Add("ArchNumber", _child.Value);
                    }
                    else if (_child.Key == "ConSubType")
                    {
                        PageModel.SelectCondtion.Add("ConSubType", _child.Value);
                    }
                    else if (_child.Key == "ConSubStatus")
                    {
                        PageModel.SelectCondtion.Add("ConSubStatus", _child.Value);
                    }
                    else if (_child.Key == "ConSubCategory")
                    {
                        PageModel.SelectCondtion.Add("ConSubCategory", _child.Value);
                    }
                }
            }

            if (engineeringNumber!="")
            {
                PageModel.SelectCondtion.Add("EngineeringNumber", engineeringNumber.Trim(','));
            }
            else
            {
                if (projectID.Length > 0)
                {
                    string prId = string.Join(",", projectID).Trim(',');
                    if (prId != "0")
                    {
                        PageModel.SelectCondtion.Add("ProjectID", prId);
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

            if (!string.IsNullOrEmpty(Request.Params["ConSubCustId"]))//统计外协客户合同数量、金额使用
            {
                int ConSubCustId = TypeHelper.parseInt(Request.Params["ConSubCustId"]);
                PageModel.SelectCondtion.Add("ConSubCustId", ConSubCustId);
            }
            var dt = op.GetListInfo(PageModel, this.userInfo);

            if (Request.QueryString["Footer"] != null)
            {
                string row = JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = dt,
                    footer = ShowFooter(PageModel)
                });
                return row;
            }
            else
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = dt
                });
            }
        }
        private object ShowFooter(Common.SqlPageInfo condition)
        {
            condition.PageIndex = 0;
            condition.ToPageData = true;

            DataTable dt = op.GetListInfo(condition);
            List<object> list = new List<object>();
            object a = new
            {
                Footer = 1,
                ConFulfilType = (int)DataModel.ConDealWays.开口,
                ConSubFee = dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["ConSubFee"].ToString())),
                AlreadySumFeeMoney = dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["AlreadySumFeeMoney"].ToString())),
                UnPay = dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["UnPay"].ToString())),
                AlreadySumInvoiceMoney = dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["AlreadySumInvoiceMoney"].ToString())),
                ConSubDate = "合计："
            };
            list.Add(a);
            return list;
        }

        public string jsonSubFeeFact()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            string companyType = Request.Params["CompanyType"].ToString();

            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub");
                    break;
            }

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
                }
            }
            PageModel.SelectCondtion.Add("IsAudit", 1);
            var dt = op.GetListInfo(PageModel, this.userInfo);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        public string jsonFeeSubInvoive()
        {
            //Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //if (!PageModel.SelectOrder.isNotEmpty())
            //{
            //    PageModel.SelectOrder = "Id desc";
            //}

            //var list = op.GetPagedList(p => p.DeleterEmpId == 0, PageModel).ToList().Select(p => new
            //{
            //    p.Id,
            //    p.ConSubNumber,
            //    p.ConSubName,
            //    p.ConSubType,
            //    ConSubTypeName = p.FK_BussContractSub_ConSubType == null ? "" : p.FK_BussContractSub_ConSubType.BaseName,
            //    p.ConSubCategory,
            //    ConSubCategoryName = p.FK_BussContractSub_ConSubCategory == null ? "" : p.FK_BussContractSub_ConSubCategory.BaseName,
            //    p.ConSubStatus,
            //    ConSubStatusName = p.FK_BussContractSub_ConSubStatus == null ? "" : p.FK_BussContractSub_ConSubStatus.BaseName,
            //    p.ConSubCustId,
            //    ConSubCustName = p.FK_BussContractSub_ConSubCustId == null ? "" : p.FK_BussContractSub_ConSubCustId.CustName,
            //    p.ConSubDate,
            //    p.ConSubFee,
            //    AlreadySumInvoiceMoney = p.FK_BussSubFeeInvoice_ConSubId.Where(m => m.ConSubId == p.Id).Sum(m => m.SubFeeInvoiceMoney)

            //});

            //return JavaScriptSerializerUtil.getJson(new
            //{
            //    total = PageModel.PageTotleRowCount,
            //    rows = list
            //});
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
                }
            }

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub");
                    break;
            }

            PageModel.SelectCondtion.Add("IsAudit", 1);
            var dt = op.GetListInfo(PageModel, this.userInfo);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        /// <summary>
        /// 选择外包工程
        /// 返回Json值
        /// </summary>
        /// <param name="projIDs"></param>
        /// <returns></returns>
        public string JsonSelectProjSub(string projIDs)
        {
            DBSql.Project.ProjSub con = new DBSql.Project.ProjSub();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var list = con.GetPagedList(PageModel).Select(p => new
            {
                Id = p.Id,
                ProjNumber = p.SubNumber,
                p.SubName,
                p.SubFee,
                p.SubPlanFinishDate,
                p.SubFactFinishDate,
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion
        /// <summary>
        /// 选择工程
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectProjSub()
        {
            return View();
        }
        #region 新增付款合同中外包项目列表
        /// <summary>
        /// 获取相应的项目信息
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>string projIDs, int NConID
        public string GetProjSubList(FormCollection Tcondition)
        {
            string ProjSubIDs = "";
            if (Tcondition["ProjSubIDs"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs"].ToString();
            }
            else if (Tcondition["ProjSubIDs[]"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs[]"].ToString();
            }
            int[] Id = (from n in ProjSubIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.ProjSub>.True();
            if (Id.Length == 0)
            {
                TWhere = TWhere.And(p => p.Id == -1);
            }
            else
            {
                var Ids = string.Join(",", Id.Select(p => p.ToString()));
                TWhere = TWhere.And(p => Id.Contains(p.Id));
            }
            //TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            var list = new DBSql.Project.ProjSub().GetList(TWhere).Select(p => new
            {
                p.Id,
                p.SubNumber,
                p.SubName,
                ProjNumber = p.FK_ProjSub_ProjID.ProjNumber,
                ProjName = p.FK_ProjSub_ProjID.ProjName,
                p.SubFee,
                p.SubPlanFinishDate,
                p.SubFactFinishDate,
                CustName = p.FK_ProjSub_ColAttType2 != null ? p.FK_ProjSub_ColAttType2.CustName : "",
                ConSubNumber = p.FK_ProjSub_ConSubID != null ? p.FK_ProjSub_ConSubID.ConSubNumber : "",
                ConSubName = p.FK_ProjSub_ConSubID != null ? p.FK_ProjSub_ConSubID.ConSubName : "",
                p.ColAttDate1,
                p.CreationTime,
            }).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list

            });
        }
        #endregion
        #region 添加
        [Description("添加")]
        public ActionResult add(FormCollection fc)
        {
            var model = new BussContractSub();
            model.CreateEmpId = userInfo.EmpID;
            model.CreateDate = DateTime.Now;
            ViewBag.permission = "['add','submit']";
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
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
            IEnumerable<DataModel.Models.ProjSub> list = opProjSub.GetList(p => p.ConSubID == id);
            foreach (var i in list)
            {
                ViewBag.ProjSubIDs += i.Id + ",";
            }
            ViewBag.permission = "['add','submit']";
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
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
                op.UpdateBussContractSubInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "BussContractSub");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BussContractSub");
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
            var model = new BussContractSub();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            try
            {
                var _ProjSubIDs = Request.Form["ProjSubIDs"];
                var _CreateEmpId = TypeHelper.parseInt(Request.Form["CreateEmpId"]);
                var _CustID = TypeHelper.parseInt(Request.Form["ConSubCustId"]);
                op.CreateOrUpdate(model, _ProjSubIDs, _CreateEmpId, _CustID);
                reuslt = 1;
            }
            catch (Exception ex)
            {

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        public string GetConSubCodeCount(string ConSubNumber, int Id)
        {
            if (Id == 0)
            {
                ConSubNumber = ConSubNumber.Trim();
                return op.GetList(p => p.DeleterEmpId == 0 && p.ConSubNumber == ConSubNumber).Count().ToString();
            }
            else
            {
                return op.GetList(p => p.DeleterEmpId == 0 && p.ConSubNumber == ConSubNumber && p.Id != Id).Count().ToString();
            }
        }
    }
}
