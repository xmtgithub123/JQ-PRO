using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQWebMVC
{
    public class JQEngine : RazorViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext.RouteData.DataTokens["area"] != null)
            {
                var appPath = AppDomain.CurrentDomain.BaseDirectory;
                var area = controllerContext.RouteData.DataTokens["area"].ToString();
                var controllerName = controllerContext.Controller.GetType().Name.Replace("Controller", "");
                var realPath = Path.GetDirectoryName(appPath.TrimEnd('\\')) + "\\" + area + "\\Views\\" + controllerName + "\\" + viewName + ".cshtml";
                if (File.Exists(realPath))
                {
                    var targetPath = appPath + "\\Areas\\" + area + "\\Views\\" + controllerName;
                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    string targetPathViewName = targetPath + "\\" + viewName + ".cshtml";
                    bool IsSameView = File.Exists(targetPathViewName) && File.GetLastWriteTime(realPath).Equals(File.GetLastWriteTime(targetPathViewName));
                    if (!IsSameView)
                        File.Copy(realPath, targetPathViewName, true);
                }
            }
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override void ReleaseView(ControllerContext controllerContext, IView view)
        {
            base.ReleaseView(controllerContext, view);
        }
    }
}