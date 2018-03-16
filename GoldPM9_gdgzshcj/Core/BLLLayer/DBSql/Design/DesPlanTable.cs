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

namespace DBSql.Design
{
    public class DesPlanTable : EFRepository<DataModel.Models.DesPlanTable>
    {
        public DataTable GetDesPlanList(Common.SqlPageInfo condition)
        {
            string RowColumn = @"
                p.ProjNumber ,
                p.ProjName ,
                p.ProjEmpName,
                p.DatePlanStart,
                p.DatePlanFinish,
                dt.*,
                (select BaseName from BaseData where BaseID=PhaseId) as PhaseName 
            ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                SELECT  Count(1) 
                FROM    DesPlanTable dt
                        LEFT JOIN DesTaskGroup dg on dg.Id=dt.TaskGroupId
                        LEFT JOIN Project p ON dt.ProjId = p.Id
            ");
            var sbCondition = new StringBuilder(" WHERE 1=1 ");
            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
            };

            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                sbCondition.Append(" AND (p.ProjNumber like '%'+@TextCondtion+'%' or p.ProjName like '%'+@TextCondtion+'%' ) ");
                paras[0].Value = condition.TextCondtion;
            }

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "ProjDeleterEmpId":
                            sbCondition.Append(" and p.DeleterEmpId = 0  ");
                            break;
                        case "groupDeleterEmpId":
                            sbCondition.Append(" and dg.DeleterEmpId = 0 ");
                            break;
                    }
                }
            }
            //判断PageModel中查询条件是否为空
            if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            {
                string selectT = "";
                List<SqlParameter> _paramList = paras.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                paras = _paramList.ToArray();
                sbCondition.Append(selectT);
            }
            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString() + sbCondition.ToString(), paras);
            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//
            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = " p.Id DESC";
            }
          
            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql.Append(sbCondition));
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }
        

    }
}
