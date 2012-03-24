using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;
using JGrim.MVC.Models;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    [Authorize]
    [HandleError]
    public partial class HomeController : AuthenticatedPageController
    {

        public virtual ActionResult Index(string ID)
        {
            ViewData["Message"] = "Welcome to the fantastic ASP.NET MVC !";

            if (!String.IsNullOrEmpty(ID))
            {
                return View(ID);
            }
            else
            {
                return View();
            }
        }

        public virtual ActionResult About()
        {
            return View();
        }


        public virtual ActionResult Admin()
        {
            ViewData["Message"] = "Home => Controller";
            return View();
        }

        public virtual ActionResult Contact()
        {
            return View();
        }


    }
}
