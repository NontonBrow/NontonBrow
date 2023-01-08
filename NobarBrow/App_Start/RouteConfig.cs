using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NobarBrow
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "In Theater Movies",
                url: "InTheaters",
                defaults: new {controller = "Movie", action = "InTheaters"}
            );
            routes.MapRoute(
                name: "Popular",
                url: "Popular",
                defaults: new {controller = "Movie", action = "Popular"}
            );
            routes.MapRoute(
                name: "Search Movie",
                url: "Search",
                defaults: new {controller = "Movie", action = "Search"}
            );
            routes.MapRoute(
                name: "Detail Movie",
                url: "Detail/{id}",
                defaults: new {controller = "Movie", action = "Detail"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Movie", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}