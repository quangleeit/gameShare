using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameMob
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

            //routes.MapRoute(
            //    name: "Search",
            ////    url: "{controller}/{action}/{id}/{catchall}",
            //    defaults: new { controller = "Search", action = "SearchResult", id = UrlParameter.Optional }
            //);

          //  routes.MapRoute(
          //    name: "Delte",
          //    url: "{controller}/{action}/{id}",
          //    defaults: new { controller = "Gamemanagement", action = "Delete", id = UrlParameter.Optional }
          //);
        }
    }
}
