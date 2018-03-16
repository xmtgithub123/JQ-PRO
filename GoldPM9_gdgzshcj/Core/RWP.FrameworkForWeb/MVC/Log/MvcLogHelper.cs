/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using JQ.Log;
using System;
using System.Web.Mvc;

namespace JQ.Web
{
    /// <summary>
    /// Mvc日志助手
    /// </summary>
    public static class MvcLogHelper
    {
        /// <summary>
        /// 记录Mvc的异常
        /// </summary>
        /// <param name="filterContext"></param>
        public static void writeException(ExceptionContext filterContext)
        {
            writeMessage(filterContext, filterContext.Exception.ToString());
        }

        /// <summary>
        /// 记录Mvc的错误信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="str"></param>
        public static void writeMessage(ControllerContext filterContext, string str)
        {
            string area = filterContext.RouteData.DataTokens["area"] as string;
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;
            string newLine = Environment.NewLine;
            string logstr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + newLine;
            logstr += string.Format("发生异常 Area:{0} Controller：{1} Action：{2}", area, controller, action) + newLine;
            logstr += "上次请求：" + WebRoot.urlReferrer + newLine;
            logstr += "当前请求：" + WebRoot.rawUrl + newLine;
            logstr += str;
            LogHelper.Error(logstr);
        }
    }
}
