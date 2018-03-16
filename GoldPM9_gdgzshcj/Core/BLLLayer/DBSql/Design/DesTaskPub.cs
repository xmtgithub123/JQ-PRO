using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DAL;
namespace DBSql.Design
{
    public partial class DesTask
    {
        public DataTable GetTaskListForPublic(Common.SqlPageInfo condition)
        {
            string RowColumn = " p.ProjNumber,p.ProjName,p.ProjEmpName ";
            RowColumn += " ,dt.TaskName,dt.TaskEmpName,dt.TaskStatus,(select BaseName from BaseData where BaseID=dt.TaskSpecId) as SpecName ";
            RowColumn += " ,(select BaseName from BaseData where BaseID=dt.TaskPhaseId) as TaskPhaseName ";
            RowColumn += " ,dt.DatePlanStart,dt.DatePlanFinish,dt.DateActualFinish,dt.TaskSpecId AS SpecID,p.Id AS projID,dt.Id AS TaskID  ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from DesTask dt LEFT JOIN Project p on dt.ProjID=p.Id  ");
            strSql.Append(" where 1=1 ");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@TaskType",SqlDbType.Int),
                new SqlParameter("@ProjId",SqlDbType.Int),
                new SqlParameter("@TaskPhaseId",SqlDbType.Int),
                new SqlParameter("@EmpID",SqlDbType.Int)
            };

            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and (ProjNumber like '%'+@TextCondtion+'%' or ProjName like '%'+@TextCondtion+'%' or dt.TaskName like '%'+@TextCondtion+'%' ) ");
                paras[0].Value = condition.TextCondtion;
            }

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        //扩展查询列
                        case "OtherColumn":
                            if (de.Value.ToString() != "")
                            {
                                RowColumn += de.Value.ToString();
                            }
                            break;
                        case "ProjDeleterEmpId":
                            strSql.Append(" and p.DeleterEmpId=0 ");
                            break;
                        case "TaskDeleterEmpId":
                            strSql.Append(" and dt.DeleterEmpId=0 ");
                            break;
                        case "TaskType":
                            if (de.Value.ToString() != "-1")
                            {
                                strSql.Append(" and dt.TaskType=@TaskType ");
                                paras[1].Value = Convert.ToInt32(de.Value);
                            }
                            break;
                        case "ProjId":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.Append(" and dt.ProjId=@ProjId ");
                                paras[2].Value = Convert.ToInt32(de.Value);
                            }
                            break;
                        case "TaskPhaseId":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.Append(" and dt.TaskPhaseId=@TaskPhaseId ");
                                paras[3].Value = Convert.ToInt32(de.Value);
                            }
                            break;
                        case "PermitEmpID":
                            int EmpID = Convert.ToInt32(de.Value);
                            strSql.Append(" and (p.ProjEmpId=@EmpID or (select count(1) from DesTaskGroup dg where dg.DeleterEmpId=0 and TaskGroupEmpID=@EmpID and dg.Id=dt.TaskGroupId)>0  ");//设总 ||阶段负责人
                            strSql.Append(" or (select count(1) from destask dt1 where dt1.ProjId=dt.ProjId and dt1.TaskGroupId=dt.TaskGroupId and dt1.TaskPhaseId=dt.TaskPhaseId and dt1.TaskSpecId=dt.TaskSpecId and TaskType=1 and  dt1.TaskEmpID=@EmpID )>0 ");//专业负责人
                            strSql.Append(" or dt.TaskEmpID=@EmpID ");//设计负责人
                            strSql.Append(" ) ");
                            paras[4].Value = EmpID;
                            break;
                        case "ProjEmp":
                            //设总||阶段负责人
                            strSql.Append("and (p.ProjEmpId=" + de.Value.ToString() + " or (select count(1) from DesTaskGroup dg where dg.DeleterEmpId=0 and TaskGroupEmpID=" + de.Value.ToString() + " and dg.Id=dt.TaskGroupId)>0  )");
                            break;
                        case "SpecEmp":
                            strSql.Append(" and (select count(1) from destask dt1 where dt1.DeleterEmpId=0 and dt1.ProjId=dt.ProjId and dt1.TaskGroupId=dt.TaskGroupId and dt1.TaskPhaseId=dt.TaskPhaseId and dt1.TaskSpecId=dt.TaskSpecId and TaskType=1 and  dt1.TaskEmpID=" + de.Value.ToString() + " )>0 ");//专业负责人
                            break;
                        case "PersonEmp":
                            strSql.Append(" and dt.TaskEmpID=" + de.Value.ToString() + " ");
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
                List<SqlParameter> _paramList = paras.ToList();
                condition.SetSqlPrams(_paramList, ref selectT);
                paras = _paramList.ToArray();
                strSql.Append(selectT);
            }

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), paras);

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = "TaskGroupId desc,TaskPath asc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }


        public DataTable GetSpecHeaderByTaskId(int taskId)
        {
            string sql = string.Format("SELECT * FROM F_GetTaskSpecHeader({0})", taskId);
            return DBExecute.ExecuteDataTable(sql);
        }
    }
}
