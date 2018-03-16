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
    public class IsoFormProjSubPFController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
        private DBSql.Project.ProjSub dbProjSub = new DBSql.Project.ProjSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("项目外委评分列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore")));
            return View();
        }

        [Description("项目外委评分列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_SJ")));
            return View();
        }

        [Description("项目外委评分列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_GC")));
            return View();
        }

        [Description("项目外委评分列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubScore_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetJsonList()
        {
            List<string> result = PermissionList("ProjSubScore");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            string refTable = "IsoFormGrade";
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("ProjSubScore_SJ");
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    refTable = "IsoFormGrade_SJ";
                    break;
                case "GC":
                    result = PermissionList("ProjSubScore_GC");
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    refTable = "IsoFormGrade_GC";
                    break;
                case "CJ":
                    result = PermissionList("ProjSubScore_CJ");
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    refTable = "IsoFormGrade_CJ";
                    break;
                default:
                    result = PermissionList("ProjSubScore");
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    refTable = "IsoFormGrade";
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
                    else if (_child.Key == "CTStartTime")
                    {
                        PageModel.SelectCondtion.Add("CTStartTime", _child.Value);
                    }
                    else if (_child.Key == "CTEndTime")
                    {
                        PageModel.SelectCondtion.Add("CTEndTime", _child.Value);
                    }                   
                    else if (_child.Key == "ProjSubTypeState")
                    {
                        PageModel.SelectCondtion.Add("ProjSubTypeState", _child.Value);
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

            PageModel.SelectCondtion.Add("BussContractSubStatus", 3);//必须为审批结束，才能显示外围合同信息




            var dt = op.GetListInfo(PageModel, (int)DataModel.IsoForm.IsoFormGrade, this.userInfo, refTable);
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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateIsoFormInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoFormGrade");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoForm");
                }
                reuslt = 1;
            }
            catch (Exception ex)
            {
                string s = "";
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(FormCollection fc)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<Root></Root>");
            foreach (string key in fc.AllKeys)
            {
                if (key == "FormID")
                {
                    continue;
                }
                var node = xmlDocument.CreateElement("Item");
                node.SetAttribute("name", key);
                node.InnerText = fc[key].ToString();
                xmlDocument.DocumentElement.AppendChild(node);
            }

            var model = new IsoForm();
            int FormID = int.Parse(fc["FormID"].ToString());
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            int reuslt = 0;
            if (model.FormID > 0)
            {
                model.FormCtlXml = xmlDocument.OuterXml;
                model.LastModificationTime = System.DateTime.Now;
                model.LastModifierEmpId = userInfo.EmpID;
                model.LastModifierEmpName = userInfo.EmpName;
                op.Edit(model);
            }
            else
            {
                model.FormCtlXml = xmlDocument.OuterXml;
                model.RefTable = "ProjSubPF";
                //其他值
                model.FormDate = System.DateTime.Now;
                model.FormName = "项目外委评分";
                model.CreationTime = System.DateTime.Now;
                model.CreatorEmpId = userInfo.EmpID;
                model.CreatorEmpName = userInfo.EmpName;
                model.CreatorDepId = userInfo.EmpDepID;
                model.CreatorDepName = userInfo.EmpDepName;
                model.LastModificationTime = System.DateTime.Now;
                model.LastModifierEmpId = userInfo.EmpID;
                model.LastModifierEmpName = userInfo.EmpName;

                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}