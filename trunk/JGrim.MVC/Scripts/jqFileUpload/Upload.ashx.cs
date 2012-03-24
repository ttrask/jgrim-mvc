using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using JGrim.MVC.Models;

namespace JGrim.MVC.Scripts.jqFileUpload.example
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        JGrimEntities1 db;

        public Upload()
        {
            db = new JGrimEntities1();
        }

        public void ProcessRequest(HttpContext context)
        {
            List<JsonImage> photos = new List<JsonImage>();

            if (context.Request.Files.Count > 0)
            {
                // get the applications path
                string albumName = context.Request.Params["albumName"];
                string albumPath = ConfigurationManager.AppSettings["PhotoPath"] + "/" + albumName;

                string uploadPath = context.Server.MapPath(albumPath);
                // loop through all the uploaded files
                for (int j = 0; j < context.Request.Files.Count; j++)
                {

                    // get the current file
                    HttpPostedFile uploadFile = context.Request.Files[j];
                    // if there was a file uploded
                    if (uploadFile.ContentLength > 0)
                    {
                        //uploads file to path
                        uploadFile.SaveAs(Path.Combine(uploadPath, uploadFile.FileName));

                        //saves file information to database
                        Photo p = new Photo()
                        {
                            Name = uploadFile.FileName,
                            FileName = uploadFile.FileName,
                            IsActive = true,
                            Description = "Picture",
                            PhotoAlbum = (from pa in db.PhotoAlbums where pa.Name == albumName select pa).First<PhotoAlbum>(),
                            ContentType = uploadFile.ContentType
                        };

                        db.AddToPhotos(p);
                        db.SaveChanges();
                             

                        //adds image information to JSON response
                        photos.Add(new JsonImage() { 
                            name = p.FileName,
                            size = uploadFile.ContentLength, 
                            url="../../photos/ScaledImage/"+p.PhotoID,
                            thumbnail_url =  "../../photos/ScaledImage/"+p.PhotoID+"?width=120&height=120"
                        });
                    }
                }
            }

            
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            StringBuilder sbJsonResults = new StringBuilder();

            serializer.Serialize(photos, sbJsonResults);



            context.Response.Clear();

            context.Response.ContentType = "application/json; charset=utf-8";

            context.Response.Write(sbJsonResults.ToString());

        }


        public bool UploadFile(HttpContext context)
        {

            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    
    [Serializable]
    public class JsonImage
    {
        public string name;
        public int size;
        public string url;
        public string thumbnail_url;
        public string delete_url;
        public string delete_type{
            get{
                return "DELETE";
            }
        }
    }
}


