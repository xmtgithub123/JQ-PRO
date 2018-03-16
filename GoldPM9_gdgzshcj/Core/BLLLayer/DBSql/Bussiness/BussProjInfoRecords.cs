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

namespace DBSql.Bussiness
{
    public class BussProjInfoRecords : EFRepository<DataModel.Models.BussProjInfoRecords>
    {
        public int GetProjCodeCount(string ProjCode, int Id = 0)
        {
            int Res = 0;
            string sql = "select count(*) from BussProjInfoRecords where ProjCode=@ProjCode and DeleterEmpId = 0 ";

            if (Id > 0)
            {
                sql += " and Id!=" + Id;
            }

            SqlParameter[] sqlp = {
                new SqlParameter("@ProjCode",SqlDbType.NVarChar)
            };
            sqlp[0].Value = ProjCode.Trim();
            try
            {
                Res = Convert.ToInt32(DBExecute.ExecuteScalar(sql, sqlp));
            }
            catch { }
            return Res;
        }
        public int UpdateBussProjInfoRecordsInfo(DataModel.Models.BussProjInfoRecords baseBussProjInfoRecords)
        {
            int success = 0;
            try
            {
                Edit(baseBussProjInfoRecords);
                UnitOfWork.SaveChanges();
                success = 1;
            }
            catch (Exception ex)
            {

            }
            return success;
        }
        public void UpdateBussProjInfoRecordsList(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update BussProjInfoRecords set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
