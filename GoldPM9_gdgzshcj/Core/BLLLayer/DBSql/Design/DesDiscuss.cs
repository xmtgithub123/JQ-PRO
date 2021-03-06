﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-30 11:51:43
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
namespace DBSql.Design
{
    public class DesDiscuss : EFRepository<DataModel.Models.DesDiscuss>
    {
        public void UpdateDesDiscussList(int[] ids, DataModel.EmpSession userInfo)
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
            sbSQL.Append(string.Format("Update DesDiscuss set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }

        public int GetTotalReply(int discussID)
        {
            return JQ.Util.TypeParse.parse<int>(DBExecute.ExecuteScalar("SELECT COUNT(1) FROM DesDiscussReply WHERE TalkId=" + discussID + " AND DeleterEmpId=0"));
        }
    }
}
