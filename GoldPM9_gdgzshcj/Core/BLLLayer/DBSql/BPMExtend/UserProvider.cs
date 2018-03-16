using JQ.BPM.Core.Behavior;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using AppUtility = JQ.BPM.Configuration.Utility;
using JQ.BPM.Core.Organization;
using JQ.BPM.Data.Extern;
using JQ.BPM.Data;
using System;

namespace DBSql.BPMExtend
{
    public class UserProvider : IUserProvider
    {
        public UserProvider(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbContext dbContext;

        public RouteUser Get(string userID)
        {
            return dbContext.Database.ExecuteDataTable("SELECT EmpID AS ID,EmpName AS Name,EmpDepID AS DepartmentID FROM [BaseEmployee] WHERE EmpID=" + userID).ToFirstOrDefault<RouteUser>();
        }

        public List<RouteUser> GetList(string userIDs)
        {
            var sql = "SELECT EmpID AS ID,EmpName AS Name,EmpDepID AS DepartmentID FROM [BaseEmployee] WHERE EmpIsDeleted=0";
            if (!string.IsNullOrEmpty(userIDs))
            {
                sql += " AND EmpID IN (" + userIDs + ")";
            }
            return dbContext.Database.ExecuteDataTable(sql).ToList<RouteUser>();
        }

        public List<RouteUser> GetListByRoleID(string roleIDs, string departmentID = null)
        {
            return dbContext.Database.ExecuteDataTable("SELECT EmpID AS ID,EmpName AS Name,EmpDepID AS DepartmentID FROM [BaseEmployee] WHERE EmpID IN (SELECT PermissionEmpID FROM BaseEmpPermission WHERE PermissionEmpValue IN (" + roleIDs + "))" + (!string.IsNullOrEmpty(departmentID) ? (" AND EmpDepID=" + departmentID) : "")).ToList<RouteUser>();
        }

        public DataTable GetVariableList(QueryContext queryContext)
        {
            queryContext.Columns = "svu.ID,be.EmpID AS UserID,be.EmpName AS UserName,bd.BaseName AS DepartmentName,svu.Value,be.EmpTitle";
            queryContext.Tables = "[BaseEmployee] be LEFT JOIN(SELECT * FROM SystemVariableUser WHERE SystemVariableID=" + queryContext.Criteries["VariableID"] + ") svu ON be.EmpID=svu.UserID";
            queryContext.ExtendJoinTables = "LEFT JOIN BaseData bd ON bd.BaseID=be.EmpDepID";
            queryContext.Conditions = "1=1";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var item in queryContext.Criteries)
            {
                switch (item.Key)
                {
                    case "Keywords":
                        queryContext.Conditions += " AND be.EmpName LIKE @keywords";
                        sqlParameters.Add(new SqlParameter("@keywords", "%" + item.Value + "%"));
                        break;
                    case "DepartmentIDs":
                        queryContext.Conditions += " AND be.EmpDepID IN (" + item.Value + ")";
                        break;
                    case "RoleIDs":
                        queryContext.Conditions += " AND be.EmpID IN (SELECT PermissionEmpID FROM BaseEmpPermission WHERE PermissionEmpValue IN (" + item.Value + "))";
                        break;
                }
            }
            queryContext.Order = "be.EmpIsDeleted DESC,bd.BaseOrder,be.EmpOrder";
            queryContext.TotalRowAmount = AppUtility.ToInt(dbContext.Database.ExecuteScalar(queryContext.GetCalculateSqlString(), sqlParameters.ToArray()));
            return dbContext.Database.ExecuteDataTable(queryContext.GetQuerySqlString(), sqlParameters.ToArray());
        }

        public DataTable GetFullList(QueryContext queryContext)
        {
            queryContext.Columns = "EmpID AS ID,EmpLogin AS Account,EmpName AS Name,EmpDepID AS DepartmentID,EmpOrder AS [Order]";
            queryContext.Tables = "[BaseEmployee]";
            queryContext.Conditions = "EmpIsDeleted=0";
            queryContext.Order = "EmpOrder";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var item in queryContext.Criteries)
            {
                switch (item.Key)
                {
                    case "Keywords":
                        queryContext.Conditions += " AND EmpName LIKE @keywords";
                        sqlParameters.Add(new SqlParameter("@keywords", "%" + item.Value + "%"));
                        break;
                    case "DepartmentIDs":
                        queryContext.Conditions += " AND EmpDepID IN (" + item.Value + ")";
                        break;
                    case "RoleIDs":
                        queryContext.Conditions += " AND EmpID IN (SELECT PermissionEmpID FROM BaseEmpPermission WHERE PermissionEmpValue IN (" + item.Value + "))";
                        break;
                    case "UserIDs":
                        if (!string.IsNullOrEmpty(item.Value.ToString()))
                        {
                            queryContext.Conditions += " AND EmpID IN (" + item.Value.ToString() + ")";
                        }
                        break;
                }
            }
            if (queryContext.IsPaging)
            {
                queryContext.TotalRowAmount = AppUtility.ToInt(dbContext.Database.ExecuteScalar(queryContext.GetCalculateSqlString()));
            }
            return dbContext.Database.ExecuteDataTable(queryContext.GetQuerySqlString());
        }

        public string GetNames(string ids)
        {
            using (var dt = dbContext.Database.ExecuteDataTable("SELECT EmpName FROM [BaseEmployee] WHERE EmpID IN (" + ids + ")"))
            {
                var result = "";
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        result += ",";
                    }
                    result += dt.Rows[i]["EmpName"].ToString();
                }
                return result;
            }
        }
    }
}
