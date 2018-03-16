using DataModel.Models;
using JQ.Web;
using JQ.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JQ.Web
{
    public class MobileController : Controller
    {
        private DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();

        public DataModel.EmpSession userInfo = null;// 用户信息
        private string userSessionName = "Employee";// 用户session名称
        private string userCookies = "UID";// 用户Cook名称

        /// <summary>
        /// 控制器访问验证
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //var session = Session[userSessionName];
            //if (session == null)
            //{
            userInfo = CheckEmpLogin();
            //}
            //else
            //{
            //    userInfo = session as DataModel.EmpSession;
            //}
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public DataModel.EmpSession CheckEmpLogin()
        {
            string account = Request.Params["a"] ?? "";
            string password = Request.Params["p"] ?? "";
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                throw new Common.JQException("登录账户无效");
            }
            if (!dbBaseEmployee.CheckEmpPassword(account.Trim(), password.Trim().ToUpper()))
            {
                throw new Common.JQException("账户登录失败");
            }
            var emp = dbBaseEmployee.GetBaseEmployeeInfo(account.Trim());
            var empSession = dbBaseEmployee.SaveSessionInfo(Session, emp);
            CookieUtil.saveCookie("UID", emp.EmpID.ToString(), 24);
            return empSession;
        }

        /// <summary>
        /// 权限列表Select 1 ^ 2 ^ 4 ^ 8 ^ 16 & 4
        /// 添加权1、修改权2、删除权4、导出8、个人查看权16、部门查看权、全部查看权、全部维护权
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public List<string> PermissionList(string permissionName)
        {
            List<string> result = new List<string>();
            var menu = new DBSql.Sys.BaseMenu().getPermitMenu(userInfo.EmpID, permissionName);

            if (menu == null) return result;
            result.Add(permissionName);
            var permit = menu.MenuPermissions;
            bool IsAll = false;
            if (((int)DataModel.PermitType.alledit & permit) == (int)DataModel.PermitType.alledit)
            {
                IsAll = true;
            }
            foreach (int item in Enum.GetValues(typeof(DataModel.PermitType)))
            {
                string strName = Enum.GetName(typeof(DataModel.PermitType), item);//获取名称
                int strVaule = item;//获取值
                //如果有维护权就不要判断是不是有权限了
                if ((!IsAll))
                {
                    if ((item & permit) == item)
                    {
                        result.Add(strName);
                    }
                }
                else
                {
                    if (item < 16)
                    {
                        result.Add(strName);
                    }
                    else
                    {
                        if ((item & permit) == item)
                        {
                            result.Add(strName);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取页面传入参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetRequestValue<T>(string key)
        {
            T result = default(T);

            string s = Request.Params[key];

            if (s != null)
            {
                try
                {
                    s = s.Trim();
                    if (result is string)
                        result = (T)Convert.ChangeType(Server.UrlDecode(s), typeof(T));
                    else
                        result = (T)Convert.ChangeType(s, typeof(T));

                }
                catch { }
            }

            return result;
        }
    }
}
