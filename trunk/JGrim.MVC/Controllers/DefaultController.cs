using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JGrim.MVC.Controllers
{
    public partial class DefaultController : Controller
    {
        //
        // GET: /Default/

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Admin()
        {
            return View();
        }

    }
}
