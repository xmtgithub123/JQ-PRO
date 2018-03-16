using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Web.UI;

using System.Web.SessionState;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
namespace Common
{
    public static class ExtensionMethods
    {
        public static T Value<T>(this string value)
        {
            T result = default(T);
            if (value != null)
            {
                value = value.Trim();
                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                catch { }
            }
            return result;
        }

        public static string TrimAll(this string value)
        {
            return value.Replace(" ", "").Replace("\r\n", "");
        }
        public static DateTime DateTimeOrDefault(this string value)
        {
            DateTime dt = DateTime.Parse("1900-01-01");
            if (value != null)
            {
                value = value.Trim();
                try
                {
                    dt = DateTime.Parse(value);
                }
                catch { }
            }
            return dt;
        }
        /// <summary>
        /// 将枚举转换为Int
        /// </summary>
        /// <param name="enu"></param>
        /// <returns>返回对应的 int 值</returns>
        public static int ToInt(this Enum enu)
        {
            return Convert.ToInt32(enu);
        }
        public static T Value<T>(this object value)
        {
            T result = default(T);
            if (value != null)
            {
                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                catch { }
            }
            return result;
        }


        /// <summary>
        /// 将时间转换为字符串，“1900-01-01”时 返回“”
        ///     默认格式：yyyy-MM-dd
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this DateTime dt)
        {
            return dt == DateTime.Parse("1900-01-01") ? "" : dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 将时间转换为字符串，“1900-01-01”时 返回“”
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this DateTime dt, string format)
        {
            return dt == DateTime.Parse("1900-01-01") ? "" : dt.ToString(format);
        }
        /// <summary>
        /// 获取Session值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Value<T>(this HttpSessionState Session, string key)
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

        /// <summary> 将字符串 格式化成文件夹可以使用的名称
        /// 
        /// </summary>
        /// <param name="fileName">原始文件夹名称</param>
        /// <returns>格式化后名称</returns>
        public static string FormatFileName(this string fileName)
        {
            fileName = fileName.Replace(@"/", "／");
            fileName = fileName.Replace(@"\", "＼");
            fileName = fileName.Replace(@":", "：");
            fileName = fileName.Replace(@"?", "？");
            fileName = fileName.Replace(@"*", "＊");
            fileName = fileName.Replace(@"<", "〈");
            fileName = fileName.Replace(@">", "〉");
            fileName = fileName.Replace("\"", "“");
            fileName = fileName.Replace(@"|", "｜");
            fileName = fileName.Replace("'", "＇");
            fileName = fileName.Replace(" ", "　");
            fileName = fileName.Replace("#", "＃");
            return fileName;
        }
        /// <summary>
        /// 将 Hashtable 转换为 XElement
        /// </summary>
        /// <param name="ht">需要转换的 Hashtable</param>
        /// <returns>转换后的 XElement</returns>
        public static XElement ConvertToXElement(this Hashtable ht, XElement xe)
        {
            if (ht != null)
            {
                var xor = new XElement("OR");

                foreach (DictionaryEntry item in ht)
                {

                    if (item.Value != null && item.Value.ToString() != "")
                    {
                        xor.Add(new XAttribute(item.Key.ToString(), item.Value));
                    }

                }
                xe.Add(xor);
            }
            return xe;
        }
        /// <summary>
        /// 将 Hashtable 转换为 XElement
        /// </summary>
        /// <param name="xe">需要转换的 XElement</param>
        /// <returns>转换后的 Hashtable</returns>
        public static Hashtable ConvertToHashtable(this XElement xe)
        {
            var ht = new Hashtable();
            if (xe != null)
            {
                var list = from x in xe.Attributes()
                           select new
                           {
                               x.Name,
                               x.Value,
                           };
                foreach (var item in list)
                {
                    ht.Add(item.Name.ToString(), item.Value);
                }
            }
            return ht;
        }

        




        /// <summary>
        /// 将文字html编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string str)
        {
            return new Helper().HtmlEncode(str);
        }
        /// <summary>
        /// 将文字html解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string str)
        {
            return new Helper().HtmlDecode(str);
        }










        public static IList<T> OrderBy<T>(this IList<T> source, string propertyNames) where T : class
        {
            string[] propertyNameArray = propertyNames.Split(';');
            bool IsSecondOrder = false;
            for (int i = 0; i < propertyNameArray.Length; i++)
            {
                string[] propertyArray = propertyNameArray[i].Split(',');
                string propertyName = propertyArray[0];
                bool ascending = true;

                if (propertyArray.Count() > 1)
                {
                    if (propertyArray[1].ToLower().Trim() == "desc")
                    {
                        ascending = false;
                    }
                    else
                    {
                        ascending = true;
                    }
                }
                var methodName = !IsSecondOrder ? (ascending ? "OrderBy" : "OrderByDescending") : (ascending ? "ThenBy" : "ThenByDescending");
                IsSecondOrder = true;
                source = ApplyOrder<T>(source, propertyName, methodName);
            }
            return (IList<T>)source;
        }

        static IList<T> ApplyOrder<T>(IList<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(IList).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IList<T>)result;
        }


    }

    public class Helper : System.Web.UI.Page
    {
        /// <summary>
        /// 将文字html编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string HtmlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            } 
            return Server.HtmlEncode(str).Replace("\r\n", "<br>");
        }
        /// <summary>
        /// 将文字html解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string HtmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return Server.HtmlDecode(str.Replace("<br>", "\r\n"));
        }
    }




}
