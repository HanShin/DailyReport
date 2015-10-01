using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DailyReport
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",                    // 라우트 이름
                url: "login", // URL (매개변수 포함)
                defaults: new
                {                         // 매개변수 기본값 
                    controller = "Login",
                    action = "Login"
                }
           );

           ol routes.MapRoute(
                name: "Default",
                url: "{contrler}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
