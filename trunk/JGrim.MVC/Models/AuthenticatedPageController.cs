using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JGrim.MVC.Models
{
    public class AuthenticatedPageController : Controller
    {
        //
        // GET: /AuthenticatedPage/
        protected override void OnActionExecuting(ActionExecutingContext filterContext ){
        
            if(Session["UserName"]==null){
                RedirectToRoute("Login", "Login");
            }
            base.OnActionExecuting(filterContext);
        }   

    }
}
