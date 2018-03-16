using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Models;
namespace Common
{
    public class PubSql
    {
        public static string ExecPageStrSql(SqlPageInfo condition, string RowColumn, StringBuilder sql)
        {
            if (!condition.ToPageData)
                return ExecStrSql(condition, RowColumn, sql);

            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT TOP ({PageSize}) * FROM ({strSql}) as DataRecord WHERE [DataRecord].[row_number] > {PageSkip}");
            string RowNumber = ",row_number() OVER (ORDER BY {SelectOrder}) AS [row_number]";
            sql.Replace("Count(1)", RowColumn + RowNumber);

            strSql.Replace("{PageSize}", condition.PageSize.ToString());
            int Skip = condition.PageIndex * condition.PageSize;
            strSql.Replace("{PageSkip}", Skip.ToString());
            strSql.Replace("{strSql}", sql.ToString());
            strSql.Replace("{SelectOrder}", condition.SelectOrder.ToString());

            return strSql.ToString();
        }


        private static string ExecStrSql(SqlPageInfo condition, string RowColumn, StringBuilder sql)
        {
            StringBuilder strSql = new StringBuilder();
            sql.Replace("Count(1)", RowColumn);
            if (condition.SelectOrder.Trim() != string.Empty)
            {
                sql.Append(" ORDER BY " + condition.SelectOrder.ToString());
            }
            return sql.ToString();
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

    }
}
