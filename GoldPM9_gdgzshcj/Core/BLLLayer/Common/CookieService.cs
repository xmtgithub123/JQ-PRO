using System;
using System.Web;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using System.Collections;
using System.Text.RegularExpressions;

namespace Common
{
    public class CookieService
    {
        #region Private Methods

        private static string GetCookieValue(string key)
        {
            if ((HttpContext.Current.Request.Cookies[key] != null))
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
            return string.Empty;
        }

        #endregion

        #region Public Methods

        public static void SetCookieValue(string key, string value, int expiration)
        {
            if ((HttpContext.Current.Request.Cookies[key] != null))
            {
                HttpContext.Current.Response.Cookies[key].Value = value;
                HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(Convert.ToDouble(expiration));
            }
            else
            {
                HttpCookie Cookie = new HttpCookie(key, value);
                Cookie.Expires = DateTime.Now.AddDays(Convert.ToDouble(expiration));
                HttpContext.Current.Response.Cookies.Add(Cookie);
            }
        }

        public static string GetCookieAsString(string key, string DefaultValue)
        {
            string Result = GetCookieValue(key);
            if (Result != string.Empty)
            {
                return Result;
            }
            return DefaultValue;
        }

        public static bool GetCookieAsBool(string key, bool DefaultValue)
        {
            string Result = GetCookieValue(key);
            if (Result != string.Empty)
            {
                return Convert.ToBoolean(Result);
            }
            return DefaultValue;
        }

        public static int GetCookieAsInt(string key, int DefaultValue)
        {
            string Result = GetCookieValue(key);
            if (Result != string.Empty)
            {
                return Convert.ToInt32(Result);
            }
            return DefaultValue;
        }

        #endregion
    }
}
