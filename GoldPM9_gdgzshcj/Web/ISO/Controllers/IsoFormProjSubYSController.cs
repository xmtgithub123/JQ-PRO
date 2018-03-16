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
    public class IsoFormProjSubYSController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("项目外委验收列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubCheck")));
            return View();
        }

        [Description("项目外委验收列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubCheck_SJ")));
            return View();
        }

        [Description("项目外委验收列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubCheck_GC")));
            return View();
        }

        [Description("项目外委验收列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubCheck_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetJsonList()
        {
            List<string> result = PermissionList("ProjSubCheck");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            string IsoFormRefTable = "IsoFormAccept";
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("ProjSubCheck_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_SJ");
                    IsoFormRefTable = "IsoFormAccept_SJ";
                    break;
                case "GC":
                    result = PermissionList("ProjSubCheck_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_GC");
                    IsoFormRefTable = "IsoFormAccept_GC";
                    break;
                case "CJ":
                    result = PermissionList("ProjSubCheck_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub_CJ");
                    IsoFormRefTable = "IsoFormAccept_CJ";
                    break;
                default:
                    result = PermissionList("ProjSubCheck");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussContractSub");
                    IsoFormRefTable = "IsoFormAccept";
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
                    else if (_child.Key == "ColAttDate1S")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate1S", _child.Value);
                    }
                    else if (_child.Key == "ColAttDate1E")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate1E", _child.Value);
                    }
                    else if (_child.Key == "ColAttDate2S")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate2S", _child.Value);
                    }
                    else if (_child.Key == "ColAttDate2E")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate2E", _child.Value);
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



            var dt = op.GetListInfo(PageModel, (int)DataModel.IsoForm.IsoFormAccept, this.userInfo, IsoFormRefTable);
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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateIsoFormInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoFormAccept");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoForm");
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
                model.RefTable = "ProjSubYS";
                //其他值
                model.FormName = "项目外委验收";
                model.LastModificationTime = System.DateTime.Now;
                model.LastModifierEmpId = userInfo.EmpID;
                model.LastModifierEmpName = userInfo.EmpName;

                op.Edit(model);
            }
            else
            {
                model.FormCtlXml = xmlDocument.OuterXml;
                model.RefTable = "ProjSubYS";
                //其他值
                model.FormDate = System.DateTime.Now;
                model.FormName = "项目外委验收";
                model.CreationTime = System.DateTime.Now;
                model.CreatorEmpId = userInfo.EmpID;
                model.CreatorDepId = userInfo.EmpDepID;
                model.CreatorEmpName = userInfo.EmpName;
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