using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System;

namespace ISO.Controllers
{
    public partial class IsoFormProjSubPFMobileController : MobileController
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
            string CompanyName = "";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    CompanyName = "设计公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_SJ")));
                    break;
                case "GC":
                    page = "info_gc";
                    CompanyName = "工程公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_GC")));
                    break;
                case "CJ":
                    page = "info_cj";
                    CompanyName = "创景公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_CJ")));
                    break;
                default:
                    page = "info";
                    CompanyName = "广东分院"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore")));
                    break;
            }

            ViewBag.ConSubNumber = "";
            ViewBag.ConSubName = "";
            ViewBag.ConSubFee = "";
            ViewBag.HZSJName = "";
            ViewBag.CompanyName = CompanyName;
            ViewBag.CustName = "";
            ViewBag.ProjName = "";

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem items1 = new SelectListItem(); items1.Value = "1"; items1.Text = "我方选定"; items.Add(items1);
            SelectListItem items2 = new SelectListItem(); items2.Value = "2"; items2.Text = "业主方选定"; items.Add(items2);
            SelectListItem items3 = new SelectListItem(); items3.Value = "3"; items3.Text = "其他"; items.Add(items3);
            ViewBag.items = new SelectList(items, "Value", "Text");


            List<SelectListItem> itemsA = new List<SelectListItem>();
            SelectListItem itemA1 = new SelectListItem(); itemA1.Value = "1"; itemA1.Text = "优秀"; itemsA.Add(itemA1);
            SelectListItem itemA2 = new SelectListItem(); itemA2.Value = "2"; itemA2.Text = "一般"; itemsA.Add(itemA2);
            SelectListItem itemA3 = new SelectListItem(); itemA3.Value = "3"; itemA3.Text = "较差"; itemsA.Add(itemA3);
            ViewBag.itemsA = new SelectList(itemsA, "Value", "Text");

            List<SelectListItem> itemsB = new List<SelectListItem>();
            SelectListItem itemB1 = new SelectListItem(); itemB1.Value = "1"; itemB1.Text = "优秀"; itemsB.Add(itemB1);
            SelectListItem itemB2 = new SelectListItem(); itemB2.Value = "2"; itemB2.Text = "一般"; itemsB.Add(itemB2);
            SelectListItem itemB3 = new SelectListItem(); itemB3.Value = "3"; itemB3.Text = "较差"; itemsB.Add(itemB3);
            ViewBag.itemsB = new SelectList(itemsB, "Value", "Text");


            List<SelectListItem> itemsC = new List<SelectListItem>();
            SelectListItem itemC1 = new SelectListItem(); itemC1.Value = "1"; itemC1.Text = "优秀"; itemsC.Add(itemC1);
            SelectListItem itemC2 = new SelectListItem(); itemC2.Value = "2"; itemC2.Text = "一般"; itemsC.Add(itemC2);
            SelectListItem itemC3 = new SelectListItem(); itemC3.Value = "3"; itemC3.Text = "较差"; itemsC.Add(itemC3);
            ViewBag.itemsC = new SelectList(itemsC, "Value", "Text");

            List<SelectListItem> itemsD = new List<SelectListItem>();
            SelectListItem itemsD1 = new SelectListItem(); itemsD1.Value = "1"; itemsD1.Text = "优秀"; itemsD.Add(itemsD1);
            SelectListItem itemsD2 = new SelectListItem(); itemsD2.Value = "2"; itemsD2.Text = "一般"; itemsD.Add(itemsD2);
            SelectListItem itemsD3 = new SelectListItem(); itemsD3.Value = "3"; itemsD3.Text = "较差"; itemsD.Add(itemsD3);
            ViewBag.itemsD = new SelectList(itemsD, "Value", "Text");
            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.contentXml = model.FormCtlXml;
            ViewBag.permission = "['add','submit']";
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string CompanyName = "";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj"; CompanyName = "设计公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_SJ")));
                    break;
                case "GC":
                    page = "info_gc"; CompanyName = "工程公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_GC")));
                    break;
                case "CJ":
                    page = "info_cj"; CompanyName = "创景公司"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_CJ")));
                    break;
                default:
                    page = "info"; CompanyName = "广东分院"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore")));
                    break;
            }

            ViewBag.ConSubNumber = "";
            ViewBag.ConSubName = "";
            ViewBag.ConSubFee = "";
            ViewBag.HZSJName = "";
            ViewBag.CompanyName = CompanyName;
            ViewBag.CustName = "";


            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(model.FormCtlXml);

            XmlNodeList listContents1 = xmlDocument.SelectNodes("Root/Item[@name='items']");
            XmlNode node1 = (XmlNode)listContents1[0];

            XmlNodeList listContents2 = xmlDocument.SelectNodes("Root/Item[@name='itemsA']");
            XmlNode node2 = (XmlNode)listContents2[0];

            XmlNodeList listContents3 = xmlDocument.SelectNodes("Root/Item[@name='itemsB']");
            XmlNode node3 = (XmlNode)listContents3[0];

            XmlNodeList listContents4 = xmlDocument.SelectNodes("Root/Item[@name='itemsC']");
            XmlNode node4 = (XmlNode)listContents4[0];

            XmlNodeList listContents5 = xmlDocument.SelectNodes("Root/Item[@name='itemsD']");
            XmlNode node5 = (XmlNode)listContents5[0];

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem items1 = new SelectListItem(); items1.Value = "1"; items1.Text = "我方选定"; items.Add(items1);
            SelectListItem items2 = new SelectListItem(); items2.Value = "2"; items2.Text = "业主方选定"; items.Add(items2);
            SelectListItem items3 = new SelectListItem(); items3.Value = "3"; items3.Text = "其他"; items.Add(items3);
            ViewBag.items = new SelectList(items, "Value", "Text", null != node1 ? node1.InnerText : "");
            ViewBag.stritems = null != node1 ? node1.InnerText.TrimEnd(',') : "";

            List<SelectListItem> itemsA = new List<SelectListItem>();
            SelectListItem itemA1 = new SelectListItem(); itemA1.Value = "1"; itemA1.Text = "优秀"; itemsA.Add(itemA1);
            SelectListItem itemA2 = new SelectListItem(); itemA2.Value = "2"; itemA2.Text = "一般"; itemsA.Add(itemA2);
            SelectListItem itemA3 = new SelectListItem(); itemA3.Value = "3"; itemA3.Text = "较差"; itemsA.Add(itemA3);
            ViewBag.itemsA = new SelectList(itemsA, "Value", "Text", null != node2 ? node2.InnerText : "");
            ViewBag.stritemsA = null != node2 ? node2.InnerText.TrimEnd(',') : "";

            List<SelectListItem> itemsB = new List<SelectListItem>();
            SelectListItem itemB1 = new SelectListItem(); itemB1.Value = "1"; itemB1.Text = "优秀"; itemsB.Add(itemB1);
            SelectListItem itemB2 = new SelectListItem(); itemB2.Value = "2"; itemB2.Text = "一般"; itemsB.Add(itemB2);
            SelectListItem itemB3 = new SelectListItem(); itemB3.Value = "3"; itemB3.Text = "较差"; itemsB.Add(itemB3);
            ViewBag.itemsB = new SelectList(itemsB, "Value", "Text", null != node3 ? node3.InnerText : "");
            ViewBag.stritemsB = null != node3 ? node3.InnerText.TrimEnd(',') : "";


            List<SelectListItem> itemsC = new List<SelectListItem>();
            SelectListItem itemC1 = new SelectListItem(); itemC1.Value = "1"; itemC1.Text = "优秀"; itemsC.Add(itemC1);
            SelectListItem itemC2 = new SelectListItem(); itemC2.Value = "2"; itemC2.Text = "一般"; itemsC.Add(itemC2);
            SelectListItem itemC3 = new SelectListItem(); itemC3.Value = "3"; itemC3.Text = "较差"; itemsC.Add(itemC3);
            ViewBag.itemsC = new SelectList(itemsC, "Value", "Text", null != node4 ? node4.InnerText : "");
            ViewBag.stritemsC = null != node4 ? node4.InnerText.TrimEnd(',') : "";

            List<SelectListItem> itemsD = new List<SelectListItem>();
            SelectListItem itemsD1 = new SelectListItem(); itemsD1.Value = "1"; itemsD1.Text = "优秀"; itemsD.Add(itemsD1);
            SelectListItem itemsD2 = new SelectListItem(); itemsD2.Value = "2"; itemsD2.Text = "一般"; itemsD.Add(itemsD2);
            SelectListItem itemsD3 = new SelectListItem(); itemsD3.Value = "3"; itemsD3.Text = "较差"; itemsD.Add(itemsD3);
            ViewBag.itemsD = new SelectList(itemsD, "Value", "Text", null != node5 ? node5.InnerText : "");
            ViewBag.stritemsD = null != node5 ? node5.InnerText.TrimEnd(',') : "";

            var _projSub = dbProjSub.Get(model.ProjID);
            var _project = dbProject.Get(_projSub.ProjID);
            ViewBag.ProjName = _project.ProjName;
            return View(page, model);
        }
        #endregion
    }
}
