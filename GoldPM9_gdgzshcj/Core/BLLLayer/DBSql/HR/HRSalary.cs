﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-16 10:46:59
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
    public class HRSalary : EFRepository<DataModel.Models.HRSalary>
    {
        /// <summary>
        /// 查询薪资Table
        /// </summary>
        /// <returns></returns>
        public DataTable GetHRSalaryTable(Common.SqlPageInfo sqlMod)
        {
            //定义查询列
            string RowColumn = @"
                s.Id,
	            bd.BaseName as DepName,
	            e.EmpName,
	            s.HireTypeID,
	            isnull((select top(1)BaseName from BaseData where BaseData.BaseID=s.HireTypeID),'')as HireTypeName,
	            s.BaseSalary,
	            s.MeritSalary
            ";
            //定义查询体
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select Count(1) from HRSalary as s inner join HREmployee as e on s.EmpID=e.Id
inner join BaseData as bd on e.DepID = bd.BaseID where 1=1  ");
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
                        case "DepID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and e.DepID={0}", de.Value.ToString());
                            }
                            break;
                        case "EmpStatusID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and e.EmpStatusID={0}", de.Value.ToString());
                            }
                            break;
                        case "HireTypeID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and s.HireTypeID={0}", de.Value.ToString());
                            }
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
                            strSql.AppendFormat(" and e.CompanyID IN (0,1537,1538,1612)", de.Value.ToString());
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
                sqlMod.SelectOrder = "bd.BaseOrder,e.Id";
            }

            //将组装后的sqlMod组装成sql语句
            string sql = Helper.SqlPage.ExecPageStrSql(sqlMod, RowColumn, strSql);
            //执行sql语句
            DataTable dt = DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString());
            return dt;
        }
    }
}
