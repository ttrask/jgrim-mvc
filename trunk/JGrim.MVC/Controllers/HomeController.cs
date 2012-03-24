using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiaLibrary.Web;

namespace JGrim.MVC.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
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
                return View("Pages/About");
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
