﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-28 14:59:29
#endregion
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

namespace DBSql.Oa
{
    public class OaFileSend : EFRepository<DataModel.Models.OaFileSend>
    {

        public DataTable GetInfoList(Common.SqlPageInfo queryContext,DataModel.EmpSession userInfo = null)
        {
            string RowColumn = " o.Id,o.OaFileName,o.OaFilePage,o.OaFileDate,o.OaFileSendNote,o.OaFileSendDep,o.OaFileGetEmp,o.OaFileGetEmpDept,o.CreatorEmpName,o.CreationTime,o.CreatorEmpId";

            RowColumn += ",isnull((SELECT  empName + ','FROM BaseEmployee  WHERE  CHARINDEX(','+CAST(EmpID AS  nvarchar(5))+',',','+o.OaFileGetEmp+',')>0 FOR XML PATH('')),'') as AcceptEmpNames";
            RowColumn += ",isnull((SELECT  baseName + ','FROM BaseData  WHERE  CHARINDEX(','+CAST(BaseID AS  nvarchar(5))+',',','+o.OaFileGetEmpDept+',')>0 FOR XML PATH('')),'') as AcceptDeptNames";
            RowColumn += ",f.Id AS FlowID, f.FlowName,f.FlowStatusID,f.FlowStatusName,f.FlowXml.value('(Root/TurnedEmpIDs/text())[1]', 'nvarchar(200)') AS FlowTurnedEmpIDs ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from dbo.OaFileSend AS o  LEFT JOIN Flow f ON f.FlowRefID=o.Id and f.FlowRefTable='OaFileSend' where 1=1  and  DeleterEmpId=0");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@startTime",SqlDbType.DateTime),
                new SqlParameter("@endTime",SqlDbType.DateTime),
                new SqlParameter("@CreatorDepId",SqlDbType.Int),
                new SqlParameter("@CreatorEmpId",SqlDbType.Int),
            };

            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and (o.OaFileName like '%'+@TextCondtion+'%') ");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "startTime":
                            if (de.Value.ToString() != "")
                            {
                                paras[1].Value = Convert.ToDateTime(de.Value);
                                strSql.Append(" and o.CreationTime>=@startTime");
                            }
                            break;
                        case "endTime":
                            if (de.Value.ToString() != "")
                            {
                                paras[2].Value = Convert.ToDateTime(de.Value).AddHours(23.99);
                                strSql.Append(" and o.CreationTime<=@endTime");
                            }
                            break;
                        case "QueryDeptID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and o.CreatorDepId=@CreatorDepId");
                                    paras[3].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "QueryEmpID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and o.CreatorEmpId=@CreatorEmpId");
                                    paras[4].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
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
                queryContext.SelectOrder = "o.Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        public void UpdateOaFileSendList(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update OaFileSend set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
