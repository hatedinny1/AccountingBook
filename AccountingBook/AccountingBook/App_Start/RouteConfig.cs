using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AccountingBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapMvcAttributeRoutes();

            //記帳本V1用
            routes.MapRoute(
                name: "AccountingBook",
                url: "skilltree/{year}/{month}",
                defaults: new { controller = "AccountingBook", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional },
                namespaces: new[] { "AccountingBook.Controllers" }
                );

            //Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AccountingBook.Controllers" }
            );
        }
    }
}
