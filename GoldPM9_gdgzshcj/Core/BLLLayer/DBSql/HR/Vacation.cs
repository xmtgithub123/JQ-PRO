﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-03 10:28:47
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
namespace DBSql.Hr
{
    public class Vacation : EFRepository<DataModel.Models.Vacation>
    {
        public DataTable GetVacations()
        {
            string strSql = "select * from Vacation order by id desc";
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, strSql);
             

        }

        public string GetDays(int eid)
        {
            string strSql = "select VacationDays from HREmployee where ID=" + eid;
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, strSql).Rows[0][0].ToString();
        }

        public string GetCount(int empId)
        {
            string strSql = " select COUNT(1) from  (select PermissionEmpID from BaseEmpPermission where PermissionEmpValue in (select BaseID from BaseData where BaseName like '%总工%' or BaseName like '%所长%') and PermissionEmpID = " + empId + " ) t1 ";
            try
            {
                return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, strSql).Rows[0][0].ToString();
            }
            catch { return "0"; }
        }
    }
}
