using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace UcbWeb.Helpers
{
    public class UserManager1
    {
        // Gets current windows users (assumes Windows authentication)
        public static string GetCurrentUser()
        {
            // Get raw username from Windows Identity
            string CurrentUser = WindowsIdentity.GetCurrent().Name;

            return CurrentUser;
        }
    }
}