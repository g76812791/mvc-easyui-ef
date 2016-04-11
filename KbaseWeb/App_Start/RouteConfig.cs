using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KbaseWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Error", "Error.html", new { Controller = "Error", Action = "Index" });
            routes.MapRoute("Default", "", new { Controller = "Kbase", Action = "Index" });
            routes.MapRoute("Login", "Login.html", new { Controller = "Login", Action = "Index" });
            routes.MapRoute("do", "{Controller}/{Action}.do");
            routes.MapRoute("html", "{Controller}/{Action}.html");
        }
    }
}