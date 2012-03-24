using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using JGrim.MVC.Models;
using System.Configuration;

namespace JGrim.MVC.Areas.Admin.Controllers
{
    public partial class LoginController : Controller
    {
        //
        // GET: /Login/

        private JGrimEntities1 db;



        public LoginController()
        {
            db = new JGrimEntities1();
        }

        public virtual ActionResult Index()
        {
            User userToUpdate = db.Users.First<User>();

            userToUpdate.DbPassword = EncryptPassword("DoubleRainbow");

            db.SaveChanges(true);

            RedirectToAction("Login", "Login");
            return View();
        }

        [HttpGet]
        public virtual ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public virtual void Login(string username, string password)
        {

            try
            {
                User user = (from u in db.Users where (u.Username == username) select u).FirstOrDefault<User>();

                if (user != null && DecryptPassword(user.DbPassword) == password)
                {
                    SetAuthenticationCookie(username);

                    this.AuthenticateFileIOUser();

                    if (!String.IsNullOrEmpty(FormsAuthentication.GetRedirectUrl(username, false)))
                    {
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(username, false));
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(username, true);
                    }

                }
                else
                {
                    Response.Redirect("~/Accessdenied.aspx");
                }
            }
            finally
            {
                Log.LogError("User " + username + " logged in sucessfully.");
            }
            
        }

        private void AuthenticateFileIOUser()
        {
            string fileIOUser = ConfigurationManager.AppSettings["FileIOUser"];
            if (!String.IsNullOrEmpty(fileIOUser))
            {
                Dictionary<String, String> userAccount = new Dictionary<string, string>();
                List<string> userInfo = fileIOUser.Split(';').ToList<String>();
                userInfo.ForEach(delegate(string info)
                {
                    userAccount.Add(info.Split('=')[0], info.Split('=')[1]);
                });
                try
                {
                    JGrim.MVC.Models.Authentication.Authenticate(userAccount["username"], userAccount["password"]);
                }
                catch
                {
                    Log.LogError("Unable to authenticate user for file I/O:" + userAccount["username"]);
                }
            }
            else {
                Log.LogError("Configuration Entry [FileIOUser] is missing.");
            }

        }

        public virtual void Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            Response.Redirect("~/Admin/");
        }


        public virtual void SetAuthenticationCookie(string username)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket( 
                1, username,         System.DateTime.Now,        System.DateTime.Now.AddMinutes(30),   true,  ""   ,        FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            FormsAuthentication.SetAuthCookie(username, false);
        }

        private byte[] EncryptPassword(string decryptedPassword)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("DoubleRainbow"));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(decryptedPassword);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Results;

        }


        private string DecryptPassword(byte[] encryptedPassword)
        {

            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("DoubleRainbow"));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = encryptedPassword;

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

    }
}
