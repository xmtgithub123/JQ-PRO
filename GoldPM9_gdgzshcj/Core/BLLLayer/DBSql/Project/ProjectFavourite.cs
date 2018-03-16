using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Project
{
    public class ProjectFavourite : EFRepository<DataModel.Models.ProjectFavourite>, IDisposable
    {
        public List<dynamic> GetProjectList(int empID, int amount)
        {
            return (from t in this.DbContext.Set<DataModel.Models.ProjectFavourite>()
                    join t1 in this.DbContext.Set<DataModel.Models.Project>() on t.ProjectId equals t1.Id
                    where t.EmpID == empID
                    orderby t.CreationTime descending
                    select new { t1.Id, t1.ProjNumber, t1.ProjName, ProjectTypeName = t1.FK_Project_ProjTypeID.BaseName, ProjectVoltName = t1.FK_Project_ProjVoltID.BaseName, t1.DatePlanStart }).Take(amount).ToList<dynamic>();
        }

        private bool isDisposed = false;
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }
    }
}
