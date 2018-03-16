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
[Description("HRLabourContract")]
public class HRLabourContractController : CoreController
    {
        private DBSql.Hr.HRLabourContract op = new DBSql.Hr.HRLabourContract();
        DBSql.HR.HREmployee empDal = new DBSql.HR.HREmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("HRLabourContract")));
            ViewBag.TimeNow = DateTime.Now.ToString("yyyy-MM-dd");
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

            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "DepID")))
            {
                sqlModel.SelectCondtion.Add("DepID", Common.SunClass.GetRequestVal(queryInfo, "DepID"));
            }
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "EmpStatusID")))
            {
                sqlModel.SelectCondtion.Add("EmpStatusID", Common.SunClass.GetRequestVal(queryInfo, "EmpStatusID"));
            }
            DateTime beginDate = Common.SunClass.ConvertToDateTime(Common.SunClass.GetRequestVal(queryInfo, "StarDateBegin"), Convert.ToDateTime("1900-01-01"));
            sqlModel.SelectCondtion.Add("StarDateBegin", beginDate.ToString("yyyy-MM-dd"));
            DateTime endDate = Common.SunClass.ConvertToDateTime(Request.Params["StarDateEnd"], Convert.ToDateTime("9999-01-01"));
            sqlModel.SelectCondtion.Add("StarDateEnd", endDate.ToString("yyyy-MM-dd"));

            DateTime beginDate1 = Common.SunClass.ConvertToDateTime(Common.SunClass.GetRequestVal(queryInfo, "EndDateBegin"), Convert.ToDateTime("1900-01-01"));
            sqlModel.SelectCondtion.Add("EndDateBegin", beginDate1.ToString("yyyy-MM-dd"));
            DateTime endDate1 = Common.SunClass.ConvertToDateTime(Request.Params["EndDateEnd"], Convert.ToDateTime("9999-01-01"));
            sqlModel.SelectCondtion.Add("EndDateEnd", endDate1.ToString("yyyy-MM-dd"));

            #region 权限控制
            List<string> result = PermissionList("HRLabourContract");

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

            DataTable dt = op.GetHRLabourContractTable(sqlModel);

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
            var model = new HRLabourContract();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id,int empID)
        {
            var model= op.Get(id);
            ViewBag.EmpName = string.Empty;
            ViewBag.EmpID = empID;
            ViewBag.EmpName = empDal.GetHREmployee(empID).EmpName;
            if (model==null)
            {
                model= new DataModel.Models.HRLabourContract();
            }
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
            DateTime starDate = Common.SunClass.ConvertToDateTime(Request.Params["StarDate"], Convert.ToDateTime("1900-01-01"));
            DateTime endDate = Common.SunClass.ConvertToDateTime(Request.Params["EndDate"], Convert.ToDateTime("1900-01-01"));
            if (starDate>endDate)
            {
                return Json(DoResultHelper.doSuccess(-1, "合同开始不能大于合同结束"));
            }
            var model = new HRLabourContract();
            int empID = int.Parse(Request.Params["EmpID"]);
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.EmpID = empID;

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
