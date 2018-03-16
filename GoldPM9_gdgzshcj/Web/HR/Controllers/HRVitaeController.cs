using System.Collections.Generic;
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
[Description("HRVitae")]
public class HRVitaeController : CoreController
    {
        private DBSql.Hr.HRVitae op = new DBSql.Hr.HRVitae();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
        Common.SunClass scDal = new Common.SunClass();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("HRVitae")));
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
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "EmpName")))
            {
                sqlModel.TextCondtion = Common.SunClass.GetRequestVal(queryInfo, "EmpName");
            }

            DateTime beginDate = Common.SunClass.ConvertToDateTime(Common.SunClass.GetRequestVal(queryInfo, "StarDateBegin"), Convert.ToDateTime("1900-01-01"));
            sqlModel.SelectCondtion.Add("StarDateBegin", beginDate.ToString("yyyy-MM-dd"));
            DateTime endDate = Common.SunClass.ConvertToDateTime(Request.Params["StarDateEnd"],Convert.ToDateTime("9999-01-01"));
            sqlModel.SelectCondtion.Add("StarDateEnd", endDate.ToString("yyyy-MM-dd"));

            DateTime beginDate1 = Common.SunClass.ConvertToDateTime(Common.SunClass.GetRequestVal(queryInfo, "EndDateBegin"), Convert.ToDateTime("1900-01-01"));
            sqlModel.SelectCondtion.Add("EndDateBegin", beginDate1.ToString("yyyy-MM-dd"));
            DateTime endDate1 = Common.SunClass.ConvertToDateTime(Request.Params["EndDateEnd"], Convert.ToDateTime("9999-01-01"));
            sqlModel.SelectCondtion.Add("EndDateEnd", endDate1.ToString("yyyy-MM-dd"));

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

            DataTable dt = op.GetHRVitaeTable(sqlModel);

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
            var model = new HRVitae();
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
            string empid = Request.Params["EmpID"];
            var model = new HRVitae();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.EmpID = int.Parse(empid);
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
