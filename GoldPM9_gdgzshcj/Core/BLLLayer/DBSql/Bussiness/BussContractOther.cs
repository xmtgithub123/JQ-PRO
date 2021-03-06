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
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace DBSql.Bussiness
{
    public class BussContractOther : EFRepository<DataModel.Models.BussContractOther>
    {
        public DataTable GetContractOtherList(Common.SqlPageInfo condition)
        {
            string RowColumn = "c.*,";
            RowColumn += "isnull ((select Sum(FeeMoney) from BussContractOtherFeeFact bf where bf.RefID=c.Id),0) as SumFeeMoney, ";
            RowColumn += " isnull (( select CustName from  dbo.BussCustomer where id=c.CustID),'') CustNames, ";
            RowColumn += "isnull ((select Sum(InvoiceMoney) from dbo.BussContractOtherInvoice bfi where bfi.RefID=c.Id),0) as SumInvoiceMoney ";
            RowColumn += ",0 as NoFeeMoneyInfo ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from dbo.BussContractOther c where 1=1 ");
            SqlParameter[] paras = {
                    new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                    new SqlParameter("@ConTypeID",SqlDbType.Int),
                    new SqlParameter("@CreatorDepId",SqlDbType.Int),
                    new SqlParameter("@CreatorEmpId",SqlDbType.Int),
                    new SqlParameter("@CompanyID",SqlDbType.Int),
            };

            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and (c.ConNumber like '%'+@TextCondtion+'%' or c.ConrName like '%'+@TextCondtion+'%' or c.CustName like '%'+@TextCondtion+'%'  ) ");
                paras[0].Value = condition.TextCondtion;
            }

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "ConTypeID":
                            if (de.Value.ToString() != "-1")
                            {
                                strSql.Append(" and ConTypeID=@ConTypeID ");
                                paras[1].Value = Convert.ToInt32(de.Value.ToString());
                            }
                            break;
                        case "CustName":
                            if (de.Value.ToString() != "")
                            {
                                //strSql.Append(" and CustName=''+@CustName+'' ");
                                //paras[2].Value = de.Value.ToString();
                            }
                            break;
                        case "QueryDeptID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and CreatorDepId=@CreatorDepId ");
                                    paras[2].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "QueryEmpID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and CreatorEmpId=@CreatorEmpId ");
                                    paras[3].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "CompanyID":
                            strSql.Append(" and c.CompanyID=@CompanyID ");
                            paras[4].Value = Convert.ToInt32(de.Value.ToString());
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
                condition.SelectOrder = "Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);

        }
    }
}
