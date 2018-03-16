using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace DBSql.PubFlow
{
    public static class DataTableExterns
    {
        /// <summary>
        /// 将DataTable转化为List
        /// </summary>
        /// <typeparam name="T">要转化的类型</typeparam>
        /// <param name="dataTable">元参数</param>
        /// <param name="isDispose">是否在转化完后释放DataTable，默认为True</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dataTable, bool isDispose = true) where T : new()
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("DataTable不能为NULL");
            }
            List<T> result = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            Dictionary<string, string> cache = null;
            foreach (DataRow dr in dataTable.Rows)
            {
                T t = new T();
                if (cache == null)
                {
                    cache = new Dictionary<string, string>();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (propertyInfo.CanWrite && !cache.ContainsKey(propertyInfo.Name) && dataTable.Columns[propertyInfo.Name] != null)
                        {
                            cache.Add(propertyInfo.Name, propertyInfo.Name);
                            SetPropertyValue(propertyInfo, t, dr[propertyInfo.Name], null);
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (propertyInfo.CanWrite && cache.ContainsKey(propertyInfo.Name))
                        {
                            try
                            {
                                SetPropertyValue(propertyInfo, t, dr[cache[propertyInfo.Name]], null);
                            }
                            catch { }
                        }
                    }
                }
                result.Add(t);
            }
            if (isDispose)
            {
                dataTable.Dispose();
            }
            cache = null;
            properties = null;
            return result;
        }

        public static void SetPropertyValue(PropertyInfo propertyInfo, object source, object value, params object[] index)
        {
            if (value == null || value == DBNull.Value || !propertyInfo.CanWrite)
            {
                return;
            }
            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyInfo.SetValue(source, Convert.ChangeType(value, propertyInfo.PropertyType.GetGenericArguments()[0]), null);
                return;
            }
            propertyInfo.SetValue(source, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        /// <summary>
        /// 将DataTable中的第一行转换成实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dataTable">源数据</param>
        /// <param name="isDispose">是否在使用完后释放DataTable</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <returns></returns>
        public static T ToInstance<T>(this DataTable dataTable, bool isDispose = true) where T : new()
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                throw new ArgumentNullException("DataTable不能为NULL或者DataTable中没有包含任何行");
            }
            T t = new T();
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (dataTable.Columns[propertyInfo.Name] != null)
                {
                    SetPropertyValue(propertyInfo, t, dataTable.Rows[0][propertyInfo.Name], null);
                }
            }
            if (isDispose)
            {
                dataTable.Dispose();
            }
            properties = null;
            return t;
        }
    }
}
