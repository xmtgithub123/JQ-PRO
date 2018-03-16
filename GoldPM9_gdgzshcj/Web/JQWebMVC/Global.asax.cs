using JQ.BPM.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JQWebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //清除临时文件夹中最后访问时间超过6小时的文件
            JQ.Util.IO.JQTimer.Default.Begin();
            JQ.Util.IO.JQTimer.Default.OnTimerElapsed += Default_OnTimerElapsed;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            BPMContext.DBNameOrConnectionString = "jqpm7Context";
            BPMContext.IsUseSelfDB = true;
            Database.SetInitializer<JQ.BPM.Data.ProcessAccessor>(null);
            Database.SetInitializer<JQ.BPM.Data.FormAccessor>(null);
            Database.SetInitializer<JQ.BPM.Data.MemberAccessor>(null);
            BPMContext.Register<JQ.BPM.Core.Organization.IUserProvider, DBSql.BPMExtend.UserProvider>();
            BPMContext.Register<JQ.BPM.Core.Organization.IDepartmentProvider, DBSql.BPMExtend.DepartmentProvider>();
            BPMContext.Register<JQ.BPM.Core.Organization.IRoleProvider, DBSql.BPMExtend.RoleProvider>();
            BPMContext.Register<JQ.BPM.Core.Form.IBaseData, DBSql.BPMExtend.BaseData>();
            new Core.Controllers.mainController().AutoEntityFramework();
        }

        private void Default_OnTimerElapsed(DateTime datetime)
        {
#if DEBUG
            if (datetime.Hour == 12 && datetime.Minute == 00)
#else
            if (datetime.Hour == 23 && datetime.Minute == 59)
#endif
            {
                var tempPath = JQ.Util.IOUtil.GetTempPath();
                foreach (var file in Directory.GetFiles(tempPath, "*.*", SearchOption.AllDirectories))
                {
                    if ((DateTime.Now - File.GetLastWriteTime(file)).TotalHours >= 6)
                    {
                        JQ.Util.IOUtil.TryDeleteFile(file);
                    }
                }
                foreach (var directory in Directory.GetDirectories(tempPath, "*", SearchOption.AllDirectories))
                {
                    if (!Directory.Exists(directory))
                    {
                        continue;
                    }
                    if ((DateTime.Now - Directory.GetLastWriteTime(directory)).TotalHours >= 6 && Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).Length == 0)
                    {
                        JQ.Util.IOUtil.TryDeleteDirectory(directory);
                    }
                }
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            JQ.BPM.Configuration.Utility.WriteLog((Exception)e.ExceptionObject);
        }
    }
}
