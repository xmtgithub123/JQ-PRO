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
    public class DesTaskFeeDetails : EFRepository<DataModel.Models.DesTaskFeeDetails>
    {
        public DataTable GetFeeDetailsList(Common.SqlPageInfo condition)
        {
            string RowColumn = " dtg.ProjNumber,dtg.ProjName,dtg.TaskGroupEmpName,dt.* ";
            RowColumn += ",(select BaseName from BaseData where BaseID=dtg.TaskGroupPhaseId) as PhaseName ";
            RowColumn += ",isnull((select TaskName from DesTask d where d.ID=dt.TaskID),'') as TaskName";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from DesTaskFeeDetails dt left join DesTaskGroup dtg on dt.taskgroupid=dtg.ID  ");
            strSql.Append(" left join project p on p.Id=dtg.projID where 1=1 ");
            SqlParameter[] param = {
                                        new SqlParameter("@txtContent",SqlDbType.VarChar),

            };
            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and ( dtg.ProjNumber like '%'+@txtContent+'%' or dtg.ProjName like '%'+@txtContent+'%' )");
                param[0].Value = condition.TextCondtion.ToString();
            }
            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "TaskID":
                            if (de.Value.ToString() != "-1")
                            {
                                strSql.Append(" and dms.TaskID=@TaskID ");
                                param[1].Value = Convert.ToInt32(de.Value);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //判断PageModel中查询条件是否为空
            if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            {
                string selectT = "";
                List<SqlParameter> _paramList = param.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                param = _paramList.ToArray();
                strSql.Append(selectT);
            }

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), param);

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = " dtg.Id desc,dt.Id asc ";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), param);
        }


        public DataTable GetDesTaskList(Common.SqlPageInfo condition)
        {
            string RowColumn = " dtg.ProjId,dtg.ProjNumber,dtg.ProjName,dtg.TaskGroupEmpName,dtg.Id as TaskGroupId ";
            RowColumn += ",(select BaseName from BaseData where BaseID=dtg.TaskGroupPhaseId) as PhaseName,dtg.TaskGroupPhaseId ";
            RowColumn += ",isnull((select Top 1 Id,FeeZBF,FeeXMQQF,FeeKCF,FeeSJF,FeeYSBZF,FeeJGTBZF,FeeZTZ  from DesTaskFeeDetails dts where dts.TaskGroupId=dtg.Id for xml raw),'') as TaskFeeDetails";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from DesTaskGroup dtg left join Project p on p.Id=dtg.ProjID where 1=1  ");
            SqlParameter[] param = {
                                        new SqlParameter("@txtContent",SqlDbType.VarChar),
                                        new SqlParameter("@TaskType",SqlDbType.Int)

            };
            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and ( dtg.ProjNumber like '%'+@txtContent+'%' or dtg.ProjName like '%'+@txtContent+'%' )");
                param[0].Value = condition.TextCondtion.ToString();
            }
            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "ProjDeleterEmpId":
                            strSql.Append(" and p.DeleterEmpId=" + de.Value.ToString() + " ");
                            break;
                        case "GroupDeleterEmpId":
                            strSql.Append("and dtg.DeleterEmpId=" + de.Value.ToString() + " ");
                            break;
                        case "NoJoinDetail":
                            if (de.Value.ToString() == "1")
                            {
                                strSql.Append(" and (select count(1) from DesTaskFeeDetails dts where dts.TaskGroupId=dtg.Id  )=0 ");
                            }
                            break;
                        case "TaskGroupPhaseId":
                            if (de.Value.ToString()=="true")
                            {
                                strSql.Append(" and TaskGroupPhaseId<>0 ");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //判断PageModel中查询条件是否为空
            if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            {
                string selectT = "";
                List<SqlParameter> _paramList = param.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                param = _paramList.ToArray();
                strSql.Append(selectT);
            }

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), param);

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = " dtg.ProjId asc,dtg.Id asc ";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), param);
        }
    }
}
