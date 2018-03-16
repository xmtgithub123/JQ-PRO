﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-12 14:56:40
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DBSql.Project
{
    public class ProjSub : EFRepository<DataModel.Models.ProjSub>
    {
        public int GetProjCodeCount(string Number, int Id = 0)
        {
            int Res = 0;
            string sql = "select count(*) from ProjSub where SubNumber=@Number and DeleterEmpId = 0 ";

            if (Id > 0)
            {
                sql += " and Id!=" + Id;
            }

            SqlParameter[] sqlp = {
                new SqlParameter("@Number",SqlDbType.NVarChar)
            };
            sqlp[0].Value = Number.Trim();
            try
            {
                Res = Convert.ToInt32(DBExecute.ExecuteScalar(sql, sqlp));
            }
            catch { }
            return Res;
        }

        public void UpdateProjSubInfoList(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update ProjSub set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
        public void UpdateProjSubByConSubIDs(string ids)
        {
            var sbSQL = new StringBuilder();
            sbSQL.Append(string.Format("Update ProjSub set ConSubID=0 WHERE ID IN ({0})", ids.TrimEnd(',')));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }

        public DataTable GetListInfo(Common.SqlPageInfo queryContext, DataModel.EmpSession userInfo = null)
        {
            string RowColumn = @"s.Id ,
                            p.ProjName ,
                            p.ProjNumber ,
                            s.SubNumber ,
                            s.SubName ,
                            s.ColAttDate1 ,
                            s.CreationTime ,
                            s.ConSubID ,
                            Cust.CustName ,
                            s.CreatorEmpId ,
                            ISNULL(( SELECT BaseName
                                     FROM   BaseData
                                     WHERE  BaseID = s.SubType
                                   ), '') AS SubTypeName ,
                            ISNULL(( SELECT EmpName
                                     FROM   BaseEmployee
                                     WHERE  EmpID = s.SubEmpId
                                   ), '') AS SubEmpName ,
                            f.Id AS FlowID ,
                            f.FlowName ,
                            f.FlowStatusID ,
                            f.FlowStatusName ,
                            f.FlowXml.value('(Root/TurnedEmpIDs/text())[1]', 'nvarchar(200)') AS FlowTurnedEmpIDs ";

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT  Count(1)
                        FROM    dbo.ProjSub AS s
                                INNER JOIN Project AS p ON s.ProjID = p.Id
                                LEFT JOIN BussCustomer AS Cust ON s.ColAttType2 = Cust.Id
                                LEFT JOIN Flow f ON f.FlowRefID = s.Id
                                                    AND f.FlowRefTable = @RefTable
                        WHERE   s.DeleterEmpId = 0 ");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@ColAttDate1S",SqlDbType.DateTime),
                new SqlParameter("@ColAttDate1E",SqlDbType.DateTime),
                new SqlParameter("@CreationTimeS",SqlDbType.DateTime),
                new SqlParameter("@CreationTimeE",SqlDbType.DateTime),
                new SqlParameter("@ConSubID",SqlDbType.Int),
                new SqlParameter("@CompanyID",SqlDbType.Int),
                new SqlParameter("@RefTable",SqlDbType.VarChar),
            };

            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and (p.ProjName like '%'+@TextCondtion+'%' or p.ProjNumber like '%'+@TextCondtion+'%' or s.SubNumber like '%'+@TextCondtion+'%' or s.SubName like '%'+@TextCondtion+'%' or Cust.CustName like '%'+@TextCondtion+'%') ");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "ColAttDate1S":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.ColAttDate1>=@ColAttDate1S");
                                paras[1].Value = Convert.ToDateTime(de.Value);
                            }
                            break;
                        case "ColAttDate1E":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.ColAttDate1<=@ColAttDate1E");
                                paras[2].Value = Convert.ToDateTime(de.Value).AddHours(23.99);
                            }
                            break;
                        case "CreationTimeS":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.CreationTime>=@CreationTimeS");
                                paras[3].Value = Convert.ToDateTime(de.Value);
                            }
                            break;
                        case "CreationTimeE":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.CreationTime<=@CreationTimeE");
                                paras[4].Value = Convert.ToDateTime(de.Value).AddHours(23.99);
                            }
                            break;
                        case "ProjSubTypeState":
                            {
                                strSql.Append(string.Format(" and s.SubType in ({0})", de.Value.ToString()));
                            }
                            break;
                        case "IsAudit":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and (SELECT COUNT(*) FROM dbo.Flow WHERE FlowRefTable=@RefTable AND FlowRefID=s.id AND  FlowStatusID=3)>0");
                                }
                            }
                            break;
                        case "ConSubID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and s.ConSubID=@ConSubID");
                                    paras[5].Value = Convert.ToInt32(de.Value);
                                }
                            }
                            break;
                        case "FilterConSubID":
                            {
                                if (de.Value.ToString() == "1")
                                {
                                    strSql.Append(" and s.ConSubID=0");
                                }
                            }
                            break;
                        case "CompanyID":
                            strSql.Append(" and s.CompanyID=@CompanyID ");
                            paras[6].Value = de.Value.ToString();
                            break;
                        case "RefTable":
                            paras[7].Value = de.Value.ToString();
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
                queryContext.SelectOrder = "s.Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        public DataTable GetProjSubContractList(Common.SqlPageInfo queryContext)
        {
            string RowColumn = "s.Id, s.SubNumber,s.SubName,p.ProjName,p.ProjNumber,Cust.CustName,s.ColAttDate1,s.CreationTime,contractSub.ConSubName,contractSub.ConSubNumber,contractSub.ConSubFee";
            RowColumn += ", isnull((SELECT  BaseName + ','FROM BaseData  WHERE  CHARINDEX(','+CAST(BaseID AS  nvarchar(5))+',',','+s.SubHZSJ+',')>0 FOR XML PATH('')),'')  as HZSJName";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select Count(1) from dbo.ProjSub AS s inner join  Project  as p on s.ProjID=p.Id 
                                                                   left join BussContractsub as contractSub on s.ConSubID=contractSub.Id
                                                                   left join BussCustomer as Cust on s.ColAttType2=Cust.Id                                                               
                                                                   where s.DeleterEmpId=0 ");

            SqlParameter[] paras = {
                new SqlParameter("@ProjSubID",SqlDbType.Int),
            };

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {

                        case "ProjSubID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and s.Id=@ProjSubID");
                                    paras[0].Value = Convert.ToInt32(de.Value);
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
                queryContext.SelectOrder = "s.Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }
    }
}
