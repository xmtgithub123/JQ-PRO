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
using System.Linq.Expressions;

namespace Pay.Controllers
{
    [Description("PayBalanceSum")]
    public class PayBalanceSumController : CoreController
    {
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Pay.PayBalanceLot lot = new DBSql.Pay.PayBalanceLot();
        private DBSql.Pay.PayBalanceEngineering engi = new DBSql.Pay.PayBalanceEngineering();
        private DBSql.Project.Project pro = new DBSql.Project.Project();
        DBSql.Pay.PayBalanceUserAccount op = new DBSql.Pay.PayBalanceUserAccount();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult PayStatistics()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OldBalanceSum")));
            List<string> permission = PermissionList("OldBalanceSum");
            ViewBag.Permit = (int)DataModel.PermitType.allview;//默认赋值全部查看权限
            if (!permission.Contains("allview") && !permission.Contains("dep"))
            {
                ViewBag.Permit = (int)DataModel.PermitType.emp;//view中判断使用
                ViewBag.DeptId = userInfo.EmpDepID;
            }
            if (!permission.Contains("allview") && permission.Contains("dep"))//部门查看权利
            {
                ViewBag.Permit = (int)DataModel.PermitType.dep;//设置combobox时使用
                ViewBag.DeptId = userInfo.EmpDepID;
            }

            return View();
        }
        #endregion



        #region 用户绩效统计列表json
        [Description("用户绩效统计列表json")]
        [HttpPost]
        public string json()
        {
            List<string> permission = PermissionList("OldBalanceSum");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedList(PageModel).ToList();
            string EmpDeptID = Request.QueryString["EmpDeptID"];
            if(!string.IsNullOrEmpty(EmpDeptID))
            {
                int deptID = TypeHelper.parseInt(Request.QueryString["EmpDeptID"]);
                list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p=>p.EmpDepID==deptID,PageModel).ToList();
            }
            //判定当前的权限是个人、部门、全部查看权利
            if(!permission.Contains("allview")&&!permission.Contains("dep"))
            {
                list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p=>p.EmpID==userInfo.EmpID,PageModel).ToList();//个人 查看全权
            }
            if(!permission.Contains("allview")&& permission.Contains("dep"))//部门查看权利
            {
                list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p => p.EmpDepID == userInfo.EmpDepID, PageModel).ToList();//部门查看权
            }
            var td = from n in list
                     select new
                     {
                        n.EmpID,
                        n.DepartmentID,
                        n.EmpDepID,
                        n.DepartmentName,
                        n.EmpName,
                        BalanceMoney = sum(n.EmpID),
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = td
            });
        }
        #endregion
         
        /// <summary>
        /// 获取个人统计 
        /// </summary>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public decimal sum(int empid)
        {
            DBSql.Pay.PayBalanceUserAccount account = new DBSql.Pay.PayBalanceUserAccount();
            decimal sum = 0.00M;
            sum = account.GetList(p => p.BalanceLotID != 0 && p.EmpId == empid).Select(s => s.BalanceMoney).Sum();
            return sum;
        }

         
        #region   用户绩效统计明细列表
        [Description("用户绩效统计明细列表")]
        public string Sumjson(FormCollection Tcondtion)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            List<string> permission = PermissionList("OldBalanceSum");
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "BalanceLotID desc";
            }
          
            if (!string.IsNullOrEmpty(Request.QueryString["EmpID"]))
            {
                int EmpID = TypeHelper.parseInt(Request.QueryString["EmpID"]);
                PageModel.SelectCondtion.Add("EmpID", EmpID);
            }
            if (!string.IsNullOrEmpty(Request.Form["KeyJQSearch"]))
            {
                string KeyJQSearch = Request.Form["KeyJQSearch"];
                PageModel.SelectCondtion.Add("KeyJQSearch", KeyJQSearch);
            }
            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];
            if (!string.IsNullOrEmpty(startTime)&&TypeHelper.isDateTime(startTime))
            {
                PageModel.SelectCondtion.Add("startTime", TypeHelper.parseDateTime(startTime));
            }
            if (!string.IsNullOrEmpty(endTime)&&TypeHelper.isDateTime(endTime))
            {
                PageModel.SelectCondtion.Add("endTime", TypeHelper.parseDateTime(endTime).AddDays(1));
            }
            //判定当前的权限是个人、部门、全部查看权利
            if (!permission.Contains("allview") && !permission.Contains("dep"))
            {
                PageModel.SelectCondtion.Add("UserID",userInfo.EmpID);//个人 查看全权
            }
            if (!permission.Contains("allview") && permission.Contains("dep"))//部门查看权利
            {
                PageModel.SelectCondtion.Add("DeptID",userInfo.EmpDepID);//部门查看权
            }

            Expression<Func<DataModel.Models.PayBalanceUserAccount, bool>> func = op.GetFun(PageModel);
            var list = op.GetPagedList(func, PageModel).ToList();

            var targetList = from item in list
                             let empMode = emp.Get(item.EmpId)
                             let lotModel = lot.Get(item.BalanceLotID)
                             let engiModel = engi.Get(item.BalanceEngineeringID)
                             orderby item.BalanceLotID descending   //, item.BalanceAmount descending
                             //where engi.GetList(p => p.Id == item.BalanceEngineeringID && p.EngineeringID == EngiID && p.BalanceState == (int)DataModel.BalanceStatus.已结算).Count() > 0//已结算信息
                             select new
                             { 
                                 item.Id,
                                 item.BalanceEngineeringID,
                                 item.BalanceLotID,
                                 BalanceLotName = lotModel == null ? "" : lotModel.BalanceLotName,//批次名称
                                 BalanceDate = lotModel == null ? new DateTime(1900, 1, 1) : lotModel.BalanceDate,//批次日期
                                 EngAmountArrange = engiModel == null ? 0 : engiModel.EngAmountArrange,//分配产值
                                 item.EmpId,
                                 TechEmpName = empMode == null ? "" : empMode.EmpName,//计算技术人员
                                 item.BalanceReason,//来源
                                 item.BalanceAmount,//技术人员计算产值
                                 item.BalanceMoney,//金额
                                 item.BalanceNote,//备注信息
                                 ProjNumber = item.FK_PayBalanceUserAccount_BalanceEngineeringID == null ? "" :  pro.Get( item.FK_PayBalanceUserAccount_BalanceEngineeringID.EngineeringID).ProjNumber,
                                ProjName  =  item.FK_PayBalanceUserAccount_BalanceEngineeringID == null ? "" :  pro.Get( item.FK_PayBalanceUserAccount_BalanceEngineeringID.EngineeringID).ProjName,
                          
                             };
          
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion
         
    }
}
