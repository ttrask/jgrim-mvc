using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using System.Web.Mvc;
using JGrim.MVC.Models;
using JGrim.MVC.Services;
using System.Configuration;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public partial class PhotoAlbumsController : Controller
    {
        //
        // GET: /PhotoAlbum/

        private static string m_dns = ConfigurationManager.AppSettings["connectionString"];

        private JGrimEntities1 db;

        public PhotoAlbumsController()
        {
            db = new JGrimEntities1();
        }

        public virtual ActionResult Index()
        {
            return View(db.PhotoAlbums);
        }

        //
        // GET: /PhotoAlbum/Details/5

        public virtual ActionResult Details(int id)
        {
            return View((from album in db.PhotoAlbums where album.PhotoAlbumID == id select album).FirstOrDefault<PhotoAlbum>());
        }

        //
        // GET: /PhotoAlbum/Create

        public string GetAlbumUrl(string albumUrl)
        {
            return String.IsNullOrEmpty(albumUrl) ? "~/images/no_image.png" : albumUrl;
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PhotoAlbum/Create

        [HttpPost]
        public virtual ActionResult Create(int? page, FormCollection form)
        {
            try
            {
                PhotoAlbum albumToAdd = new PhotoAlbum();
                TryUpdateModel<PhotoAlbum>(albumToAdd, form.ToValueProvider());
                albumToAdd.AlbumFileLocation=(Path.Combine(ConfigurationManager.AppSettings["PhotoPath"],albumToAdd.Name));
                albumToAdd.Description=String.IsNullOrEmpty(albumToAdd.Description) ? "" : albumToAdd.Description;
                albumToAdd.AlbumCoverUrl = UploadAlbumCover(albumToAdd.Name);
                albumToAdd.IsActive = true;
                db.AddToPhotoAlbums(albumToAdd);
                db.SaveChanges(true);

                CreateAlbumDirectory(albumToAdd.Name);

                return RedirectToAction("Index", new{@page=page});
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        public virtual ActionResult UpdateAlbumsFromFiles()
        {
            new AjaxService().LoadImagesToDB();
            return RedirectToAction("Index");
        }

        public string GetFileFromPath(string path)
        {
            return path.Substring(path.LastIndexOf("/"));
        }

        public bool CreateAlbumDirectory(string albumName)
        {
            string albumPath = Server.MapPath(ConfigurationManager.AppSettings["PhotoPath"] + "/" + albumName);

            if (!Directory.Exists(albumPath))
            {
                try
                {
                    Directory.CreateDirectory(albumPath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        //
        // GET: /PhotoAlbum/Edit/5
        public string UploadAlbumCover(string photoAlbumName)
        {
            string relativeFileName = ConfigurationManager.AppSettings["DefaultAlbumPhoto"];  
            
            foreach (string file in Request.Files)
            {
                CreateAlbumDirectory(photoAlbumName);

                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                relativeFileName = ConfigurationManager.AppSettings["PhotoPath"] + "/" + Path.GetFileName(photoAlbumName + hpf.FileName.Substring(hpf.FileName.LastIndexOf('.')));
                string savedFileName = Server.MapPath(relativeFileName);


                if (System.IO.File.Exists(savedFileName))
                    System.IO.File.Delete(savedFileName);
                hpf.SaveAs(savedFileName);

            }
            return relativeFileName;

        }
        public virtual ActionResult Edit(String ID)
        {
            try
            {
                int albumID = 0;
                Int32.TryParse(ID, out albumID);

                if (albumID > 0)
                {
                    return View((from dbAlbum in db.PhotoAlbums where dbAlbum.PhotoAlbumID == albumID select dbAlbum).FirstOrDefault<PhotoAlbum>());

                }
                else
                {
                    RedirectToAction("Index");
                    return View();
                }
            }
            catch
            {
                RedirectToAction("Index");
                return View();
            }


        }
        //


        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection form)
        {
            try
            {

                PhotoAlbum albumToUpdate = db.PhotoAlbums.First<PhotoAlbum>(p => p.PhotoAlbumID == id);

                string oldAlbumName = albumToUpdate.Name;

                TryUpdateModel<PhotoAlbum>(albumToUpdate, form.ToValueProvider());
                try
                {
                    if (oldAlbumName != albumToUpdate.Name)
                    {
                        string albumDir = ConfigurationManager.AppSettings["PhotoPath"];
                        if (Directory.Exists(Server.MapPath(albumDir + "/" + oldAlbumName)))
                        {
                            Directory.Move(Server.MapPath(albumDir + "/" + oldAlbumName), Server.MapPath(albumDir + "/" + albumToUpdate.Name));
                        }
                    }
                }
                catch(Exception ex){
                    Log.LogError("There was an issue renaming the Album:  There was an issue moving files from the old folder directory to the new one.  <br>" + ex.Message + "<br>You might want to try doing it manually.");
                }

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0) albumToUpdate.AlbumCoverUrl = UploadAlbumCover(albumToUpdate.Name);

                db.SaveChanges(true);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Log.LogError("There was an issue updating the album:<br>" + ex.Message);
                return RedirectToAction("Index");
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
                if (id > 0)
                {

                    PhotoAlbum albumToDelete = db.PhotoAlbums.First(p => p.PhotoAlbumID == id && p.IsPermanent == false);
                    if (albumToDelete != null)
                    {
                        List<Photo> photosToDelete = (from p in db.Photos where p.PhotoAlbum.PhotoAlbumID == albumToDelete.PhotoAlbumID select p).ToList<Photo>();
                        photosToDelete.ForEach(delegate(Photo p) { db.DeleteObject(p); });
                        db.SaveChanges();
                        //DeleteMoveAlbum(albumToDelete);
                        db.DeleteObject(albumToDelete);
                        db.SaveChanges();

                    }

                }

                return RedirectToAction("Admin", "PhotoAlbums", null);
            }
            catch
            {
                return View();
            }
        }

        public virtual void Enable(int id)
        {
            PhotoAlbum albumToEnable = db.PhotoAlbums.First(p => p.PhotoAlbumID == id);
            albumToEnable.IsActive = true;
            db.SaveChanges();

        }

        public virtual void Disable(int id)
        {
            PhotoAlbum albumToDisable = db.PhotoAlbums.First(p => p.PhotoAlbumID == id);
            albumToDisable.IsActive = false;
            db.SaveChanges();

        }

        private bool DeleteMoveAlbum(PhotoAlbum albumToDelete)
        {
            String oldAlbumPath = Server.MapPath(albumToDelete.AlbumFileLocation);
            string deletedAlbumPath = Server.MapPath(Path.Combine(ConfigurationManager.AppSettings["DeletedAlbumPath"].ToString() , albumToDelete.Name));

            //if the album is already there (and there are files), timestamp the old one and move the new one in.

            if (Directory.Exists(deletedAlbumPath))
            {
                if (Directory.GetFiles(deletedAlbumPath).Count<string>() > 0)
                {
                    //string of timestamped deleted directory
                    string newDeleteAlbumPath = (Path.Combine(deletedAlbumPath, DateTime.Today.ToShortDateString().Replace("/", "_")));

                    //create timestamped directory if it doesn't exist.
                    if (!Directory.Exists(newDeleteAlbumPath))
                    {
                        Directory.CreateDirectory(newDeleteAlbumPath);
                    }

                    //move files that are in deleted folder to new timestamped folder
                    foreach (string fi in Directory.GetFiles(deletedAlbumPath))
                    {
                        if (!System.IO.File.Exists(Path.Combine(newDeleteAlbumPath, fi))) new FileInfo(Path.Combine(deletedAlbumPath, fi)).MoveTo(newDeleteAlbumPath);
                    }

                    //move old files to timestamepd deleted folder.
                    foreach (string fi in Directory.GetFiles(oldAlbumPath))
                    {
                        new FileInfo(Path.Combine(deletedAlbumPath, fi)).MoveTo(newDeleteAlbumPath);
                    }

                    //delete old directory
                    Directory.Delete(oldAlbumPath);
                }
                else
                {
                    foreach (string fi in Directory.GetFiles(oldAlbumPath))
                    {
                        new FileInfo(Path.Combine(deletedAlbumPath, fi)).MoveTo(deletedAlbumPath);
                    }
                    //delete old directory
                    Directory.Delete(oldAlbumPath);
                }
            }
            else
            {
                Directory.Move(oldAlbumPath, deletedAlbumPath);
            }

            return true;
        }

        public virtual void Admin()
        {
            RedirectToAction("Index");
        }


        [HttpGet]
        public virtual ActionResult AlbumArtUpload(int? id)
        {
            return View((from dbAlbum in db.PhotoAlbums where dbAlbum.PhotoAlbumID == id.Value select dbAlbum).FirstOrDefault<PhotoAlbum>());
        }


    }
}
