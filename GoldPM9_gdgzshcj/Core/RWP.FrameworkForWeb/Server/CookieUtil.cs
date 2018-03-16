/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using JQ.Util;
using System;
using System.Web;

namespace JQ.Web
{
    /// <summary>
    /// Cookie助手类
    /// </summary>
    public abstract class CookieUtil
    {
        /// <summary>
        /// 判断Cookie是否存在
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static bool isCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName] == null ? false : true;
        }
        /// <summary>
        /// 读取Cookie
        /// </summary>
        /// <param name="CookName">要读取的Cookie名称</param>
        /// <returns></returns>
        public static HttpCookie getCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }
        /// <summary>
        /// 读取Cookie值
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string get(string cookieName)
        {
            HttpCookie myCookie = getCookie(cookieName);
            return (myCookie != null) ? myCookie.Value : string.Empty;
        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        public static void clear(string cookieName)
        {
            HttpCookie myCookie = new HttpCookie(cookieName);
            myCookie.Expires = new DateTime(0x7bf, 5, 0x15);
            saveCookie(myCookie);
        }
        /// <summary>   
        /// 保存Cookie
        /// </summary>   
        /// <param name="CookieName">Cookie名称</param>   
        /// <param name="CookieValue">Cookie值</param>   
        /// <param name="CookieTime">Cookie过期时间(小时),0为关闭页面失效</param>
        public static void saveCookie(string cookieName, string cookieValue, int cookieTime)
        {
            HttpCookie myCookie = new HttpCookie(cookieName);
            myCookie.Value = cookieValue;
            if (cookieTime != 0)
            {
                DateTime now = DateTime.Now;
                myCookie.Expires = now.AddHours(cookieTime);
                //myCookie.HttpOnly = true;
            }
            saveCookie(myCookie);
        }
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="myCookie"></param>
        public static void saveCookie(HttpCookie myCookie)
        {
            if (getCookie(myCookie.Name) != null)
            {
                HttpContext.Current.Response.Cookies.Remove(myCookie.Name); //myCookie.Path = "/";
            }
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="serializeString">序列化字符串</param>
        public static void setCookie(string cookieName, string serializeString)
        {
            setCookie(cookieName, serializeString, 0);
        }
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="serializeString">序列化字符串</param>
        /// <param name="cookHourTime">过期时间 小时</param>
        public static void setCookie(string cookieName, string serializeString, int cookHourTime)
        {
            setCookie(cookieName, serializeString, cookHourTime, EncryptionType.Des);
        }
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="serializeString">序列化字符串</param>
        /// <param name="cookHourTime">过期时间 小时</param>
        /// <param name="enType">加密方式</param>
        public static void setCookie(string cookieName, string serializeString, int cookHourTime, EncryptionType enType)
        {
            string enString = EnHelper.encrypt(serializeString, enType);
            CookieUtil.saveCookie(cookieName, enString, cookHourTime);
        }
        /// <summary>
        /// 获取Cookie并转换对象 使用DES加密方式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="cookieName">cookie名称</param>
        /// <returns></returns>
        public static T forCookie<T>(string cookieName)
        {
            return forCookie<T>(cookieName, EncryptionType.Des);
        }
        /// <summary>
        /// 获取Cookie并转换对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="enType">解密类型</param>
        /// <returns></returns>
        public static T forCookie<T>(string cookieName, EncryptionType deType)
        {
            string cookieValues = CookieUtil.get(cookieName);
            if (string.IsNullOrEmpty(cookieValues))
            {
                return default(T);
            }
            cookieValues = EnHelper.decrypt(cookieValues, deType);
            return JavaScriptSerializerUtil.objectToEntity<T>(cookieValues);
        }
    }
}
