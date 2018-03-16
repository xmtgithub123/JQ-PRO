using Microsoft.Practices.Unity;
using System.Web.Mvc;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Center.Controllers
{
    public class RoleController : JQ.Web.CoreController
    {
        // GET: Center/Role
        public string GetList()
        {
            var queryContext = new Data.QueryContext();
            if (!string.IsNullOrEmpty((Request.Form["keywords"] ?? "").Trim()))
            {
                queryContext.Criteries.Add("Keywords", Request.Form["keywords"]);
            }
            queryContext.IsPaging = false;
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                using (var dt = BPM.Configuration.BPMContext.Resolve<Core.Organization.IRoleProvider>(new ParameterOverride("dbContext", dataAccessor)).GetFullList(queryContext))
                {
                    return AppUtility.ObjectToJson(new
                    {
                        total = dt.Rows.Count,
                        rows = dt
                    });
                }
            }
        }
    }
}