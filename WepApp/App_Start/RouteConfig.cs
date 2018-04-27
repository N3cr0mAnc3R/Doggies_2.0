﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WepApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Templates",
                url: "template/{*path}",
                defaults: new { controller = "Home", action = "Template" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{*query}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}
