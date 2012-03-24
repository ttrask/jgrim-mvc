using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JGrim.MVC.Services;
using RiaLibrary.Web;
using JGrim.MVC.Models;

namespace JGrim.MVC.Controllers
{
    public partial class PagesController : Controller
    {
        //
        // GET: /Pages/

        private static string m_dns = ConfigurationManager.AppSettings["connectionString"];
        private JGrimEntities1 db;

        public PagesController()
        {
            db = new JGrimEntities1();
        }

        [Url("")]
        public virtual ActionResult Index(string ID = "About.aspx")
        {

            try
            {
                db.Connection.Open();

                string pageUrl = Request.Path.Substring(Request.Path.LastIndexOf("/")).Replace("/", "");


                var page = (from p in db.Pages where p.Url == pageUrl select p).FirstOrDefault<Page>();

                
                if (page == null)
                    page = (from dbPage in db.Pages where dbPage.Url == ID select dbPage).FirstOrDefault<Page>();

                if (page == null)
                    page = (from dbPage in db.Pages where dbPage.Name.Equals(ID) select dbPage).FirstOrDefault<Page>();

                if (page == null)
                {
                    try
                    {
                        page = (from dbPage in db.Pages where dbPage.PageID == Int32.Parse(ID) select dbPage).FirstOrDefault<Page>();
                    }
                    catch (Exception)
                    {
                    }
                }

                db.Connection.Close();


                return View(page == null ? new Page() : page);
            }
            catch (EntityException ex)
            {
                throw new EntityException("Error with Connection String:" + m_dns, ex);
            }
        }

        public virtual ActionResult Admin(string ID)
        {
            Response.Redirect("~/Admin/");
            return View();

        }
    }
}
