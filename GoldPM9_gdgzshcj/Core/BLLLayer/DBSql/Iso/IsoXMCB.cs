using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Iso
{
    public class IsoXMCB : EFRepository<DataModel.Models.IsoXMCB>
    {
        public DataTable GetListInfo(Common.SqlPageInfo queryContext, DataModel.EmpSession userInfo = null)
        {
            string RowColumn = "ISO.Id, p.ProjName,p.ProjNumber,ISO.CreatorEmpId";
            RowColumn += ",(select BaseName from BaseData where baseID=ISO.ProjPhaseId) as ProjPhaseName";
            RowColumn += ",isnull((select sum(CostFactFee) from ProjCost where XMCBID=ISO.Id and CostIsType=1 and CostIsSum=0),0) as CostFee";
            RowColumn += string.Format(",FlowStatus=dbo.GetFlowStatusId('{0}',ISO.Id,ISO.CreatorEmpId,{1},{2},{3})", "IsoXMCB", userInfo.EmpID, (int)DataModel.NodeStatus.轮到, (int)DataModel.NodeAction.轮到);
            RowColumn += ",f.Id AS FlowID, f.FlowName,f.FlowStatusID,f.FlowStatusName,f.FlowXml.value('(Root/TurnedEmpIDs/text())[1]', 'nvarchar(200)') AS FlowTurnedEmpIDs ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select Count(1) from dbo.IsoXMCB AS ISO inner join  Project as p on ISO.ProjID=p.Id                                                                  
                                                                   LEFT JOIN Flow f ON f.FlowRefID=ISO.Id and f.FlowRefTable='IsoXMCB' where 1=1 and ISO.DeleterEmpId=0 ");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@CTStartTime",SqlDbType.DateTime),
                new SqlParameter("@CTEndTime",SqlDbType.DateTime),
            };

            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and ( p.ProjName like '%'+@TextCondtion+'%' or p.ProjNumber like '%'+@TextCondtion+'%')");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "CTStartTime":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and ISO.CreationTime>=@CTStartTime");
                                paras[1].Value = Convert.ToDateTime(de.Value);
                            }
                            break;
                        case "CTEndTime":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and ISO.CreationTime<=@CTEndTime");
                                paras[2].Value = Convert.ToDateTime(de.Value).AddHours(23.99);
                            }
                            break;
                        case "QueryDeptID":
                            strSql.Append(" and (ISO.CreatorDepId=" + de.Value.ToString() + ")");
                            break;
                        case "QueryEmpID":
                            strSql.Append(" and (ISO.CreatorEmpId=" + de.Value.ToString() + ")");
                            break;
                        default:
                            break;
                    }
                }
            }
            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), paras);

            if (obj == null && obj == DBNull.Value) queryContext.PageTotleRowCount = 0;
            else queryContext.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(queryContext.SelectOrder))
            {
                queryContext.SelectOrder = "ISO.Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }
        public void UpdateIsoXMCBInfo(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update IsoXMCB set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
