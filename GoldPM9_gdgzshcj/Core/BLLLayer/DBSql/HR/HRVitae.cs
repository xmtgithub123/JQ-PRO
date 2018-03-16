﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-11 10:30:33
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Collections;
using System.Data;

namespace DBSql.Hr
{
    public class HRVitae : EFRepository<DataModel.Models.HRVitae>
    {
        /// <summary>
        /// 查询简历Table
        /// </summary>
        /// <returns></returns>
        public DataTable GetHRVitaeTable(Common.SqlPageInfo sqlMod)
        {
            //定义查询列
            string RowColumn = @"
                v.Id,
	            v.EmpID,
	            e.EmpName,
	            v.OldCom,
	            v.OldDep,
	            v.OldPost,
	            v.StarDate,
	            v.EndDate
            ";
            //定义查询体
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(1) from HRVitae as v inner join HREmployee as e on v.EmpID=e.Id where 1=1  ");
            //定义模糊查询条件
            if (!string.IsNullOrEmpty(sqlMod.TextCondtion))
            {
                strSql.AppendFormat(" and ( ");
                strSql.AppendFormat(" e.EmpName Like '%{0}%' ", sqlMod.TextCondtion);
                strSql.AppendFormat(" ) ");
            }
            //定义赋值查询条件
            if (sqlMod.SelectCondtion != null && sqlMod.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in sqlMod.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "StarDateBegin":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),v.StarDate,111))>='{0}'", de.Value.ToString());
                            break;
                        case "StarDateEnd":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),v.StarDate,111))<='{0}'", de.Value.ToString());
                            break;
                        case "EndDateBegin":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),v.EndDate,111))>='{0}'", de.Value.ToString());
                            break;
                        case "EndDateEnd":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),v.EndDate,111))<='{0}'", de.Value.ToString());
                            break;
                        case "CreatorDepId":
                            strSql.AppendFormat(" and e.DepID={0}", de.Value.ToString());
                            break;
                        case "CreateEmpId":
                            strSql.AppendFormat(" and e.SysEmpId={0}", de.Value.ToString());
                            break;
                        case "CompanyID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and e.CompanyID={0}", de.Value.ToString());
                            }
                            break;
                        case "CompanyTS":
                            {
                                strSql.AppendFormat(" and e.CompanyID IN (0,1537,1538,1612)", de.Value.ToString());
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            //定义总记录数
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString());
            if (obj == null && obj == DBNull.Value)
            {
                sqlMod.PageTotleRowCount = 0;
            }
            else
            {
                sqlMod.PageTotleRowCount = Convert.ToInt32(obj);
            }
            //排序
            if (String.IsNullOrEmpty(sqlMod.SelectOrder))
            {
                sqlMod.SelectOrder = "v.Id";
            }

            //将组装后的sqlMod组装成sql语句
            string sql = Helper.SqlPage.ExecPageStrSql(sqlMod, RowColumn, strSql);
            //执行sql语句
            DataTable dt = DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString());
            return dt;
        }
    }
}
