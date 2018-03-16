using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Bussiness.Controllers
{
    public partial class BussContractSubMobileController : MobileController
    {
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

          
            var createEmpName = dbBaseEmployee.GetQuery(x => x.EmpID == model.CreateEmpId).FirstOrDefault();
            ViewBag.CreateEmpName = createEmpName.EmpName;

            ViewData["CreateEmpName"] = dbBaseEmployee.GetQuery(x => x.EmpID == model.CreateEmpId).FirstOrDefault().EmpName;
            //var creatorEmpName = dbBaseEmployee.GetQuery(x => x.EmpID == model.CreatorEmpId).FirstOrDefault();
            //ViewBag.CreatorEmpName = creatorEmpName.EmpName;


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
    }
}
