using DataModel.Models;
using JQ.Web;
using JQ.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JQ.BPM.Configuration;

namespace JQ.Web
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class CoreController : Controller
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public DataModel.EmpSession userInfo = null;

        /// <summary>
        /// 用户session名称
        /// </summary>
        private string userSessionName = "Employee";

        /// <summary>
        /// 用户Cook名称
        /// </summary>
        private string userCookies = "UID";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["JQDownload"] != null)
            {
                //无需验证客户端的下载
                return;
            }
            var session = Session[userSessionName];
            if (session == null)
            {
                int uID = TypeHelper.parseInt(CookieUtil.get(userCookies), 0);
                if (uID <= 0)
                {
                    reglogin(filterContext);
                    return;
                }
                else
                {
                    var empAgentID = TypeHelper.parseInt(CookieUtil.get("AgentID"));
                    if (empAgentID > 0)
                    {
                        //获取出
                        try
                        {
                            SaveSessionInfo(new DBSql.Sys.BaseEmpAgen().Get(empAgentID), uID);
                        }
                        catch
                        {
                            reglogin(filterContext);
                            return;
                        }
                    }
                    else
                    {
                        SaveSessionInfo(new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(uID));
                    }
                }
            }
            else
            {
                userInfo = session as DataModel.EmpSession;
            }
            if (userInfo == null || userInfo.EmpID <= 0)
            {
                reglogin(filterContext);
                return;
            }
            BPMContext.CurrentUser = new BPM.Configuration.User()
            {
                Account = userInfo.EmpName,
                AgentUserID = userInfo.AgenEmpID,
                AgentUserName = userInfo.AgenEmpName,
                DepartmentID = userInfo.EmpDepID,
                DepartmentName = userInfo.EmpDepName,
                Name = userInfo.EmpName,
                ID = userInfo.EmpID
            };
            ViewBag.divid = Request["divid"];
            ViewBag.iframeID = Request["iframeID"];
            ViewBag.loadingType = Request["loadingType"];
            ViewBag.dgID = Request["dgID"];
            ViewBag.sitePath = Url.Content("~");
            string thems = string.IsNullOrEmpty(userInfo.EmpThemesName) ? "skin-bluelight" : "skin-" + userInfo.EmpThemesName;
            Session.Add("themes", thems);
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
        }

        /// <summary>
        /// 在执行Action之前调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var permissionAttributes = filterContext.Controller.GetType().GetCustomAttributes(typeof(JQ.Util.PermissionAttribute), false).ToList();
            permissionAttributes.AddRange(filterContext.ActionDescriptor.GetCustomAttributes(typeof(JQ.Util.PermissionAttribute), false));
            if (permissionAttributes.Count == 0)
            {
                return;
            }
            var names = new List<string>();
            foreach (JQ.Util.PermissionAttribute permissionAttribute in permissionAttributes)
            {
                foreach (var name in permissionAttribute.PermissionNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    names.AddRange(permissionAttribute.PermissionNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            if (names.Count == 0)
            {
                return;
            }
            if (!new DBSql.Sys.BaseMenu().getPermit(userInfo.EmpID, names.ToArray()))
            {
                filterContext.Result = Json(DoResultHelper.doUnAuthorized(), JsonRequestBehavior.AllowGet);
                return;
            }
        }



        #region 获取通用方法

        /// <summary>
        /// 获取GoldFile下载 虚拟目录
        /// </summary>
        /// <returns></returns>
        public string GetGoldFileUrl()
        {
            return string.Format("http://{0}/GoldFile", this.Request.ServerVariables["LOCAL_ADDR"] == "::1" ? "127.0.0.1" : this.Request.ServerVariables["LOCAL_ADDR"]);
        }

        /// <summary>
        /// 判断用户是否存在某权限
        /// </summary>
        /// <param name="permissionName">权限ENG</param>
        /// <returns></returns>
        public bool ContainPermission(string permissionName)
        {
            return new DBSql.Sys.BaseMenu().getPermit(userInfo.EmpID, permissionName);
        }

        /// <summary>
        /// 权限列表Select 1 ^ 2 ^ 4 ^ 8 ^ 16 & 4
        /// 添加权、修改权、删除权、导出、个人查看权、部门查看权、全部查看权、全部维护权
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
            foreach (int item in Enum.GetValues(typeof(DataModel.PermitType)))
            {
                string strName = Enum.GetName(typeof(DataModel.PermitType), item);//获取名称
                int strVaule = item;//获取值
                if ((item & permit) == item)
                {
                    result.Add(strName);
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

            string s = Request.QueryString[key];

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

        /// <summary>
        /// 获取Session参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetSessionValue<T>(string key)
        {
            T result = default(T);

            object s = Session[key];

            if (s != null)
            {
                try
                {
                    result = (T)Convert.ChangeType(s, typeof(T));
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 得到用户对象（缓存）
        /// </summary>
        public DataModel.Models.BaseEmployee GetEmpInfo(int EmpID = 0)
        {
            if (EmpID == 0) return new DataModel.Models.BaseEmployee();
            return new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(EmpID);
        }

        /// <summary>
        /// 得到基础数据对象（缓存）
        /// </summary>
        public DataModel.Models.BaseData GetBaseInfo(int BaseID = 0)
        {
            if (BaseID == 0) return new DataModel.Models.BaseData();
            return new DBSql.Sys.BaseData().GetBaseDataInfo(BaseID);
        }

        /// <summary>
        /// 得到基础数据对象（缓存）
        /// </summary>
        public List<string> GetBaseName(string BaseIDs)
        {
            if (BaseIDs == "") return new List<string>();

            var Arr = BaseIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var AllData = new DBSql.Sys.BaseData().AllData;
            List<string> result = new List<string>();
            foreach (var item in AllData)
            {
                if (Arr.Contains(item.BaseID.ToString()))
                {
                    result.Add(item.BaseName);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取部门主任 管理专业ID列表
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public List<int> GetManagerSpecID(string EmpID)
        {
            var list = new DBSql.Sys.BaseData().GetDataSetByEngName("Special");
            var Spec = list.Where(s => s.BaseAtt2 == EmpID).Select(s => s.BaseID).ToList();
            if (Spec.Count == 0) return null;
            return Spec;
        }

        /// <summary>
        /// 移除对应session键值
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveSessionKey(params string[] keys)
        {
            if (keys == null) return;

            foreach (string c in keys)
            {
                if (Session[c] == null) continue;

                Session.Remove(c);
            }
        }

        /// <summary>
        /// 保存用户EmpSession
        /// </summary>
        /// <param name="emp">当前登录的用户信息</param>
        /// <param name="agentEmp">当前登录的代理用户信息</param>
        public void SaveSessionInfo(DataModel.Models.BaseEmployee emp, DataModel.Models.BaseEmployee agentEmp = null)
        {
            var empSession = new DBSql.Sys.BaseEmployee().SaveSessionInfo(Session, emp, agentEmp);
            userInfo = empSession;
        }

        /// <summary>
        /// 代理登录的用户登录数据
        /// </summary>
        /// <param name="baseEmpAgent">当前代理登录信息</param>
        /// <param name="empID">验证身份信息的empID</param>
        public void SaveSessionInfo(DataModel.Models.BaseEmpAgen baseEmpAgent, int empID = 0)
        {
            //验证当前代理是否还有效
            var current = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            if (baseEmpAgent == null || baseEmpAgent.AgenStatus == 1 || baseEmpAgent.FromEmpID == 0 || current < baseEmpAgent.DateLower || current > baseEmpAgent.DateUpper || (empID > 0 && baseEmpAgent.FromEmpID != empID) || (userInfo != null && userInfo.EmpID != baseEmpAgent.ToEmpID))
            {
                throw new Common.JQException("该委托授权已经失效！");
            }
            var empDAL = new DBSql.Sys.BaseEmployee();
            DataModel.Models.BaseEmployee emp = empDAL.GetBaseEmployeeInfo(baseEmpAgent.FromEmpID);
            if (emp == null)
            {
                throw new Common.JQException("委托用户无效！");
            }
            var agentEmp = empDAL.GetBaseEmployeeInfo(baseEmpAgent.ToEmpID);
            if (agentEmp == null)
            {
                throw new Common.JQException("受委托用户无效！");
            }
            SaveSessionInfo(emp, agentEmp);
            userInfo.AgenMenu = "";
            userInfo.AgenFlow = "";
            if (baseEmpAgent.AgenType == 0 || baseEmpAgent.AgenType == 1)
            {
                //菜单授权
                userInfo.AgenMenu = baseEmpAgent.AgenMenu;
            }
            if (baseEmpAgent.AgenType == 0 || baseEmpAgent.AgenType == 2)
            {
                //流程授权
                userInfo.AgenFlow = baseEmpAgent.AgenFlowRefTable;
            }
            if (baseEmpAgent.AgenType == 0)
            {
                //全部
                userInfo.AgenTypeID = -1;
            }
            else
            {
                userInfo.AgenTypeID = baseEmpAgent.AgenType;
            }
            CookieUtil.saveCookie("UID", emp.EmpID.ToString(), 24);
            CookieUtil.saveCookie("AgentID", baseEmpAgent.BaseEmpAgenID.ToString(), 24);
        }

        #endregion

        #region 重新登录
        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="filterContext"></param>
        private void reglogin(AuthorizationContext filterContext)
        {
            if (isAjaxRequest(filterContext))
            {
                //DoResult dr = DoResultHelper.doError("登录已过期，请重新登录！");
                //dr.url = Url.Content("~/Login");
                //JsonResult _json = new JsonResult
                //{
                //    Data = dr,
                //    ContentType = null,
                //    ContentEncoding = null,
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //};
                filterContext.Result = new HttpStatusCodeResult(499);
                filterContext.HttpContext.Response.Write(Url.Content("~/Login"));
                //filterContext.Result = _json;
            }
            else
            {
                filterContext.Result = new RedirectResult(Url.Content("~/Login"));
            }
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip == "::1" ? "127.0.0.1" : ip;
        }
        #endregion

        #region 判断是否为ajax提交
        /// <summary>
        /// 判断是否为ajax提交
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool isAjaxRequest(AuthorizationContext filterContext)
        {
            string sheader = filterContext.HttpContext.Request.Headers["X-Requested-With"];
            return (sheader != null && sheader == "XMLHttpRequest") ? true : false;
        }
        #endregion


        #region 设置审批列表的权限
        /// <summary>
        /// 设置审批列表的权限
        /// </summary>
        /// <param name="FlowRefTable"></param>
        /// <param name="RefID"></param>
        /// <param name="CreateEmpId"></param>
        /// <param name="CurrentEmpId"></param>
        /// <returns></returns>
        public int SetFlowStatus(string FlowRefTable, int RefID, int CreateEmpId, int CurrentEmpId)
        {
            int flowStatusId = 0;
            DataModel.Models.Flow flowModel = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefTable == FlowRefTable && p.FlowRefID == RefID).FirstOrDefault();
            if (flowModel != null)
            {
                flowStatusId = flowModel.FlowStatusID;
                DataModel.Models.FlowNode flowNode = new DBSql.PubFlow.FlowNode().GetList(m => m.FlowID == flowModel.Id && m.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到).FirstOrDefault();
                if (flowNode != null)
                {
                    if (flowNode.FlowNodeEmpId == CreateEmpId && flowNode.FlowNodeOrder == 1 && CreateEmpId == CurrentEmpId)   //轮到的人为创建人且是第一个节点，显示为修改
                    {
                        flowStatusId = 0;
                    }
                    else
                    {
                        if (flowNode.FlowNodeEmpId == CurrentEmpId)
                        {
                            flowStatusId = 1;//轮到的人为当前用户，显示处理
                        }
                    }
                }
            }
            else
            {
                if (CreateEmpId != CurrentEmpId)
                {
                    flowStatusId = -1;//表示查看
                }
            }
            return flowStatusId;
        }
        #endregion
    }
}
