﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;

namespace Hr.Controllers
{
[Description("HRSalary")]
public class HRSalaryController : CoreController
    {
        private DBSql.Hr.HRSalary op = new DBSql.Hr.HRSalary();
        Common.SunClass scDal = new Common.SunClass();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("HRSalary")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo sqlModel = new Common.SqlPageInfo();

            string queryInfo = Request.Form["queryInfo"];
            string s = Common.SunClass.GetRequestVal(queryInfo, "EmpName");
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "EmpName")))
            {
                sqlModel.TextCondtion = Common.SunClass.GetRequestVal(queryInfo, "EmpName");
            }

            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "DepID")))
            {
                sqlModel.SelectCondtion.Add("DepID", Common.SunClass.GetRequestVal(queryInfo, "DepID"));
            }
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "EmpStatusID")))
            {
                sqlModel.SelectCondtion.Add("EmpStatusID", Common.SunClass.GetRequestVal(queryInfo, "EmpStatusID"));
            }
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "HireTypeID")))
            {
                sqlModel.SelectCondtion.Add("HireTypeID", Common.SunClass.GetRequestVal(queryInfo, "HireTypeID"));
            }

            #region 权限控制
            List<string> result = PermissionList("HRVitae");

            if (!result.Contains("allview"))
            {
                if (result.Contains("dep"))
                {
                    sqlModel.SelectCondtion.Add("CreatorDepId", userInfo.EmpDepID);
                }
                else
                {
                    sqlModel.SelectCondtion.Add("CreateEmpId", userInfo.EmpID);
                }
            }

            if (userInfo.EmpName != "admin")
            {
                if (userInfo.EmpName == "陈健明")
                {
                    sqlModel.SelectCondtion.Add("CompanyTS", 1);
                }
                else
                {
                    sqlModel.SelectCondtion.Add("CompanyID", userInfo.CompanyID);
                }
            }
            #endregion

            DataTable dt = op.GetHRSalaryTable(sqlModel);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = sqlModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.EmpName = "";
            var model = new HRSalary();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.EmpName = new DBSql.HR.HREmployee().Get(model.EmpID).EmpName;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            try
            {
                op.UnitOfWork.SaveChanges();
                reuslt = 1;
            }
            catch (Exception)
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
            var model = new HRSalary();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.EmpID = int.Parse(Request.Params["EmpID"]);
            int reuslt = 0;
            if (model.Id > 0)
            {
				op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
