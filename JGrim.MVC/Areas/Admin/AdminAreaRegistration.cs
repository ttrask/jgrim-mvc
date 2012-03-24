using System.Web.Mvc;

namespace JGrim.MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Admin_default1",
                "Admin/{controller}.aspx/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional },
                new string[] { "JGrim.MVC.Areas.Admin.Controllers" }
            ); 
            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {controller="home", action = "Index", id = UrlParameter.Optional },
                new string[] { "JGrim.MVC.Areas.Admin.Controllers" }
            );

        }
    }
}
