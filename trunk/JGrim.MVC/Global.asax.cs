using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using RiaLibrary.Web;



namespace JGrim.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static string _defaultPage = ConfigurationManager.AppSettings["DefaultPage"].ToString();

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            /*
            routes.MapRoute(
                "AdminRoot",                                              // Route name
                "Admin",                           // URL with parameters
                new { controller = "Home", action = "Admin", id = UrlParameter.Optional }  // Parameter defaults
            );
             */

            /*routes.MapRoute(
                "Admin",                                              // Route name
                "Admin/{controller}.aspx/{id}",                           // URL with parameters
                new { controller = "Home", action = "Admin", id = UrlParameter.Optional }  // Parameter defaults
            );*/

            routes.MapRoute(
                "Photos",                                              // Route name
                "Photos.aspx",                           // URL with parameters
                new { controller = "Photos", action = "Index" },
                new string[] { "JGrim.MVC.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
                "Pages",                                              // Route name
                "Pages/{id}",                           // URL with parameters
                new { controller = "Pages", action = "Index", id = _defaultPage },
                new string[] { "JGrim.MVC.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}.aspx/{action}/{id}",                           // URL with parameters
                new { controller = "Pages", action = "Index", id = _defaultPage},  // Parameter defaults
                new string[] { "JGrim.MVC.Controllers" }
            );

            routes.MapRoute(
                "Default1",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Pages", action = "Index", id = _defaultPage },
                new string[] { "JGrim.MVC.Controllers" }// Parameter defaults
            );


            routes.RouteExistingFiles = false;
            routes.MapRoutes();

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            
        }
    }
}