using System;
using System.Data;
using System.Data.SqlClient;
using DataModel.Models;
using System.Collections.Generic;

namespace DBSql
{
    public class BaseEmployeeData
    {
        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>结果集</returns>
        public Dictionary<int, string> Get(int[] Ids)
        {
            Dictionary<int, string> empDic = new Dictionary<int, string>();
            string idStr = string.Empty;
            foreach (int i in Ids)
            {
                idStr += (i.ToString() + ",");
            }

            DataTable dt = new DataTable();
            string sqlStr = string.Format("SELECT EmpID,EmpName FROM BaseEmployee WHERE EmpID in ({0})", idStr.Trim(','));
            SqlHelper.FillDataTable(sqlStr, dt, null);

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["EmpID"]) > 0)
                {
                    empDic.Add(Convert.ToInt32(dr["EmpID"]), dr["EmpName"].ToString());
                }
            }

            return empDic;
        }
    }
}
