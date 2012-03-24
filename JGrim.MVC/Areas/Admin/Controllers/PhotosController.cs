using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Data.EntityClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using JGrim.MVC.Extenders;
using JGrim.MVC.Models;
using System.Web.Services;
using RiaLibrary.Web;
using Webdiyer.WebControls.Mvc;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    [Authorize]
    [WebService]
    public partial class PhotosController : Controller
    {
        //
        // GET: /Photos/

        private static string m_dns = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        private JGrimEntities1 db;
        private static string currentAlbum;

        public PhotosController()
        {
            db = new JGrimEntities1();
        }

        public virtual ActionResult Albums(string id, int? page)
        {

            if (String.IsNullOrEmpty(id))
            {
                RedirectToAction("Index", "PhotoAlbums");
            }

            PhotoAlbum album = (from pa in db.PhotoAlbums where pa.Name == id select pa).FirstOrDefault<PhotoAlbum>();

            (from photo in db.Photos where photo.PhotoAlbum.Name == id select photo).OrderByDescending(p => p.CreateDate).ToList<Photo>().ForEach(delegate(Photo photo)
            {
                album.Photos.Add(photo);
            });

            currentAlbum = album.Name;

            return View(album);
        }

        public virtual ActionResult Index()
        {
            /*return View(new List<PhotoAlbum>()
            { new PhotoAlbum(){ Name="Photojournalism", AlbumCoverUrl="~/images/pj_thumb.png"},
                new PhotoAlbum(){ Name="Landscapes", AlbumCoverUrl="~/images/landscape_thumb.png"},
                new PhotoAlbum(){ Name="Portraits", AlbumCoverUrl="~/images/portrait_thumb.png"}
            });*/
            return View(db.PhotoAlbums.ToList());
        }

        public virtual ActionResult Details(int id)
        {
            Photo photo= db.Photos.Include("PhotoAlbum").Where(p=>p.PhotoID==id).First<Photo>();
            
                //Photo photo = db.Photos.Select(Photo p => new Photo() { Name = p.Name, PhotoID = p.PhotoID, CreateDate = p.CreateDate, Description = p.Description, ContentType = p.ContentType, FileName = p.FileName, PhotoAlbum = p.PhotoAlbum, IsActive = p.IsActive }).Where(p.PhotoID == id);
            
            return View(photo);
        }

        //
        // GET: /Photos/Create

        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Photos/Create

        [HttpPost]
        public virtual ActionResult Create(int? page, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Albums", new { @id = currentAlbum, @page = page });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Photos/Edit/5

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            return View((from m in db.Photos.Include("PhotoAlbum") where m.PhotoID == id select m).FirstOrDefault<Photo>());
        }

        //
        // POST: /Photos/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(int id, int? page, FormCollection form)
        {
            try
            {
                Photo photoToUpdate = db.Photos.First<Photo>(p => p.PhotoID == id);
                TryUpdateModel<Photo>(photoToUpdate, form.ToValueProvider());
                db.SaveChanges(true);

                return RedirectToAction("Albums", new { @id = currentAlbum, @page = page });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Photos/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Photos/Delete/5

        [HttpPost]
        public virtual ActionResult Delete(int id, int? page, FormCollection collection)
        {
            try
            {
                Photo pa = (from p in db.Photos where p.PhotoID==id select p).FirstOrDefault<Photo>();
                db.DeleteObject(pa);
                db.SaveChanges();

                return RedirectToAction("Albums", new { @id = currentAlbum, @page = page });
            }
            catch
            {
                return RedirectToAction("Albums", new { @id = currentAlbum, @page = page });
            }
        }

        [HttpPost]
        public virtual ActionResult Disable(int id, int? page, FormCollection collection)
        {
            try
            {
                Photo photoToUpdate = db.Photos.First<Photo>(p => p.PhotoID == id);

                TryUpdateModel<Photo>(photoToUpdate, collection.ToValueProvider());

                photoToUpdate.IsActive = false;
                db.SaveChanges();

                return RedirectToAction("Albums", new { @id = currentAlbum, @page = page });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public virtual ActionResult Enable(int id, int? page, FormCollection collection)
        {
            try
            {
                Photo photoToUpdate = db.Photos.First<Photo>(p => p.PhotoID == id);

                TryUpdateModel<Photo>(photoToUpdate, collection.ToValueProvider());
                photoToUpdate.IsActive = true;
                db.SaveChanges();

                return RedirectToAction("Albums", new { @id = currentAlbum, @page=page });
            }
            catch
            {
                return View();
            }
        }

        public virtual void Admin(string id)
        {
            RedirectToAction("Admin", "PhotoAlbum", null);

        }
    }
}
