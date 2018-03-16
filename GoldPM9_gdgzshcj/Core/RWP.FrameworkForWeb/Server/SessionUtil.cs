/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using JQ.Util;
using System.Web;

namespace JQ.Web
{
    /// <summary>
    /// Session助手
    /// </summary>
    public abstract class SessionUtil
    {
        /// <summary>
        /// 判断Session是否存在
        /// </summary>
        /// <param name="SessionName"></param>
        /// <returns></returns>
        public static bool isSession(string sessionName)
        {
            return HttpContext.Current.Session[sessionName] == null ? false : true;
        }
        /// <summary>
        /// 读取Session
        /// </summary>
        /// <param name="SessionName"></param>
        /// <returns></returns>
        public static string getSessionValue(string sessionName)
        {
            return isSession(sessionName) ? HttpContext.Current.Session[sessionName].ToString() : string.Empty;
        }
        /// <summary>
        /// 保存Session
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="Value"></param>
        public static void saveSession(string keyName, string Value)
        {
            HttpContext.Current.Session[keyName] = Value;
        }
        /// <summary>
        /// 序列化对象后保存到session
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="obj"></param>
        public static void setSeeion(string keyName, object obj)
        {
            SessionUtil.saveSession(keyName, JavaScriptSerializerUtil.getJson(obj));
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="SessionName"></param>
        public static void delSession(string SessionName)
        {
            if (isSession(SessionName)) HttpContext.Current.Session.Remove(SessionName);
        }
        /// <summary>
        /// 获取Session并转换对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sessionName">session名称</param>
        /// <returns></returns>
        public static T forSession<T>(string sessionName)
        {
            string sessionValues = SessionUtil.getSessionValue(sessionName);
            return JavaScriptSerializerUtil.objectToEntity<T>(sessionValues);
        }
    }
}
