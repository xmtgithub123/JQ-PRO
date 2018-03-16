using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System;

namespace ISO.Controllers
{
    public partial class IsoFormProjSubYSMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
            model.ColAttDate1 = DateTime.Now;
            model.ColAttDate2 = DateTime.Now;
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
            //ViewBag.contentXml = model.FormCtlXml.Replace("\r\n", @"<br/>");
            ViewBag.contentXml = model.FormCtlXml;
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
                    page = "info_sj";
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
