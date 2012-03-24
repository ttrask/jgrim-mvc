using System;
using System.Collections.Generic;
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
using JGrim.MVC.Services;
using System.Web.Services;
using RiaLibrary.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace JGrim.MVC.Controllers
{
    [WebService]
    public partial class PhotosController : Controller
    {
        //
        // GET: /Photos/

        private static string m_dns = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        private JGrimEntities1 db;

        public PhotosController()
        {
            db = new JGrimEntities1();
        }


        [HttpGet]
        public virtual ActionResult Albums(string ID)
        {
            PhotoAlbum album = (from m in db.PhotoAlbums where m.Name == ID select m).FirstOrDefault<PhotoAlbum>();


            if (album != null)
            {
                (from p in db.Photos where p.PhotoAlbum.PhotoAlbumID == album.PhotoAlbumID select p).ToList<Photo>().ForEach(
                        delegate(Photo p2)
                        {
                            album.Photos.Add(p2);
                        });
            }

            return View(album);
        }

        byte[] GetBytesFromCache(string path)
        {
            object fromCache = HttpRuntime.Cache.Get(path);
            if (fromCache == null) return null;

            try
            {
                return fromCache as byte[];
            }
            catch
            {
                //the object in the cache wasn't a byte array 
                //that's sad and perplexing... 
                return null;
            }
        }


        public virtual FileResult ScaledImage(int? id, string albumName, string imageName, int? width, int? height)
        {
            string src = "";
            if (id > 0)
            {
                Photo photo = db.Photos.Where(p => p.PhotoID == id).FirstOrDefault<Photo>();
                photo.PhotoAlbumReference.Load();
                src = Path.Combine(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["photoPath"], photo.PhotoAlbum.Name), photo.FileName).Replace("\\", "/");
            }
            else
            {
                if (String.IsNullOrEmpty(imageName))
                {
                    src = (db.PhotoAlbums.Where(pa => pa.Name == albumName)).FirstOrDefault<PhotoAlbum>().AlbumCoverUrl;
                }
                else
                {
                    src = Path.Combine(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["photoPath"], albumName), imageName).Replace("\\", "/");
                }
            }

            src = Server.MapPath(src);
            Image img = PhotoService.ResizeImage(src, width.HasValue ? width.Value : 0, height.HasValue ? height.Value : 0);
            MemoryStream ms = new MemoryStream();
            try
            {
                img.Save(ms, ImageFormat.Jpeg);
                return File(ms.ToArray(), ImageFormat.Jpeg.ToString());
            }
            catch { }

            return null;

        }

        [HttpPost]
        public virtual ActionResult Albums(string ID, string refID)
        {

            PhotoAlbum album = new PhotoAlbum();


            album.Name = ID;

            if (String.IsNullOrEmpty(ID))
            {
                Response.Redirect("~/Photos.aspx");
            }
            else
            {

                string photoPath = Server.MapPath("~/images/photos/");

                DirectoryInfo di = new DirectoryInfo(photoPath + ID + "\\");

                FileInfo[] files = di.GetFiles("*.jpg");




                foreach (FileInfo fi in files)
                {
                    try
                    {

                        album.Photos.Add(new Photo()
                        {
                            Name = fi.Name,
                        });
                    }
                    catch { throw; }
                }


                return View(album);
            }

            return View(new PhotoAlbum() { Name = "", Description = "", AlbumCoverUrl = "" });
        }

        //
        // GET: /Photos/Details/5


        public virtual ActionResult Index()
        {
            return View(db.PhotoAlbums.Where(p => (p.Photos.Count > 0 && p.IsActive == true)).ToList());
        }

        public virtual ActionResult Details(int id)
        {
            return View();
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
        // GET: /Photos/Edit/5

        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Photos/Edit/5

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
        // GET: /Photos/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Photos/Delete/5


        public virtual ActionResult Admin(string id)
        {
            Response.Redirect("~/Admin/");
            return View();

        }
    }
}
