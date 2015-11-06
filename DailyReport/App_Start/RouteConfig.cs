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

            routes.MapRoute(
                name: "Chart",                    // 라우트 이름
                url: "Chart/{action}/{start}/{end}", // URL (매개변수 포함)
                //defaults: new { controller = "Chart" ,start = DateTime.Today, end =DateTime.Today.AddMonths(-1) }
                defaults: new { controller = "Chart", start = new DateTime(2014,10,1), end = new DateTime(2014, 12, 31) }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
