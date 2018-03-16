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


namespace DBSql.Iso
{
    public class IsoFBSJCH : EFRepository<DataModel.Models.IsoFBSJCH>
    {
        public DataTable GetListInfo(Common.SqlPageInfo queryContext, DataModel.EmpSession userInfo = null)
        {
            string RowColumn = "ISO.Id,ISO.TableNumber,ISO.Number,ISO.CreatorEmpId";
            RowColumn += ",Sub.SubNumber,Sub.SubName, p.ProjName,p.ProjNumber";
            RowColumn += ",Cust.CustName,Sub.CreatorDepName";
            RowColumn += ", isnull((SELECT  BaseName + ','FROM BaseData  WHERE  CHARINDEX(','+CAST(BaseID AS  nvarchar(5))+',',','+Sub.SubHZSJ+',')>0 FOR XML PATH('')),'')  as HZSJName";

            RowColumn += ", (case when IsGuide=1 then '是' else '否' end ) as IsGuideShow,GuideTime";
            RowColumn += ", (case when IsReview=1 then '是' else '否' end ) as IsReviewShow,ReviewTime";
            RowColumn += ", (case when IsMulsign=1 then '是' else '否' end ) as IsMulsignShow,MulsignTime";

            RowColumn += string.Format(",FlowStatus=dbo.GetFlowStatusId('{0}',ISO.Id,ISO.CreatorEmpId,{1},{2},{3})", "IsoFBSJCH", userInfo.EmpID, (int)DataModel.NodeStatus.轮到, (int)DataModel.NodeAction.轮到);
            RowColumn += ",f.Id AS FlowID, f.FlowName,f.FlowStatusID,f.FlowStatusName,f.FlowXml.value('(Root/TurnedEmpIDs/text())[1]', 'nvarchar(200)') AS FlowTurnedEmpIDs ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select Count(1) from dbo.IsoFBSJCH AS ISO inner join  ProjSub  as Sub on ISO.ProjSubID=Sub.Id 
                                                                   inner join  Project  as p on Sub.ProjID=p.Id  
                                                                   inner join  BussCustomer  as Cust on Sub.ColAttType2=Cust.Id  
                                                                   LEFT JOIN Flow f ON f.FlowRefID=ISO.Id and f.FlowRefTable='IsoFBSJCH' where 1=1 and ISO.DeleterEmpId=0 ");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@FQStartTime",SqlDbType.DateTime),
                new SqlParameter("@FQEndTime",SqlDbType.DateTime),
            };

            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and (Sub.SubNumber like '%'+@TextCondtion+'%' or Sub.SubName like '%'+@TextCondtion+'%' or p.ProjName like '%'+@TextCondtion+'%' or p.ProjNumber like '%'+@TextCondtion+'%')");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "FQStartTime":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and ISO.CreationTime>=@FQStartTime");
                                paras[1].Value = Convert.ToDateTime(de.Value);
                            }
                            break;
                        case "FQEndTime":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and ISO.CreationTime<=@FQEndTime");
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
        public void UpdateIsoFBSJCHInfo(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update IsoFBSJCH set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
