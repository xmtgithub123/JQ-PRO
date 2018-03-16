using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RazorGenerator.Mvc;
using System.Web.Compilation;
using System.Web.WebPages;
using System.Web.Mvc;
using System.IO;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(JQWebMVC.App_Start.Startup))]
//#if DEBUG
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(JQWebMVC.App_Start.RazorGeneratorMvcStart), "Start")]
//#endif
namespace JQWebMVC.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            var engine = new JQWebMVC.App_Start.JQPrecompiledMvcEngine();
            ViewEngines.Engines.Insert(0, engine);
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
#endif
            app.MapSignalR();
        }
    }

    public class JQPrecompiledMvcEngine : VirtualPathProviderViewEngine, IVirtualPathFactory
    {
        public JQPrecompiledMvcEngine()
        {
            base.AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            base.AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            base.AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };
            base.ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            base.MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            base.PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            base.FileExtensions = new[] {
                "cshtml",
            };
            this.ViewLocationCache = new PrecompiledViewLocationCache(typeof(JQPrecompiledMvcEngine).Assembly.FullName, this.ViewLocationCache);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            partialPath = EnsureVirtualPathPrefix(partialPath);
            return CreateViewInternal(partialPath, masterPath: null, runViewStartPages: false);
        }

        private IView CreateViewInternal(string viewPath, string masterPath, bool runViewStartPages)
        {
            var names = viewPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 6)
            {
                return null;
            }
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')) + "\\" + names[2] + "\\Views\\" + names[4].ToString() + "\\" + Path.GetFileNameWithoutExtension(names[5]) + ".generated.cs";
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }
            using (var cSharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider())
            {
                var result = cSharpCodeProvider.CompileAssemblyFromFile(GetDefaultCompilerParameters(), path);
                if (!result.Errors.HasErrors)
                {
                    return new PrecompiledMvcView(viewPath, masterPath, result.CompiledAssembly.GetTypes()[0], runViewStartPages, base.FileExtensions, DefaultViewPageActivator.Current);
                }
            }
            return null;
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            viewPath = EnsureVirtualPathPrefix(viewPath);
            return CreateViewInternal(viewPath, masterPath, runViewStartPages: true);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return IsExistsPath(virtualPath);
        }

        internal static string EnsureVirtualPathPrefix(string virtualPath)
        {
            if (!String.IsNullOrEmpty(virtualPath))
            {
                if (!virtualPath.StartsWith("~/", StringComparison.Ordinal))
                {
                    virtualPath = "~/" + virtualPath.TrimStart(new[] { '/', '~' });
                }
            }
            return virtualPath;
        }

        private bool IsExistsPath(string virtualPath)
        {
            var names = virtualPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 6)
            {
                //if (names[5] == "_Header.cshtml" || names[5] == "_Layout.cshtml" || names[5] == "Error.cshtml")
                //{
                //    return File.Exists(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')) + "\\" + names[2] + "\\Areas\\Core\\Views\\" + names[4].ToString() + "\\" + Path.GetFileNameWithoutExtension(names[5]) + ".generated.cs");
                //}
                //else
                //{
                return File.Exists(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')) + "\\" + names[2] + "\\Views\\" + names[4].ToString() + "\\" + Path.GetFileNameWithoutExtension(names[5]) + ".generated.cs");
                //}
            }
            return false;
        }

        public bool Exists(string virtualPath)
        {
            return false;
            //return IsExistsPath(virtualPath);
        }

        public object CreateInstance(string virtualPath)
        {
            if (VirtualPathProvider.FileExists(virtualPath))
            {
                return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(WebPageRenderingBase));
            }
            else
            {
                var names = virtualPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length != 6)
                {
                    return null;
                }
                var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')) + "\\" + names[2] + "\\Areas\\Core\\Views\\" + names[4].ToString() + "\\" + Path.GetFileNameWithoutExtension(names[5]) + ".generated.cs";
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    return null;
                }
                using (var cSharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider())
                {
                    var result = cSharpCodeProvider.CompileAssemblyFromFile(GetDefaultCompilerParameters(), path);
                    if (!result.Errors.HasErrors)
                    {
                        return DefaultViewPageActivator.Current.Create((ControllerContext)null, result.CompiledAssembly.GetTypes()[0]);
                    }
                }
                return null;
            }
        }

        private System.CodeDom.Compiler.CompilerParameters GetDefaultCompilerParameters()
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var result = new System.CodeDom.Compiler.CompilerParameters();
            result.ReferencedAssemblies.Add("mscorlib.dll");
            result.ReferencedAssemblies.Add("System.dll");
            result.ReferencedAssemblies.Add("System.Core.dll");
            result.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            //s.ReferencedAssemblies.Add("System.Collections.Generic");
            result.ReferencedAssemblies.Add("System.IO.dll");
            result.ReferencedAssemblies.Add("System.Net.dll");
            //s.ReferencedAssemblies.Add("System.Text.dll");
            result.ReferencedAssemblies.Add("System.Linq.dll");
            result.ReferencedAssemblies.Add("System.Web.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\System.Web.Optimization.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\System.Web.Helpers.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\System.Web.Mvc.dll");
            //s.ReferencedAssemblies.Add("System.Web.MVC.Ajax.dll");
            //s.ReferencedAssemblies.Add("System.Web.MVC.Html.dll");
            result.ReferencedAssemblies.Add("System.Web.Routing.dll");
            //s.ReferencedAssemblies.Add("System.Web.Security.dll");
            //s.ReferencedAssemblies.Add("System.Web.UI.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\System.Web.WebPages.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\RWP.FrameworkForWeb.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\RWP.Util.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\DataModel.dll");
            result.ReferencedAssemblies.Add(appPath + @"bin\DBSql.dll");
            result.GenerateExecutable = false;
            result.GenerateInMemory = true;
            return result;
        }
    }
}