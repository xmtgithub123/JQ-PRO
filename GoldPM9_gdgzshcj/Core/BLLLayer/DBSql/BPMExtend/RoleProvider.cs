using JQ.BPM.Core.Organization;
using JQ.BPM.Data;
using JQ.BPM.Data.Extern;
using JQ.BPM.Model.Organization;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace DBSql.BPMExtend
{
    public class RoleProvider : IRoleProvider
    {
        public RoleProvider(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbContext dbContext;

        public Role Get(string id)
        {
            using (var dt = dbContext.Database.ExecuteDataTable("SELECT BaseID AS ID,BaseName AS Name,0 AS [Order],BaseOrder FROM [Role] WHERE BaseID=" + id))
            {
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                var order = dt.Rows[0]["BaseOrder"].ToString();
                dt.Rows[0]["Order"] = AppUtility.ToInt(order.Substring(order.LastIndexOf('_') + 1));
                return dt.ToFirstOrDefault<Role>(false);
            }
        }

        public DataTable GetFullList(QueryContext queryContext)
        {
            queryContext.Columns = "BaseID AS ID,BaseName AS Name,BaseOrder,0 AS [Order]";
            queryContext.Tables = "[BaseData]";
            queryContext.Order = "[BaseOrder]";
            queryContext.Conditions = "BaseOrder LIKE '001_%'";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var item in queryContext.Criteries)
            {
                switch (item.Key)
                {
                    case "Keywords":
                        queryContext.Conditions += " AND BaseName LIKE @keywords";
                        sqlParameters.Add(new SqlParameter("@keywords", "%" + item.Value + "%"));
                        break;
                }
            }
            if (queryContext.IsPaging)
            {
                queryContext.TotalRowAmount = AppUtility.ToInt(dbContext.Database.ExecuteScalar(queryContext.GetCalculateSqlString(), sqlParameters.ToArray()));
            }
            var dt = dbContext.Database.ExecuteDataTable(queryContext.GetQuerySqlString(), sqlParameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                var order = row["BaseOrder"].ToString();
                row["Order"] = AppUtility.ToInt(order.Substring(order.LastIndexOf('_') + 1));
            }
            return dt;
        }

        public List<Role> GetList(QueryContext queryContext)
        {
            queryContext.Columns = "BaseID AS ID,BaseName AS Name,BaseOrder,0 AS [Order]";
            queryContext.Tables = "[BaseData]";
            queryContext.Order = "[BaseOrder]";
            queryContext.Conditions = "BaseOrder LIKE '001_%'";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var item in queryContext.Criteries)
            {
                switch (item.Key)
                {
                    case "Keywords":
                        queryContext.Conditions += " AND BaseName LIKE @keywords";
                        sqlParameters.Add(new SqlParameter("@keywords", "%" + item.Value + "%"));
                        break;
                    case "ids":
                        queryContext.Conditions += " AND BaseID IN (" + item.Value + ")";
                        break;
                }
            }
            if (queryContext.IsPaging)
            {
                queryContext.TotalRowAmount = AppUtility.ToInt(dbContext.Database.ExecuteScalar(queryContext.GetCalculateSqlString(), sqlParameters.ToArray()));
            }
            var dt = dbContext.Database.ExecuteDataTable(queryContext.GetQuerySqlString(), sqlParameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                var order = row["BaseOrder"].ToString();
                row["Order"] = AppUtility.ToInt(order.Substring(order.LastIndexOf('_') + 1));
            }
            return dt.ToList<Role>();
        }

        public string GetNames(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return "";
            }
            using (var dt = dbContext.Database.ExecuteDataTable("SELECT BaseName FROM [BaseData] WHERE BaseID IN (" + ids + ")"))
            {
                var result = "";
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        result += ",";
                    }
                    result += dt.Rows[i]["BaseName"].ToString();
                }
                return result;
            }
        }
    }
}
