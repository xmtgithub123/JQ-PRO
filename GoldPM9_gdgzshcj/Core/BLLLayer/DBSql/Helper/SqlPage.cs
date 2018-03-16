using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using DataModel.Models;
using System.Data.SqlClient;

namespace DBSql.Helper
{
    public class SqlPage
    {
        public static string ExecPageStrSql(Common.SqlPageInfo condition, string RowColumn, StringBuilder sql)
        {
            StringBuilder strSql = new StringBuilder();
            //默认是分页的
            if (condition.ToPageData == false)
            {
                strSql.Append("SELECT TOP ({PageSize}) * FROM ({strSql}) as DataRecord WHERE [DataRecord].[row_number] > {PageSkip}");
                string RowNumber = ",row_number() OVER (ORDER BY {SelectOrder}) AS [row_number]";
                sql.Replace("Count(1)", RowColumn + RowNumber);
            }
            else
            {
                strSql.Append("SELECT * FROM ({strSql}) as DataRecord ");
                string RowNumber = ",row_number() OVER (ORDER BY {SelectOrder}) AS [row_number]";
                sql.Replace("Count(1)", RowColumn + RowNumber);
            }

            strSql.Replace("{PageSize}", condition.PageSize.ToString());
            int Skip = (condition.PageIndex - 1 < 0 ? 0 : condition.PageIndex - 1) * condition.PageSize;
            strSql.Replace("{PageSkip}", Skip.ToString());
            strSql.Replace("{strSql}", sql.ToString());
            strSql.Replace("{SelectOrder}", condition.SelectOrder.ToString());
            strSql.Append(" ORDER BY  [row_number] ");
            return strSql.ToString();
        }

        public static string ExecNoPageStrSql(Common.SqlPageInfo condition, string RowColumn, StringBuilder sql)
        {
            sql.Replace("Count(1)", RowColumn);
            sql.Append("  ORDER BY " + condition.SelectOrder.ToString());
            return sql.ToString();
        }


        public static string ExecPageStrSql(string RowColumn, StringBuilder sql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * FROM ({strSql}) ");

            sql.Replace("Count(1)", RowColumn);

            strSql.Replace("{strSql}", sql.ToString());
            strSql.Append(" as temptable");

            return strSql.ToString();
        }

        /// <summary>
        /// 执行查询，返回第一条记录第一列的值
        /// </summary>
        public static object ExecuteScalar(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                object val = null;
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, commandParameters);
                val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 准备执行命令
        /// </summary>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null && parm.Value != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }
        //public static DataTable CacheTable(string strSql,string tableName,string cacheKey)
        //{
        //    //检查缓存中是否存在该数据项;
        //    DataTable data = (DataTable)HttpRuntime.Cache[cacheKey];
        //    if (data == null)
        //    {
        //        AggregateCacheDependency dependency = new AggregateCacheDependency();

        //        dependency.Add(new SqlCacheDependency(ConfigurationManager.ConnectionStrings["CacheDataBase"].ToString(), tableName));

        //        data = DBExecute.ExecuteDataTable(DBUtility.DBExecute.ConnectionString, strSql);

        //        HttpRuntime.Cache.Add(cacheKey, data, dependency, DateTime.Now.AddHours(10), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        //    }

        //    return data;
        //}

        public static void CacheRemove(params string[] cacheKey)
        {
            foreach (string key in cacheKey)
            {
                if (HttpRuntime.Cache[key] == null) continue;
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 转义SQL中的限制符号
        /// </summary>
        /// <param name="InPut">要加工的字串</param>
        /// <returns></returns>
        public static string SQL_ESC(string InPut)
        {
            StringBuilder OutPut = new StringBuilder();
            OutPut.Append(InPut);
            OutPut.Replace("[", "[[]");
            OutPut.Replace("%", "[%]");
            OutPut.Replace("_", "[_]");
            OutPut.Replace("'", "''");
            return SQL_Parameters(OutPut.ToString().Trim());
        }

        /// <summary>
        /// 过滤SQL的特殊字符
        /// 防止SQL拼字符漏洞,
        /// </summary>
        /// <param name="InPut">要加工的字串</param>
        /// <returns></returns>
        public static string SQL_Parameters(string InPut)
        {
            StringBuilder OutPut = new StringBuilder();
            OutPut.Append(InPut);
            OutPut.Replace("'", "‘");
            OutPut.Replace("*", "＊");
            OutPut.Replace("-", "－");
            OutPut.Replace("\\", "＼");
            OutPut.Replace("/", "／");
            return OutPut.ToString().Trim();
        }

        public static T ConvertToDataModel<T>(T t, DataRow dr)
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();

            string tempName = "";
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                //判断列是否存在


                if (!dr.Table.Columns.Contains(tempName)) continue;

                // 判断此属性是否有Setter
                if (!pi.CanWrite) continue;

                object value = dr[tempName];
                if (value != DBNull.Value)
                    pi.SetValue(t, value, null);
            }
            return t;
        }

        public static string[] GetDataModelArr<T>(T t)
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();

            List<string> list = new List<string>();
            for (int i = 0; i < propertys.Length; i++)
            {
                PropertyInfo pi = propertys[i];
                list.Add(pi.Name);
            }
            return list.ToArray<string>();
        }


    }
}
