using JQ.BPM.Core.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using JQ.BPM.Data.Extern;

namespace DBSql.BPMExtend
{
    public class BaseData : IBaseData
    {
        public BaseData(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbContext dbContext;

        public DataTable GetCategory(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return dbContext.Database.ExecuteDataTable("SELECT BaseID AS ID,BaseName AS Name FROM BaseData WHERE BaseNameEng!=''");
            }
            else
            {
                return dbContext.Database.ExecuteDataTable("SELECT BaseID AS ID,BaseName AS Name FROM BaseData WHERE BaseNameEng!='' AND Name=@name", new System.Data.SqlClient.SqlParameter("@name", name));
            }
        }

        public DataTable GetItems(string id)
        {
            return dbContext.Database.ExecuteDataTable("SELECT BaseID AS ID,BaseName AS Name,[BaseOrder] FROM BaseData WHERE [BaseOrder] LIKE (SELECT TOP 1 BaseOrder FROM BaseData WHERE BaseID=" + id + ")+'_%' ORDER BY [BaseOrder]");
        }
    }
}
