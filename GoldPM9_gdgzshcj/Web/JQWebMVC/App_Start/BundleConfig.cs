using System.Web;
using System;
using System.Web.Optimization;

namespace JQWebMVC
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string themes = string.Empty;
            int pageSize = 0;
            try
            {
                themes = HttpContext.Current.Session["themes"].ToString();
            }
            catch (System.Exception)
            {
                themes = "skin-bluelight";
            }
            try
            {
                pageSize = Convert.ToInt32(HttpContext.Current.Session["pageSize"]);
            }
            catch (System.Exception)
            {
                pageSize = 20;
            }
            bundles.Add(new StyleBundle("~/content/css").Include(
                    "~/Content/site.css",
                    "~/Content/bootstrap.min.css",
                    "~/Content/desflow.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/JQ")
                .Include(
                        "~/Scripts/common.js",
                        "~/Scripts/json2.js",
                        "~/Scripts/JQ/extend.js",
                        "~/Scripts/JQ/dialog.js",
                        "~/Scripts/JQ/textbox.js",
                        "~/Scripts/JQ/combobox.js",
                        "~/Scripts/JQ/combotree.js",
                        "~/Scripts/JQ/combogrid.js",
                        "~/Scripts/JQ/datagrid.js",
                        "~/Scripts/JQ/tree.js",
                        "~/Scripts/JQ/treegrid.js",
                        "~/Scripts/JQ/form.js",
                        "~/Scripts/JQ/tools.js"
                        ));
            bundles.Add(new ScriptBundle("~/linq")
                .Include("~/Scripts/linq.min.js"));
        }
    }
}
