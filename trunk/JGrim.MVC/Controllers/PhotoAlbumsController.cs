using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JGrim.MVC.Models;

namespace JGrim.MVC.Controllers
{
    public partial class PhotoAlbumsController : Controller
    {
        //
        // GET: /PhotoAlbum/

        private JGrimEntities1 db;

        public PhotoAlbumsController()
        {
            db = new JGrimEntities1();
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PhotoAlbum/Details/5

        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PhotoAlbum/Create

        public virtual ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PhotoAlbum/Create

        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /PhotoAlbum/Edit/5

        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PhotoAlbum/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PhotoAlbum/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PhotoAlbum/Delete/5

        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public virtual ActionResult Admin(int? id)
        {
            return View(db.PhotoAlbums.ToList());
        }
    }
}
