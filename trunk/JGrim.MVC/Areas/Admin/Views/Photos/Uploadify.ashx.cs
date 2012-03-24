using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace JGrim.MVC.Scripts.uploadify
{
    /// <summary>
    /// Summary description for Uploadify
    /// </summary>
    public class Upload : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get{
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpPostedFile file = context.Request.Files["Filedata"];

                int id = (Int32.Parse(context.Request["id"]));
                string foo = context.Request["foo"];
                file.SaveAs("C:\\" + id.ToString() + foo + file.FileName);

                context.Response.Write("1");
            }
            catch
            {
                context.Response.Write("0");
            }
        }
    }

}