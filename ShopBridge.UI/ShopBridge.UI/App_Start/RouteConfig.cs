﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBridge.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Inventory", action = "Save", id = UrlParameter.Optional }
            );
            
            //routes.MapRoute(
            //    name: "ViewDetails",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Inventory", action = "ViewDetails", id = UrlParameter.Optional }
            //);
        }
    }
}
