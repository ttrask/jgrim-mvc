using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace JGrim.MVC.Models
{
    public partial class Authentication
    {

        [DllImport("advapi32.dll")]
        protected static extern bool
        LogonUser(String lpszUsername,
                  String lpszDomain,
                  String lpszPassword,

                  int dwLogonType,
                  int dwLogonProvider,
                  out int phToken);

        public static void Authenticate(string userName,
                                        string password)
        {

            int token;



            bool loggedOn = LogonUser(userName,
            null, password,
            2/*2 is if local user is logged on.*/,
            0, out token);



            if (!loggedOn)
            {

                throw new SecurityException("Error Authenticating for User: " + userName);

            }

            else
            {

                IntPtr token2 = new IntPtr(token);

                WindowsIdentity mWI = new WindowsIdentity(token2);

                WindowsImpersonationContext mwic = mWI.Impersonate();



                // Test

                WindowsIdentity mWI1 = WindowsIdentity.GetCurrent();

            }

        }
    }
}