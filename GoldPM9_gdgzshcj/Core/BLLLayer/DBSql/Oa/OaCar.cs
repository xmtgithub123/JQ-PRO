
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Entity;

namespace DBSql.OA
{
    public class OaCar : EFRepository<DataModel.Models.OaCar>
    {
        public DataTable GetCarList(Common.SqlPageInfo condition)
        {
            DataTable dt = new DataTable();
            string RowColumn = "dbo.OaCar.*,case ( SELECT COUNT(1) FROM OaCarUse as cu inner join dbo.Flow as ap on ap.FlowRefTable='CarUse'  and ap.FlowRefID = cu.Id WHERE cu.CarID = OaCar.Id and  (ap.FlowStatusID = 3 or ap.FlowStatusID = 2) and cu.IsFinish=0 AND (cu.DateArrive = '1900-01-01' and GETDATE() < cu.DateLeavePlan) ) when 0 then '正常' else '使用中' end  as IsUsed ";

            StringBuilder strSql = new StringBuilder();
             strSql = strSql.Append(" SELECT Count(1) FROM dbo.OaCar  WHERE 1=1");
            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@DateUpper",SqlDbType.DateTime),
                new SqlParameter("@DateLower",SqlDbType.DateTime),
                  new SqlParameter("@EmpID",SqlDbType.DateTime),
            };

            //((cu.CarID IS NULL) OR((cu.DateLeavePlan > '2016-08-17' OR cu.DateArrivePlan < '2016-08-17') AND(cu.DateLeavePlan > '2016-08-23' OR cu.DateArrivePlan < '2016-08-23')))
            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and (CarName like '%'+@TextCondtion+'%' or CarNumber like '%'+@TextCondtion+'%' ) ");
                paras[0].Value = condition.TextCondtion;
            }

            //if (condition.SelectCondtion.Contains("DateUpper") && condition.SelectCondtion["DateUpper"].ToString() != ""&&condition.SelectCondtion.Contains("DateLower") && condition.SelectCondtion["DateLower"].ToString() != "")
            //{
            //    strSql.Append(" AND ((cu.CarID IS NULL) OR ( cu.DateLeavePlan Between @DateLower And @DateUpper Or cu.DateArrivePlan Between @DateLower And @DateUpper or ( cu.DateLeavePlan <= @DateLower And cu.DateArrivePlan >= @DateUpper))");
            //    paras[1].Value = condition.SelectCondtion["DateUpper"].ToString();
            //    paras[2].Value = condition.SelectCondtion["DateLower"].ToString();

            //}

                if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "EmpID":
                            {
                                if (de.Value.ToString().Trim() != "" && de.Value.ToString().Trim() != "0")
                                {
                                    strSql.Append(" AND car.CreatorEmpId=@EmpID");
                                    paras[3].Value = de.Value;
                                }
                            }
                            break;

                    }
                }
            }

            //判断PageModel中查询条件是否为空ew
            if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            {
                string selectT = "";
                List<SqlParameter> _paramList = paras.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                paras = _paramList.ToArray();
                strSql.Append(selectT);
            }
            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);

        }
        public DataTable GetCarListByUse(Common.SqlPageInfo condition)
        {
            DataTable dt = new DataTable();
            string RowColumn = "dbo.OaCar.*";

            StringBuilder strSql = new StringBuilder();
            strSql = strSql.Append(" SELECT Count(1) FROM dbo.OaCar  WHERE 1=1 and DeleterEmpId=0 ");
            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@DateUpper",SqlDbType.DateTime),
                new SqlParameter("@DateLower",SqlDbType.DateTime),
                  new SqlParameter("@EmpID",SqlDbType.DateTime),
            };

            //((cu.CarID IS NULL) OR((cu.DateLeavePlan > '2016-08-17' OR cu.DateArrivePlan < '2016-08-17') AND(cu.DateLeavePlan > '2016-08-23' OR cu.DateArrivePlan < '2016-08-23')))
            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and (CarName like '%'+@TextCondtion+'%' or CarNumber like '%'+@TextCondtion+'%' ) ");
                paras[0].Value = condition.TextCondtion;
            }

            //if (condition.SelectCondtion.Contains("DateUpper") && condition.SelectCondtion["DateUpper"].ToString() != ""&&condition.SelectCondtion.Contains("DateLower") && condition.SelectCondtion["DateLower"].ToString() != "")
            //{
            //    strSql.Append(" AND ((cu.CarID IS NULL) OR ( cu.DateLeavePlan Between @DateLower And @DateUpper Or cu.DateArrivePlan Between @DateLower And @DateUpper or ( cu.DateLeavePlan <= @DateLower And cu.DateArrivePlan >= @DateUpper))");
            //    paras[1].Value = condition.SelectCondtion["DateUpper"].ToString();
            //    paras[2].Value = condition.SelectCondtion["DateLower"].ToString();

            //}

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "EmpID":
                            {
                                if (de.Value.ToString().Trim() != "" && de.Value.ToString().Trim() != "0")
                                {
                                    strSql.Append(" AND car.CreatorEmpId=@EmpID");
                                    paras[3].Value = de.Value;
                                }
                            }
                            break;

                    }
                }
            }
            if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            {
                string selectT = "";
                List<SqlParameter> _paramList = paras.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                paras = _paramList.ToArray();
                strSql.Append(selectT);
            }
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), paras);
            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);

        }

        public void UpdateOaCarList(int[] ids, DataModel.EmpSession userInfo)
        {
            if (ids.Length == 0)
            {
                return;
            }
            string idSet = string.Join(",", ids);
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            var sbSQL = new StringBuilder();
            sbSQL.Append(string.Format("Update OaCar set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }

    }
}
