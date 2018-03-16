using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;

namespace Pay.Controllers
{
    [Description("绩效密码")]
    public class PayPasssWordController : CoreController
    {
        private DBSql.Sys.AllBaseEmployee op = new DBSql.Sys.AllBaseEmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        [Description("绩效密码修改列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BalancePassWord"))); 
            return View();
        }


        [Description("人员系数配置")]
        public ActionResult listAllEmployee()
        {
            return View();
        }


       
         
        [Description("用户列表json")]
        [HttpPost]
        public string json()
        { 
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedList(PageModel).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        [Description("导入系数用户列表json")]
        [HttpPost]
        public string TransferJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p=>p.PayManageCoeff>0&&p.EmpIsDeleted==false,PageModel).ToList();
            string rowId = Request.QueryString["RowId"];
            if(!string.IsNullOrEmpty(rowId))
            {
                List<int> EmpList = new List<int>();
                string[] Ids = rowId.Split(',');
                foreach(string id in  Ids)
                {
                    EmpList.Add(Convert.ToInt32(id));
                }
               list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p => p.PayManageCoeff > 0 && p.EmpIsDeleted == false&&!EmpList.Contains(p.EmpID), PageModel).ToList();
            }
            var targetList = from item in list
                             select new
                             {
                                 item.EmpID,
                                 item.EmpName,
                                 item.DepartmentName,
                                 item.EmpLogin,
                                 item.PayManageCoeff
                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        [Description("下拉选择")]
        public string employeeJson()
        {
            var listEmp = new DBSql.Sys.AllBaseEmployee().GetList(o => o.EmpIsDeleted == false&&o.PayManageCoeff>0).ToList();
            string rowId = Request.QueryString["RowId"];
            if (!string.IsNullOrEmpty(rowId))
            {
                List<int> EmpList = new List<int>();
                string[] Ids = rowId.Split(',');
                foreach (string id in Ids)
                {
                    EmpList.Add(Convert.ToInt32(id));
                }
                listEmp = new DBSql.Sys.AllBaseEmployee().GetList(p => p.PayManageCoeff > 0 && p.EmpIsDeleted == false && !EmpList.Contains(p.EmpID)).ToList();
            }

            var result = from a in listEmp
                         orderby a.DepartmentOrder ascending,a.EmpOrder ascending
                         select new
                         {
                             EmpID = a.EmpID,
                             EmpName = a.EmpName,
                             EmpDepName = a.DepartmentName
                         };
            return JavaScriptSerializerUtil.getJson(result);
        }
        [Description("修改绩效密码")]
        public ActionResult edits(int id)
        {
            ViewBag.EmpID = id;
            return View("info");
        }

        [Description("个人系数配置")]
        public ActionResult Coeff(int id)
        {
            ViewBag.EmpID = id;
            var op = new DBSql.Sys.BaseEmployee();
            var model = op.Get(id);
            return View("infoCoeff",model);
        }

        #region 保存 绩效系数
        [Description("保存 绩效系数")]
        [HttpPost]
        public ActionResult saveCoeff(int EmpID)
        {
            var op = new DBSql.Sys.BaseEmployee();
            var model = op.Get(EmpID);
            model.MvcSetValue();
            int reuslt = 0;
            if (model.EmpID > 0)
            {
                op.Edit(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.EmpID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.EmpID, "操作成功") : DoResultHelper.doSuccess(model.EmpID, "操作失败");
            return Json(dr);
        }
        #endregion


        #region 保存 绩效密码
        [Description("保存 绩效密码")]
        [HttpPost]
        public ActionResult save(PassWordInfo model)
        {
            
            int empids = TypeHelper.parseInt( Request.Form["EmpID"]);
            var op = new DBSql.Sys.BaseEmployee();
            var emp = new DBSql.Sys.BaseEmployee();
            var baseEmployeeInfo = GetEmpInfo(empids);
            string old = EncryInfo.EncrypPassword(model.oldPassWord.Trim());

            if (baseEmployeeInfo.SalaryPassword != old)
            {
                return Json(DoResultHelper.doSuccess(1,"旧密码输入有误！"));
            }

            baseEmployeeInfo.SalaryPassword = EncryInfo.EncrypPassword(model.newPassWord.Trim());
            int result = emp.UpdateBaseEmployeeInfo(baseEmployeeInfo, null);
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result,"操作成功") : DoResultHelper.doSuccess(result,"操作失败");
            return Json(dr);
        }
        #endregion


        [Description("绩效密码验证")]
        public ActionResult infoPassCheck()
        {
            //ViewBag.EmpID = id;
            return View("infoPassCheck");
        }

            #region 验证绩效密码pass
        [Description("验证绩效密码")]
        [HttpPost]
        public string savePassCheck()
        {
            var emp = new DBSql.Sys.BaseEmployee();
            var SalaryPassword = EncryInfo.EncrypPassword(Request.Form["SalaryPassword"]);
            var model = GetEmpInfo(userInfo.EmpID);
            DoResult dr = new DoResult();
            if (model.SalaryPassword != SalaryPassword)
            { 
                return "0";
            }
            else
            {
                userInfo.EmpIsPay = true; 
            } 
            return "1";
        }
        #endregion


        #region 恢复绩效密码pass
        [Description("恢复绩效密码")]
        [HttpPost]
        public ActionResult RecoverPwd()
        {
            var emp = new DBSql.Sys.BaseEmployee();
            var EmpID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["EmpID"]);
        
            var model = GetEmpInfo(EmpID);
            int reuslt = 0;
            if (model.EmpID > 0)
            {
                model.SalaryPassword = EncryInfo.EncrypPassword("pass");
                reuslt = emp.UpdateBaseEmployeeInfo(model, null);
            }
            
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion


    }
}
