using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace JGrim.MVC.Models
{
    public partial class Log
    {
        


        public static bool LogError(string errorMessage)
        {
            JGrimEntities1 db = new JGrimEntities1();
            
            try
            {
                db.AddToLogs(new Log() { ErrorMessage = errorMessage, LogDate = DateTime.Now });

                db.SaveChanges(true);

                return true;
            }
            catch
            {
                return false;
            }
            finally{
                db.Dispose();
            }
        }

        public static PagedList<Log> GetPagedLog(string page)
        {
            int pageNumber = 1;

            if (!String.IsNullOrEmpty(page))
            {
                if (!int.TryParse(page, out pageNumber))
                {
                    pageNumber = 1;
                }
            }

            JGrimEntities1 db = new JGrimEntities1();
            return  (from log in db.Logs orderby log.LogDate descending select log).AsQueryable().ToPagedList<Log>(pageNumber, 20);
        }
    }
}