using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DAL;
using System.Linq.Dynamic;
namespace DBSql.Sys
{
    public class BaseRestDay : EFRepository<DataModel.Models.BaseRestDay>
    {

        public  IEnumerable GetBaseRestDayInfos1()
        {
            string strSql = "select *  from  BaseRestDay ";
            return SqlQuery(strSql);
        }

        public int DeleteBaseRestDayInfos(string BaseDayIDs)
        {
            string strSql = "delete from  BaseRestDay  where BaseDayID in (" + BaseDayIDs.Trim(',') + ")";
            return DBExecute.ExecuteNonQuery(DBExecute.ConnectionString, strSql);
        }

        public List<dynamic> GetList(DateTime startTime, DateTime endTime)
        {
            var datas = this.GetQuery().Where(m => startTime <= m.BaseDay && endTime >= m.BaseDay).Select(m => new { id = m.BaseDayID, start_date = m.BaseDay, end_date = m.BaseDay, text = "节假日"}).ToList<dynamic>();
            var result = new List<dynamic>();
            foreach (var data in datas)
            {
                result.Add(new { id = data.id, start_date = data.start_date.ToString("yyyy-MM-dd HH:mm"), end_date = data.end_date.ToString("yyyy-MM-dd HH:mm"), text = data.text });
            }
            return result;
        }

        public DataTable GetBaseRestDayList(Common.SqlPageInfo condition)
        {
            string RowColumn = "* ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from BaseRestDay BRD  where 1=1 ");
            SqlParameter[] par = { 
                                  new SqlParameter("@textCondition",SqlDbType.NVarChar),
                                  new SqlParameter("@BaseYear",SqlDbType.Int),
                                  new SqlParameter("@BaseMonth",SqlDbType.Int),
                                  new SqlParameter ("@BaseYearMonth",SqlDbType.DateTime),
                                  new SqlParameter ("@DateLower",SqlDbType.DateTime),
                                  new SqlParameter ("@DateUpper",SqlDbType.DateTime),
                                };
            if (condition.TextCondtion != null && condition.TextCondtion != "")
            {
                strSql.Append(" and  BRD.BaseDay like '%'+@textCondition+'%' or BRD.BaseWeekName like '%'+@textCondition+'%' ");
                par[0].Value = condition.TextCondtion;
            }
            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "BaseYear":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and BRD.BaseYear=@BaseYear ");
                                    par[1].Value = de.Value;
                                }
                            }
                            break;
                        case "BaseMonth":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and  MONTH(BaseDay)=@BaseMonth ");
                                    par[2].Value = de.Value;
                                }
                            }
                            break;
                        case "BaseYearMonth":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    par[3].Value = de.Value;
                                    DateTime BaseYearMonth = (DateTime)de.Value;
                                    strSql.Append(" and   BaseDay between '" + BaseYearMonth.AddMonths(-2) + "' and '" + BaseYearMonth.AddMonths(2) + "' ");
                                }
                            }
                            break;
                        case "DateLower":
                            {
                                if (de.Value.ToString() != "")
                                {

                                    strSql.Append(" and BaseDay>=@DateLower");
                                    par[4].Value = de.Value;
                                }
                            }
                            break;
                        case "DateUpper":
                            if (de.Value.ToString() != "")
                            {

                                strSql.Append(" and  BaseDay<=@DateUpper");
                                par[5].Value = de.Value;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), par);

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);

            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = "   BaseYear desc,BaseDay desc  ";
            }
            if (condition.PageSize == 0 && (obj != null && obj != DBNull.Value))
                condition.PageSize = Convert.ToInt32(obj);
            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), par);
        }

        public DataTable GetBaseYear()
        {
            string StrSql = " select  distinct BaseYear from  [dbo].[BaseRestDay] ";
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, StrSql);
        }


        public int ResetBaseDay(DateTime BaseDay)
        {
            int Result = 1;
            string strSql = "select count(1) from  [dbo].[BaseRestDay]  where BaseDay='" + BaseDay.ToString("yyyy-MM-dd") + "'";
            int CountNumber = Convert.ToInt32(DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql));

            string XinQi = BaseDay.ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
            if (CountNumber == 0)
            {
                strSql = "insert into BaseRestDay(BaseYear,BaseDay,BaseWeekName) values(" + BaseDay.Year + ",'" + BaseDay + "','" + XinQi + "')";
                Result = 2;
            }
            else
            { 
                strSql = "delete from BaseRestDay where  BaseDay='" + BaseDay.ToString("yyyy-MM-dd") + "'";
                Result = 3;

            }
            DBExecute.ExecuteNonQuery(DBExecute.ConnectionString, strSql);
            return Result;
        }

        public DataTable ReBaseDay(string DateLower, string DateUpper)
        {
            string strSql = "select * from BaseRestDay where BaseDay>='" + DateLower.ToString() + "' and  BaseDay<='" + DateUpper.ToString()+"'";
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, strSql);
        }

    }
}
