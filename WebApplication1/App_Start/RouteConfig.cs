using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Auth",
                url: "{controller}/{action}/{useremail}",
                defaults: new { controller = "Auth", action = "fb", useremail = "a@a.a" }
            );
        }
    }
}
