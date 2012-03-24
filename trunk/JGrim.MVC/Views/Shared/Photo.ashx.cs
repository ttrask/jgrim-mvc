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
using JGrim.MVC.Services;
using System.Web.Services;
using RiaLibrary.Web;
using Webdiyer.WebControls.Mvc;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;

namespace JGrim.MVC.Views
{
    /// <summary>
    /// Summary description for PhotoHandler
    /// </summary>
    public class PhotoHandler : IHttpHandler
    {

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext ctx)
        {
            string src = ctx.Request.QueryString["src"];
            string width = ctx.Request.QueryString["width"];
            string height = ctx.Request.QueryString["height"];
            int iWidth = 0, iHeight = 0;

            if (String.IsNullOrEmpty(width))
            {
                if (Int32.TryParse(width, out iWidth))
                {
                    Int32.TryParse(ConfigurationManager.AppSettings["PhotoWidth"], out iWidth);
                };
            }

            if (String.IsNullOrEmpty(width))
            {
                if (Int32.TryParse(width, out iWidth))
                {
                    Int32.TryParse(ConfigurationManager.AppSettings["PhotoHeight"], out iHeight);

                };
            }

            Image bmp = PhotoService.ResizeImage(src);

            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                ctx.Response.ContentType = "image/bmp";
                ctx.Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}