using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JGrim.MVC.Models;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public partial class DefaultController : AuthenticatedPageController
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
