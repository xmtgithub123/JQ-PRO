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
[Description("HREmployee")]
public class HREmployeeController : CoreController
    {
        private DBSql.HR.HREmployee op = new DBSql.HR.HREmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            #region 权限控制
            string strPer = "[";
            List<string> lisPer = PermissionList("HREmployee");
            if (lisPer.Contains("allEdit"))
            {
                strPer += "'add','edit','del,btnChoice'";
            }
            else
            {
                if (lisPer.Contains("add"))
                {
                    strPer += "'add','btnChoice',";
                }

                if (lisPer.Contains("edit"))
                {
                    strPer += "'edit',";
                }

                if (lisPer.Contains("del"))
                {
                    strPer += "'del',";
                }
            }

            if (lisPer.Contains("export"))
            {
                strPer += " 'export',";
            }

            strPer += "'moreSearch',";
            strPer = strPer.Trim(',');
            strPer += "]";
            ViewBag.strPer = strPer;
            #endregion

            return View();
        }

        [Description("员工选择列表")]
        public ActionResult selectHREmp()
        {
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
            if (!string.IsNullOrEmpty(Common.SunClass.GetRequestVal(queryInfo, "CreationTime")))
            {
                DateTime beginDate = TypeHelper.parseDateTime(Common.SunClass.GetRequestVal(queryInfo, "CreationTime"));
                sqlModel.SelectCondtion.Add("BeginDate", beginDate.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]);
                sqlModel.SelectCondtion.Add("EndDate", endDate.ToString("yyyy-MM-dd"));
            }

            #region 权限控制
            List<string> result = PermissionList("HREmployee");

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

            DataTable dt = op.GetHREmployeeTable(sqlModel);

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
            var model = new HREmployee();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
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
            var model = new HREmployee();
            int oldDays = 0, newDays = 0;
            if (Id > 0)
            {
                model = op.Get(Id);
                oldDays = model.VacationDays;
            }
            model.MvcSetValue();
            newDays = model.VacationDays;
            model.CompanyID = userInfo.CompanyID;
            int reuslt = 0;
            if (model.Id > 0)
            {
				op.Edit(model);
                model.VacationDays1 += (newDays - oldDays) > 0 ? (newDays - oldDays) : 0;
            }
            else
            {
                model.VacationDays1 = model.VacationDays;
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

        #region 同步系统人员
        [Description("同步系统人员")]
        [HttpPost]
        public ActionResult SyncEmployee()
        {
            int result = new DBSql.Sys.BaseEmployee().SyncEmployee();
            if (result > 0)
            {
                doResult = DoResultHelper.doSuccess(result,"同步成功");
            }else
            {
                doResult = DoResultHelper.doSuccess(result, "同步失败");
            }

            return Json(doResult);
        }
        #endregion
    }
}
