using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Carsales
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("Create",
                "{controller}/{action}",
                defaults: new { controller = "Vehicle", action = "Create" }
            );

            routes.MapRoute("Edit",
                "{controller}/{action}",
                defaults: new { controller = "Vehicle", action = "Edit" }
            );

            routes.MapRoute("Details",
                "{controller}/{action}",
                defaults: new { controller = "Vehicle", action = "Details" }
            );

            routes.MapRoute("Delete",
                "{controller}/{action}",
                defaults: new { controller = "Vehicle", action = "Delete" }
            );
        }
    }
}
