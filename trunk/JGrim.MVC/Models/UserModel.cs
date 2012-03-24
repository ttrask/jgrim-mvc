using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Transactions;
using System.Configuration;
using System.Globalization;
using Webdiyer.WebControls.Mvc;

namespace JGrim.MVC.Models
{

    public partial class User 
    {
        //public static User Guest
        //{
        //    get
        //    {
        //        return new User() { Username = "Guest"};
        //    }
        //}

        //#region IPrincipal Members

        //public IIdentity Identity
        //{
        //    get 
        //    {
        //        bool isAuthenticated = true ;
        //        return new IIdentity(isAuthenticated, this.Username);
        //    }
        //}

        //public bool IsInRole(string role)
        //{
        //    return this.UserRole.RoleName == role;
        //}

        //#endregion
    }

}