using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace JQ.Util
{
    public static class EntityHelpers
    {
        /// <summary>
        /// 两个实体相互之间赋值
        /// </summary>
        public static void EntityToEntity<T>(T entitySource, T entityTarget)
        {
            foreach (PropertyInfo propInfo in typeof(T).GetProperties())
            {
                try
                {
                    propInfo.SetValue(entityTarget, propInfo.GetValue(entitySource, null), null);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// 两个List实体相互之间赋值
        /// </summary>
        public static void ListToList<T>(List<T> entitySource, ref List<T> entityTarget)
        {
            foreach (T item in entitySource)
            {
                entityTarget.Add(item);
            }
        }



        /// <summary>
        /// 两个实体相互之间赋值
        /// </summary>
        public static void EntityToEntity<T, M>(T entitySource, M entityTarget, params string[] filter)
        {
            Type typeT = typeof(T);
            Type typeM = typeof(M);
            PropertyInfo[] propertysT = typeT.GetProperties();
            PropertyInfo[] propertysM = typeM.GetProperties();

            foreach (PropertyInfo pi in propertysM)
            {
                string TargetName = pi.Name;
                try
                {
                    string sourcePropName = TargetName;
                    if (filter.Length > 0)
                    {
                        sourcePropName = TargetName.Replace(filter[0], filter[1]);
                    }

                    object sourcePropValue = typeT.GetProperty(sourcePropName).GetValue(entitySource, null);
                    if (pi.PropertyType != sourcePropValue.GetType())
                    {
                        typeM.GetProperty(TargetName).SetValue(entityTarget, Convert.ChangeType(sourcePropValue, pi.PropertyType), null);
                    }
                    else
                    {
                        typeM.GetProperty(TargetName).SetValue(entityTarget, sourcePropValue, null);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            }
        }

        ///// <summary>
        ///// 两个List实体相互之间赋值
        ///// </summary>
        //public static void ListToList<T, M>(List<T> entitySource, ref List<M> entityTarget)
        //{
        //    foreach (T item in entitySource)
        //    {
        //        EntityToEntity(item, new M() { });
        //        entityTarget.Add(item);
        //    }
        //}
        /// <summary>
        /// 转换成DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            Type ColumnType;
            foreach (PropertyInfo pi in t.GetProperties())
            {
                try
                {
                    Type[] TypeName = pi.PropertyType.GetGenericArguments();
                    if (TypeName.Length == 0)
                    {
                        ColumnType = pi.PropertyType;
                    }
                    else
                    {
                        ColumnType = TypeName[0];
                    }
                    dt.Columns.Add(pi.Name, ColumnType);
                }
                catch (Exception ex)
                {
                    string exmessage = ex.Message;
                }
            }
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    try
                    {
                        if (pi.GetValue(item, null) != null)
                        {
                            dr[pi.Name] = pi.GetValue(item, null);
                        }
                        else
                        {
                            dr[pi.Name] = DBNull.Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        string ErrorMessage = ex.Message;
                    }
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item);
                table.Rows.Add(row);
            }
            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows, IDictionary<string, string> dicCol)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row, dicCol);
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table, IDictionary<string, string> dicCol)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return ConvertTo<T>(rows, dicCol);
        }

        //Convert DataRow into T Object
        public static T CreateItem<T>(DataRow row, IDictionary<string, string> dicCol)
        {
            string columnName;
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    columnName = column.ColumnName;
                    //Get property with same columnName
                    string objFiledName = columnName;
                    if (dicCol.ContainsKey(columnName))
                        objFiledName = dicCol[columnName];

                    PropertyInfo prop = obj.GetType().GetProperty(objFiledName);
                    try
                    {
                        object sourcePropValue = (row[columnName].GetType() == typeof(DBNull))? null : row[columnName];
                        if (prop.PropertyType != sourcePropValue.GetType())
                        {
                            prop.SetValue(obj, Convert.ChangeType(sourcePropValue, prop.PropertyType), null);
                        }
                        else
                        {
                            prop.SetValue(obj, sourcePropValue, null);
                        }
                    }
                    catch
                    {
                        //throw;
                        //Catch whatever here
                    }
                }
            }
            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, prop.PropertyType);

            return table;
        }
    }
}

