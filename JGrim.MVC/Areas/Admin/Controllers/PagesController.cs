using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JGrim.MVC.Services;
using RiaLibrary.Web;
using JGrim.MVC.Models;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    [Authorize]
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

            string pageUrl = Request.Path.Substring(Request.Path.LastIndexOf("/")).Replace("/", "");


            var page = (from p in db.Pages where p.Url==pageUrl select p).FirstOrDefault<Page>();


            if (page == null)
                page = (from dbPage in db.Pages where dbPage.Url == ID select dbPage).FirstOrDefault<Page>();
            else
            {
                if (page == null)
                    page = (from dbPage in db.Pages where dbPage.Name.Equals(ID) select dbPage).FirstOrDefault<Page>();
                else
                {
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
                }
            }
            
            return View(page == null ? new Page() : page);
        }

        public virtual ActionResult Admin(string ID)
        {
            return View(db.Pages.ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Edit(String ID)
        {
            try
            {
                int pageID=0;
                Int32.TryParse(ID, out pageID);

                if (pageID > 0)
                {
                    return View((from dbPage in db.Pages where dbPage.PageID == pageID select dbPage).FirstOrDefault<Page>());

                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }

            
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit(FormCollection form)
        {
            int id=Int32.Parse(form["PageID"]);
            
            Page pageToUpdate = db.Pages.First(p => p.PageID == id );
            TryUpdateModel<Page>(pageToUpdate, form.ToValueProvider());

            db.SaveChanges(true);

            return RedirectToAction("Admin");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Create()
        {
            return View(new Page());
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(FormCollection form)
        {

            Page pageToAdd = new Page();
            TryUpdateModel<Page>(pageToAdd,  form.ToValueProvider());

            db.AddToPages(pageToAdd);
            db.SaveChanges(true);

            return RedirectToAction("Admin");
        }

        /*
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string ID)
        {
            try
            {
                int pageID = 0;
                Int32.TryParse(ID, out pageID);

                if (pageID > 0)
                {
                    return View(db.Pages.First(p => p.PageID == pageID));
                }
            }
            catch
            {
                return View();
            }

            return View();
        }
        */
        
        
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete(string ID)
        {
            try
            {
                int pageID = 0;
                Int32.TryParse(ID, out pageID);

                if (pageID > 0)
                {
                    Page pageToDelete = db.Pages.First(p => p.PageID == pageID);

                    db.DeleteObject(pageToDelete);
                    db.SaveChanges();
                    RedirectToAction("Admin");
                }
            }
            catch
            {
                RedirectToAction("Admin");
            }

            RedirectToAction("Admin");
            return View();
        }
                

    }
}
