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
    public partial class BussContractMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussContract();
            model.CreateDate = System.DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            model.CreateEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";

            ViewBag.EmpId = userInfo.EmpID.ToString();
            ViewBag.EmpName = userInfo.EmpName.ToString();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");

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

        /// <summary>
        /// 新增子项合同
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [Description("新增子项合同")]
        public ActionResult AddChild(int parentId)
        {
            var model = op.Get(parentId);
            var NewModel = new DataModel.Models.BussContract();
            NewModel.FatherID = model.Id;
            model.Id = 0;
            //手动将父合同的信息，赋值给子合同
            NewModel.ConMainNumber = model.ConNumber;
            NewModel.ConNumber = model.ConNumber;
            NewModel.ConName = model.ConName;
            NewModel.ConType = model.ConType;
            NewModel.ConFulfilType = model.ConFulfilType;
            NewModel.ConStatus = model.ConStatus;
            NewModel.CustName = model.CustName;
            NewModel.CustLinkManName = model.CustLinkManName;
            NewModel.CustLinkManTel = model.CustLinkManTel;
            NewModel.CustLinkManWeb = model.CustLinkManWeb;
            NewModel.ConDate = System.DateTime.Now;

            ViewBag.EmpId = userInfo.EmpID.ToString();

            NewModel.CreateDate = System.DateTime.Now;
            NewModel.CreateEmpName = userInfo.EmpName;
            ViewBag.CreatorEmpId = userInfo.EmpID;


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

            return View(page, NewModel);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Bussiness.BusProjContractRelation().GetList(p => p.ConID == id).Select(p => p.ProjID.ToString()));
            ViewBag.permission = "['add','submit']";
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.EmpId = userInfo.EmpID.ToString();
            ViewBag.EmpName = userInfo.EmpName.ToString();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");


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
