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
using System.Collections;

namespace Project.Controllers
{
    public partial class ProjSubMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {

            Hashtable ht = new Hashtable();
            ht.Add("1", "是");
            ht.Add("2", "否");
            ViewBag.SubQualifiedDirectory = new SelectList(ht, "key", "value");

            var model = new ProjSub();
            model.TableNumber = "表SCX03－01 ";
            ViewData["SubEmpId1"] = model.SubEmpId.ToString();
            ViewData["SubEmpId2"] = model.SubEmpId.ToString();
            model.CreationTime = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepId = userInfo.EmpDepID;
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    {
                        page = "info_sj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_SJ")));
                    }
                    break;
                case "GC":
                    {
                        page = "info_gc"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_GC")));
                    }
                    break;
                case "CJ":
                    {
                        page = "info_cj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_CJ")));
                    }
                    break;
                default:
                    {
                        page = "info"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister")));
                    }
                    break;
            }
            ViewData["SubEmpName"] = "";
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem();
            item1.Value = "1"; item1.Text = "是"; items.Add(item1);
            SelectListItem item2 = new SelectListItem();
            item2.Value = "2"; item2.Text = "否"; items.Add(item2);
            ViewBag.SubQualifiedDirectory = new SelectList(items, "Value", "Text", model.SubQualifiedDirectory);

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {


            var model = op.Get(id);
            ViewData["SubEmpId1"] = model.SubEmpId.ToString();
            ViewData["SubEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.SubEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.SubEmpId).EmpDepID.ToString();
            ViewData["SubEmpName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.SubEmpId).FirstOrDefault().EmpName;
            var _porject = project.Get(model.ProjID);
            ViewBag.ProjInfo = "项目编号：" + (_porject != null ? _porject.ProjNumber : "") + ",项目名称:" + (_porject != null ? _porject.ProjName : "");
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    {
                        page = "info_sj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_SJ")));
                    }
                    break;
                case "GC":
                    {
                        page = "info_gc"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_GC")));
                    }
                    break;
                case "CJ":
                    {
                        page = "info_cj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_CJ")));
                    }
                    break;
                default:
                    {
                        page = "info"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister")));
                    }
                    break;
            }
            //model.SubFactFinishDate


            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem(); item1.Value = "1"; item1.Text = "是"; items.Add(item1);
            SelectListItem item2 = new SelectListItem(); item2.Value = "2"; item2.Text = "否"; items.Add(item2);
            ViewBag.SubQualifiedDirectory = new SelectList(items, "Value", "Text", model.SubQualifiedDirectory);

            return View(page, model);
        }
        #endregion
    }
}
