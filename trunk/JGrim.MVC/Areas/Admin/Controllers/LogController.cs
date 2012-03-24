using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JGrim.MVC.Models;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        private JGrimEntities1 db;

        public LogController() {
            db = new JGrimEntities1();
    }

        

        public ActionResult Index()
        {
            return View(new Log());
        }

        //
        // GET: /Admin/Log/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
