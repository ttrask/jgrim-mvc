using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Configuration;
using System.Globalization;
using Webdiyer.WebControls.Mvc;

namespace JGrim.MVC.Models
{

    public partial class Photo
    {
    }


    public partial class PhotoAlbum
    {

        public ViewDataUploadFilesResult AttachedArtwork
        {
            get;
            set;
        }

        public PagedList<Photo> GetPagedPhotos(string page)
        {
            JGrimEntities1 db = new JGrimEntities1();

            int pageNumber=1;
            
            if (!String.IsNullOrEmpty(page))
            {
                if (!int.TryParse(page, out pageNumber))
                {
                    pageNumber = 1;
                }
            }

            return this.Photos.AsQueryable().ToPagedList<Photo>(pageNumber , 20);
        }

        public PhotoAlbum GetByName(string AlbumName)
        {
            return new PhotoAlbum();
        }
    }
}
