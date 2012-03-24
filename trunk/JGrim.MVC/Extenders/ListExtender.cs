using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace JGrim.MVC.Extenders
{
    public class FileList
    {
        public static T[] Sort<T>(T[] oldFiles) 
        {
            switch(typeof(T).ToString()){
                case "FileInfo":
                    //Array.Sort(((FileInfo[])oldFiles), SortItem);
                    break;
        }
            return oldFiles;
        }

        private static int SortItem<T>(T t1, T t2) where T: IComparable
        {
            return t1.CompareTo(t2);
        }
    }
    
}