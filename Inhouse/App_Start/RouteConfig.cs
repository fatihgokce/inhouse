using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inhouse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
  "Context",
  "Context/{lang}/{pageId}/{pageName}",
new { controller = "Page", action = "Index", lang = "",pageId="",pageName="" });
            routes.MapRoute(
   "Detail",
   "{lang}/Detail",
new { controller = "Project", action = "Detail", lang = "" });
            routes.MapRoute(
        "ListProject","{lang}/ListProject",
             new { controller = "Project", action = "ListProject", lang = "" });
            routes.MapRoute("InsertContact",
             "Ajax/{action}",
             new { controller = "Project", action = "InsertContact" });
            routes.MapRoute(
               "Default", // Route name
               "{lang}/{controller}",
               new { controller = "Home", action = "Index", lang = "" } // Parameter defaults

           );
            //routes.MapRoute(
            //    "Default2", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);
           
            routes.MapRoute(
              "Root",
              "{lang}/{controller}/{action}",
              new { controller = "Home", action = "Index", lang = "" });
           
        }
    }
}