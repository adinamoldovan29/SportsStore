﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: null,
               url: "",
               defaults: new
               {
                   controller = "Product",
                   action = "List",
               }
           );

            routes.MapRoute(
              name: null,
              url: "Page{page}",
              defaults: new { controller = "Product", action = "List" }
          );

            routes.MapRoute(
             name: null,
             url: "{category}",
             defaults: new { controller = "Product", action = "List" }
         );

           

            routes.MapRoute(
               name: null,
               url: "{category}/Page{page}",
               defaults: new { controller = "Product", action = "List" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Product", action = "List" }
            );
        }
    }
}
