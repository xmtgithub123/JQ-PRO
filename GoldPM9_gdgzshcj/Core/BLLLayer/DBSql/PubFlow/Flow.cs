using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityFramework.Extensions;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using DAL;

namespace DBSql.PubFlow
{
    public class Flow : EFRepository<DataModel.Models.Flow>
    {
        /// <summary>
        /// 获取通用待办任务列表
        /// </summary>
        /// <param name="queryContext"></param>FlowStatus
        /// <returns></returns>
        public DataTable GetToDoList(Common.SqlPageInfo queryContext)
        {
            var sbSQL = new StringBuilder(" FROM FlowNode fn LEFT JOIN Flow f on fn.FlowID=f.ID LEFT JOIN FlowMultiSign fms on fms.FlowNodeID=fn.ID  LEFT JOIN DesExch ex on ex.ID=f.FlowRefID ");
            var sbCondition = new StringBuilder(" WHERE 1=1");
            var sbCondition1 = new StringBuilder(" WHERE 1=1");
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                sbCondition.Append(" AND (f.FlowName like @text OR fn.FlowNodeName like @text OR f.FlowTitle LIKE @text)");
                sqlParameters.Add(new SqlParameter("@text", "%" + queryContext.TextCondtion + "%"));
                sbCondition1.Append(" AND (p.Name LIKE @keywords or a.DisplayName LIKE @keywords or ft.Name LIKE @text or p.Title LIKE @keywords or a.Setting.exist('/Root/ActorPool/Actor/@name[contains(.,sql:variable(\"@text_\"))]')=1)");
                sqlParameters.Add(new SqlParameter("@text_", queryContext.TextCondtion));
            }
            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "EmpID":
                            sbCondition.Append(" AND (CASE fn.FlowNodeTypeID WHEN -1 THEN fms.SignEmpId ELSE fn.FlowNodeEmpId END)=" + de.Value.ToString());
                            sbCondition.Append("  AND f.FlowRefID  not in( (select id from DesExch where id=f.FlowRefID and  ExchIsInvalid=0 and f.FlowName='提资审批' )) ");   //去除无效提资 流程信息
                            //自定义流程条件
                            if (queryContext.SelectCondtion.ContainsKey("FlowStatus") && queryContext.SelectCondtion["FlowStatus"].ToString() == "2")
                            {
                                //已审批
                                sbCondition1.Append(" AND (CASE a.Symbol WHEN 'activity' THEN (CASE WHEN a.[Actor]=@actor THEN 1 ELSE 0 END) WHEN 'signactivity' THEN (CASE WHEN a.Setting.exist('/Root/Record/Sign/@actor[.=sql:variable(\"@actor\")]')=1 THEN 1 ELSE 0 END) ELSE 0 END)=1");
                            }
                            else
                            {
                                //未审批
                                sbCondition1.Append(" AND a.Setting.exist('/Root/ActorPool/Actor/text()[.=sql:variable(\"@actor\")]')=1");
                            }
                            sqlParameters.Add(new SqlParameter("@actor", de.Value.ToString()));
                            break;
                        case "FlowStatus":
                            //1为未审批 2为已审批
                            if (de.Value.ToString() == "1")
                            {
                                sbCondition.Append(" AND f.FlowStatusID=" + (int)DataModel.FlowStatus.审批中 + " AND ((fn.FlowNodeTypeID=0 AND fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.轮到 + ") OR (fn.FlowNodeTypeID=-1 AND fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.轮到 + " AND fms.SignStatus=0))");
                                //自定义流程条件
                                sbCondition1.Append(" AND a.[Status]=" + (int)JQ.BPM.Model.ActivityStatus.Activated + " AND p.[Status]=" + (int)JQ.BPM.Model.ProcessStatus.Activated);
                            }
                            else
                            {
                                sbCondition.Append(" AND (f.FlowStatusID!=" + (int)DataModel.FlowStatus.审批中 + " OR fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.跳过 + " OR fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.完成 + " OR fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.回退 + " OR (fn.FlowNodeTypeID=-1 AND fms.SignStatus=1))");
                                //自定义流程条件
                                sbCondition1.Append(" AND (p.[Status]=" + (int)JQ.BPM.Model.ProcessStatus.Activated + " OR (p.[Status]=" + (int)JQ.BPM.Model.ProcessStatus.Finished + " AND DATEDIFF(d,p.EndTime,GETDATE())<=30)) AND (a.[Status]=" + (int)JQ.BPM.Model.ActivityStatus.Finished + " OR (a.Symbol='signactivity' AND a.[Status]=" + (int)JQ.BPM.Model.ActivityStatus.Activated + "))");
                            }
                            break;
                        case "RefTable":
                            sbCondition.Append(" AND f.FlowRefTable=@RefTable");
                            sqlParameters.Add(new SqlParameter("@RefTable", de.Value.ToString()));
                            break;
                        case "RefTables":
                            sbCondition.Append(" AND f.FlowRefTable IN (" + de.Value.ToString() + ")");
                            break;
                        case "FormModelID":
                            {
                                if (de.Value.ToString() != "")
                                {
                                    sbCondition.Append(string.Format(" AND f.FlowModelID in ({0})", de.Value.ToString()));
                                }
                            }
                            break;
                        case "FlowStatusID":
                            {
                                if (de.Value.ToString() != "")
                                {
                                    sbCondition.Append(string.Format(" AND f.FlowStatusID={0}", de.Value.ToString()));
                                }
                            }
                            break;
                        case "Modular":
                            if (de.Value.ToString() != "")
                            {
                                //1为工程 2为办公
                                sbCondition.Append(" AND f.FlowModular=@flowModular");
                                sqlParameters.Add(new SqlParameter("flowModular", de.Value.ToString()));
                            }
                            break;
                    }
                }
            }
            if (queryContext.SelectCondtion.ContainsKey("Modular") && queryContext.SelectCondtion["Modular"].ToString() == "2")
            {
                //如果为办公，则考虑自定义流程部分
                queryContext.PageTotleRowCount = Common.ModelConvert.ConvertToDefault<int>(DAL.DBExecute.ExecuteScalar("SELECT SUM(tb.Amount) FROM (SELECT COUNT(1) AS Amount" + sbSQL.ToString() + sbCondition.ToString() + " UNION ALL SELECT COUNT(1) AS Amount FROM Activity a LEFT JOIN Process p on p.ID=a.ProcessID" + sbCondition1.ToString() + ") AS tb", sqlParameters.ToArray()));
                //处理列
                string cmdText = "SELECT * FROM (SELECT tb.*,row_number() OVER (ORDER BY tb.FlowNodeFromDateTime DESC) AS row_number FROM (SELECT 0 AS FlowType,f.ID AS FlowID,f.FlowRefID,f.FlowRefTable,fn.ID AS FlowNodeID,f.FlowName,f.FlowTitle,f.FlowStartDate,f.FlowFinishDate,f.FlowStatusName,f.CreatorEmpName,f.FlowStatusID,(CASE fn.FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignEmpName,'''') ELSE fn.FlowNodeEmpName END) AS FlowNodeEmpName,f.FlowUrl,CAST(f.FlowXml AS NVARCHAR(MAX)) AS FlowXml,CAST(fn.FlowNodeXml AS NVARCHAR(MAX)) AS FlowNodeXml,fn.FlowNodeUrl,fn.FlowNodeName,fn.FlowNodeTypeID,(CASE FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignDate,fn.FlowNodeDate) ELSE fn.FlowNodeDate END) AS FlowNodeDate,fn.FlowNodeFromDateTime,(CASE FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignStatus,FlowNodeStatusID) ELSE FlowNodeStatusID END) AS FlowNodeStatusID,ISNULL(fms.ID,0) AS FlowMultiSignID,0 AS DialogWidth,0 AS FormTemplateID" + sbSQL.ToString() + sbCondition.ToString() + " UNION ALL SELECT 1 AS FlowType,p.ID AS FlowID,0 AS FlowRefID,'' AS FlowRefTable,a.ID AS FlowNodeID,p.Name AS FlowName,(CASE WHEN p.FormTemplateID=0 THEN p.Title ELSE ft.Name END) AS FlowTitle,p.StartTime AS FlowStartDate,ISNULL(p.EndTime,'1900-01-01') AS FlowFinishDate,p.StatusName AS FlowStatusName,be.EmpName AS CreatorEmpName,p.[Status] AS FlowStatusID,'' AS FlowNodeEmpName,'' AS FlowUrl,'<Root />' AS FlowXml,'<Root />' AS FlowNodeXml,'' AS FlowNodeUrl,a.DisplayName AS FlowNodeName,0 AS FlowNodeTypeID,ISNULL(a.EndTime,'1900-01-01') AS FlowNodeDate,a.StartTime AS FlowNodeFromDateTime,a.[Status] AS FlowNodeStatusID,0 AS FlowMultiSignID,ft.Width AS DialogWidth,p.FormTemplateID FROM Activity a LEFT JOIN Process p on p.ID = a.ProcessID LEFT JOIN FormTemplate ft ON ft.ID = p.FormTemplateID LEFT JOIN BaseEmployee be ON be.EmpID = p.Creator" + sbCondition1.ToString() + ") AS tb) AS tb1 WHERE tb1.row_number BETWEEN " + (((queryContext.PageIndex - 1) * queryContext.PageSize) + 1) + " AND " + (queryContext.PageIndex * queryContext.PageSize);
                return DAL.DBExecute.ExecuteDataTable(cmdText, sqlParameters.ToArray());
            }
            else
            {
                queryContext.PageTotleRowCount = Common.ModelConvert.ConvertToDefault<int>(DAL.DBExecute.ExecuteScalar("SELECT Count(1)" + sbSQL.ToString() + sbCondition.ToString(), sqlParameters.ToArray()));
                string cmdText = "SELECT * FROM (SELECT 0 AS FlowType,f.ID AS FlowID,f.FlowRefID,f.FlowRefTable,fn.ID AS FlowNodeID,f.FlowName,f.FlowTitle,f.FlowStartDate,f.FlowFinishDate,f.FlowStatusName,f.CreatorEmpName,f.FlowStatusID,(CASE fn.FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignEmpName,'') ELSE fn.FlowNodeEmpName END) AS FlowNodeEmpName,f.FlowUrl,f.FlowXml,fn.FlowNodeXml,fn.FlowNodeUrl,fn.FlowNodeName,fn.FlowNodeTypeID,(CASE FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignDate,fn.FlowNodeDate) ELSE fn.FlowNodeDate END) AS FlowNodeDate,fn.FlowNodeFromDateTime,(CASE FlowNodeTypeID WHEN -1 THEN ISNULL(fms.SignStatus,FlowNodeStatusID) ELSE FlowNodeStatusID END) AS FlowNodeStatusID,ISNULL(fms.ID,0) AS FlowMultiSignID,0 AS DialogWidth,row_number() OVER (ORDER BY " + queryContext.SelectOrder + ") AS row_number" + sbSQL.ToString() + sbCondition.ToString() + ") AS tb WHERE tb.row_number BETWEEN " + (((queryContext.PageIndex - 1) * queryContext.PageSize) + 1) + " AND " + (queryContext.PageIndex * queryContext.PageSize);
                return DAL.DBExecute.ExecuteDataTable(cmdText, sqlParameters.ToArray());
            }
        }


        /// <summary>
        /// 获取流转记录
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public List<dynamic> GetFlowNodeExes(Common.SqlPageInfo queryContext)
        {
            var flowID = Common.ModelConvert.ConvertToDefault<int>((queryContext.SelectCondtion["FlowID"]));
            using (var dbContext = new DAL.JQPM9_DefaultContext())
            {
                var query = dbContext.FlowNodeExes.Where(m => m.FlowID == flowID);
                if (!String.IsNullOrEmpty(queryContext.SelectOrder))
                {
                    query = query.OrderBy(queryContext.SelectOrder);
                }
                if (!queryContext.ToPageData)
                {
                    if (queryContext.PageIndex < 1)
                    {
                        queryContext.PageIndex = 1;
                    }
                    if (queryContext.PageSize == 0)
                    {
                        queryContext.PageSize = 10;
                    }
                    var itemIndex = (queryContext.PageIndex - 1) * queryContext.PageSize;
                    queryContext.PageTotleRowCount = query.FutureCount();
                    query = query.Skip(itemIndex).Take(queryContext.PageSize);
                }
                else
                {
                    queryContext.PageTotleRowCount = 0;
                }
                //return (query.Select("new(Id,FK_FlowNodeExe_ExeActionID.BaseName,FlowID)") as IQueryable<dynamic>).ToList();
                //return query.Select(m => new { m.Id, FlowNodeName = m.FK_FlowNodeExe_FlowNodeID.FlowNodeName, FK_FlowNodeExe_ExeActionID = new { m.FK_FlowNodeExe_ExeActionID.BaseName }, m.FlowID }).ToList<dynamic>();
                return query.Select(m => new
                {
                    m.Id,
                    FlowNodeName = m.FlowNodeID == 0 ? "" : m.FK_FlowNodeExe_FlowNodeID.FlowNodeName,
                    ActionName = m.FK_FlowNodeExe_ExeActionID.BaseName,
                    m.FlowID,
                    m.ExeEmpName,
                    m.ExeArgEmpName,
                    m.ExeActionDate,
                    m.ExeNote
                }).ToList<dynamic>();
            }
        }

        /// <summary>
        /// 获取流程的状态列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetFlowStatuses(string refTable, List<int> refIDs)
        {
            if (string.IsNullOrEmpty(refTable) || refIDs == null || refIDs.Count == 0)
            {
                return null;
            }
            using (var dbContext = new DAL.JQPM9_DefaultContext())
            {
                return dbContext.Flows.Where(m => m.FlowRefTable == refTable && refIDs.Contains(m.FlowRefID)).Select(m => new { m.Id, m.FlowRefTable, m.FlowRefID, m.FlowStatusName, m.FlowName, m.FlowStatusID }).ToList<dynamic>();
            }
        }

        /// <summary>
        /// 获取出待办任务的数量
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="modular">1 工程表单 2 办公表单</param>
        /// <returns></returns>
        public int GetToDoAmount(int empID, int modular, int agentTypeID, string agentFlow)
        {
            if (agentTypeID == 0)
            {
                var dt = GetToDoData();
                var rows = dt.Select("EmpID=" + empID + " AND FlowType=0 AND FlowModular='" + modular + "'");
                var result = 0;
                foreach (DataRow row in rows)
                {
                    result += row.Field<int>("Amount");
                }
                if (modular == 2)
                {
                    result += dt.Select("FlowType=1 AND EmpIDs LIKE '%," + empID + ",%'").Length;
                }
                return result;
            }
            else if (agentTypeID == -1 || agentTypeID == 2)
            {
                //根据代理来
                if (string.IsNullOrEmpty(agentFlow))
                {
                    return 0;
                }
                var dt = GetToDoData();
                var rows = dt.Select("EmpID=" + empID + " AND FlowModular='" + modular + "'" + (string.IsNullOrEmpty(agentFlow) ? "" : (" AND FlowRefTable IN ('" + agentFlow.Replace(",", "','") + "')")));
                var result = 0;
                foreach (DataRow row in rows)
                {
                    result += row.Field<int>("Amount");
                }
                if (modular == 2)
                {
                    result += dt.Select("FlowType=1 AND EmpIDs LIKE '%," + empID + ",%'").Length;
                }
                return result;
            }
            else
            {
                return 0;
            }
        }

        public DataTable GetToDoData()
        {
            if (Common.CacheManager.GetCache("PubFlow") == null)
            {
                var sbSQL = new StringBuilder();
                sbSQL.Append("SELECT COUNT(1) AS Amount,(CASE fn.FlowNodeTypeID WHEN - 1 THEN fms.SignEmpId ELSE fn.FlowNodeEmpId END) AS EmpID,f.FlowModular,f.FlowRefTable,'' AS EmpIDs,0 AS FlowType");
                sbSQL.Append(" FROM FlowNode fn");
                sbSQL.Append(" LEFT JOIN Flow f on fn.FlowID=f.ID LEFT JOIN DesExch ex on ex.ID=f.FlowRefID ");
                sbSQL.Append(" LEFT JOIN FlowMultiSign fms on fms.FlowNodeID=fn.ID");
                sbSQL.Append(" WHERE f.FlowStatusID=" + (int)DataModel.FlowStatus.审批中 + " AND ((fn.FlowNodeTypeID=0 AND fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.轮到 + ") OR (fn.FlowNodeTypeID=-1 AND fn.FlowNodeStatusID=" + (int)DataModel.NodeStatus.轮到 + " AND fms.SignStatus=0))  and  f.FlowRefID  not in( ( select id from   DesExch where id=f.FlowRefID and  ExchIsInvalid=0 and f.FlowName='提资审批' ))  ");
                sbSQL.Append(" GROUP BY (CASE fn.FlowNodeTypeID WHEN -1 THEN fms.SignEmpId ELSE fn.FlowNodeEmpId END),f.FlowModular,f.FlowRefTable");
                sbSQL.Append(" UNION ALL");
                sbSQL.Append(" SELECT 1 AS Amount,0 AS EmpID,'2' AS FlowModular,'' AS FlowRefTable,CAST(a.Setting.query('for $A in Root/ActorPool/Actor/text() return concat(\",\",$A,\",\")') AS NVARCHAR) AS EmpIDs,1 AS FlowType FROM Activity a LEFT JOIN Process p on p.ID=a.ProcessID WHERE p.[Status]=" + (int)JQ.BPM.Model.ProcessStatus.Activated + " AND a.[Status]=" + (int)JQ.BPM.Model.ActivityStatus.Activated);
                var result = DAL.DBExecute.ExecuteDataTable(sbSQL.ToString());
                Common.CacheManager.SetCache("PubFlow", result, DateTime.Now.AddHours(3), TimeSpan.Zero);
                return result;
            }
            else
            {
                return (DataTable)Common.CacheManager.GetCache("PubFlow");
            }
        }

        public static void RemoveCache()
        {
            Common.CacheManager.CacheRemove("PubFlow");
        }

        public void Delete(int[] refIds, string refTable)
        {
            if (string.IsNullOrEmpty(refTable) || refIds.Length == 0)
            {
                return;
            }
            string idSet = string.Join(",", refIds);
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            var sbSQL = new StringBuilder();
            sbSQL.Append("DELETE FROM FlowNodeExe WHERE FlowID IN (SELECT ID FROM Flow WHERE FlowRefTable=@table AND FlowRefID IN (" + idSet + "))");
            sbSQL.Append(" DELETE FROM FlowMultiSign WHERE FlowID IN (SELECT ID FROM Flow WHERE FlowRefTable=@table AND FlowRefID IN (" + idSet + "))");
            sbSQL.Append(" DELETE FROM FlowNode WHERE FlowID IN (SELECT ID FROM Flow WHERE FlowRefTable=@table AND FlowRefID IN (" + idSet + "))");
            sbSQL.Append(" DELETE FROM Flow WHERE FlowRefTable=@table AND FlowRefID IN (" + idSet + ")");
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString(), new SqlParameter("@table", refTable));
            RemoveCache();
        }

        public void Delete(int Id)
        {
            var sbSQL = new StringBuilder();
            sbSQL.Append("DELETE FROM FlowNodeExe WHERE FlowID="+Id+" ");
            sbSQL.Append(" DELETE FROM FlowMultiSign WHERE FlowID="+Id+" ");
            sbSQL.Append(" DELETE FROM FlowNode WHERE FlowID="+Id+" ");
            sbSQL.Append(" DELETE FROM Flow WHERE ID="+Id+ " ");
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
            RemoveCache();
        }

        public DataTable GetFlowNodeExeTable(Common.SqlPageInfo queryContext)
        {
            string RowColumn = "FlowNodeExe.*";
            RowColumn += ",Convert(varchar(20),ExeActionDate,120) as NewExeActionDate";
            RowColumn += ",(select BaseName from BaseDataSystem where BaseID=ExeActionID) as ActionName ";
            RowColumn += ",isnull((select FlowNodeName  from  FlowNode where FlowNode.Id=FlowNodeID ),(case when FlowRefTable='DesMutiSign' then '会签人' when FlowRefTable='DesExch' then '收资人' else '' end)) as FlowNodeName ";
            var sbSQL = new StringBuilder("select Count(1)  FROM FlowNodeExe left join flow on flow.id=flowid ");
            sbSQL.Append(" WHERE 1=1");
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                //sbSQL.Append(" AND (f.FlowName like @text OR fn.FlowNodeName like @text OR f.FlowTitle LIKE @text)");
                //sqlParameters.Add(new SqlParameter("@text", "%" + queryContext.TextCondtion + "%"));
            }
            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "FlowID":
                            sbSQL.Append(" AND FlowNodeExe.FlowID=@FlowID");
                            sqlParameters.Add(new SqlParameter("@FlowID", de.Value.ToString()));
                            break;
                    }
                }
            }

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, sbSQL.ToString(), sqlParameters.ToArray());

            if (obj == null && obj == DBNull.Value) queryContext.PageTotleRowCount = 0;
            else queryContext.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(queryContext.SelectOrder))
            {
                queryContext.SelectOrder = "Id asc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, sbSQL);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), sqlParameters.ToArray());

        }

        public string ExentFlowNodeExe(string refTable)
        {
            string RowColumn = ",isnull((select FlowNodeName  from  FlowNode where FlowNode.Id=FlowNodeID ),'') as FlowNodeName ";
            switch (refTable)
            {
                case "DesMutiSign":
                    RowColumn = ",isnull((select FlowNodeName  from  FlowNode where FlowNode.Id=FlowNodeID ),'会签人') as FlowNodeName ";
                    break;
                default:
                    break;
            }
            return RowColumn;
        }


        public int GetFlowStatusId(string FlowRefTable, int RefID, int CreateEmpId, int CurrentEmpId)
        {
            int flowStatusId = 0;
            DataModel.Models.Flow flowModel = GetList(p => p.FlowRefTable == FlowRefTable && p.FlowRefID == RefID).FirstOrDefault();
            if (flowModel != null)
            {

                if (flowModel.FlowStatusID != 3)
                {
                    DataModel.Models.FlowNode flowNode = new DBSql.PubFlow.FlowNode().GetList(m => m.FlowID == flowModel.Id && m.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到).FirstOrDefault();
                    if (flowNode.FlowNodeTypeID == -1)
                    {
                        DataModel.Models.FlowNodeExe flowNodeExe = new DBSql.PubFlow.FlowNodeExe().GetList(m => m.FlowNodeID == flowNode.Id && m.ExeActionID == (int)DataModel.NodeAction.轮到 && m.ExeEmpId == CurrentEmpId).FirstOrDefault();
                        if (null == flowNodeExe)
                        {
                            flowStatusId = flowModel.FlowStatusID;
                        }
                    }
                    else
                    {
                        if (flowNode.FlowNodeEmpId != CurrentEmpId)
                        {
                            flowStatusId = flowModel.FlowStatusID;
                        }
                    }
                }
                else
                {
                    flowStatusId = flowModel.FlowStatusID;
                }
            }
            else
            {
                if (CreateEmpId != CurrentEmpId)
                {
                    flowStatusId = -1;
                }
            }
            return flowStatusId;
        }
    }
}
