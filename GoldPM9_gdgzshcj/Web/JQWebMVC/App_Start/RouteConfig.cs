using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JQWebMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var route = routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "main", action = "userlogin" }
             );
            //routes.MapRoute(
            //   name: "EmptyDefault",
            //   url: "EmptyDefault",
            //   defaults: new { controller = "layout", action = "EmptyDefault" }
            // );
            route.DataTokens["area"] = "Core";
            route = routes.MapRoute(
               name: "Download",
               url: "Download",
               defaults: new { controller = "ProcessFile", action = "Download" }
           );
            route.DataTokens["area"] = "Core";
            route = routes.MapRoute(
              name: "GCADAPI",
              url: "GCADAPI",
              defaults: new { controller = "GCADAPI", action = "Process" }
          );
            route.DataTokens["area"] = "Deisgn";
            route = routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "layout", action = "default", id = UrlParameter.Optional }
           );
            route.DataTokens["area"] = "Core";  //设置默认路由规则的区域


            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
