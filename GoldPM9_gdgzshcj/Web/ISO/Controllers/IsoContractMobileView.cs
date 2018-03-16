using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using System.Xml;

namespace ISO.Controllers
{
    public partial class IsoContractMobileController : MobileController
    {
        /// <summary>
        /// 新增 开票表单
        /// </summary>
        /// <returns></returns>
        public ActionResult FeeInvoice()
        {
            var _InvoiceType = new DBSql.Sys.BaseData().GetDataSetByEngName("InvoiceType");
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(_InvoiceType.Select(p => new { p.BaseID, p.BaseName }));

            var model = new IsoForm();
            model.CreatorEmpName = userInfo.EmpName;
            model.CreationTime = System.DateTime.Now;

            string page = "FeeInvoice";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "FeeInvoice_SJ";
                    break;
                case "GC":
                    page = "FeeInvoice_GC";
                    break;
                case "CJ":
                    page = "FeeInvoice_CJ";
                    break;
                default:
                    page = "FeeInvoice";
                    break;
            }

            return View(page, model);
        }

        public ActionResult EditInvoice(int id)
        {
            var _InvoiceType = new DBSql.Sys.BaseData().GetDataSetByEngName("InvoiceType");
            ViewData["ConFeeType"] = JavaScriptSerializerUtil.getJson(_InvoiceType.Select(p => new { p.BaseID, p.BaseName }));

            var model = op.Get(id);

            string page = "FeeInvoice";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "FeeInvoice_sj";
                    break;
                case "GC":
                    page = "FeeInvoice_gc";
                    break;
                case "CJ":
                    page = "FeeInvoice_cj";
                    break;
                default:
                    page = "FeeInvoice";
                    break;
            }

            return View(page, model);
        }

       
        public ActionResult AddSubFeeFact()
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeFact";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeFact_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeFact_GC";
                    break;
                case "CJ":
                    permission = PermissionList("ContractSubFee_CJ");
                    page = "SubFeeFact_CJ";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeFact";
                    break;
            }

            var model = new IsoForm();
            ViewBag.AllowSave = (permission.Contains("edit") || permission.Contains("alledit")) ? "1" : "0";



            return View(page, model);
        }
        public ActionResult EditSubFeeFact(int id)
        {
            List<string> permission = PermissionList("ContractSubFee");

            string page = "SubFeeFact";
            string companyTyp = Request.Params["CompanyType"].ToString();
            switch (companyTyp)
            {
                case "SJ":
                    permission = PermissionList("ContractSubFee_SJ");
                    page = "SubFeeFact_SJ";
                    break;
                case "GC":
                    permission = PermissionList("ContractSubFee_GC");
                    page = "SubFeeFact_GC";
                    break;
                default:
                    permission = PermissionList("ContractSubFee");
                    page = "SubFeeFact";
                    break;
            }

            var model = op.Get(id);
            ViewBag.AllowSave = (permission.Contains("edit") || permission.Contains("alledit")) ? "1" : "0";
            return View(page, model);
        }

    }
}