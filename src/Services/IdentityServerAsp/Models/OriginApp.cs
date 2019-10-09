using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class OriginApp
    {
        public enum OriginAppEnum
        {
            AdminApp,
            ContactApp
        }

        public static string GetUserPrefix(string appName)
        {
            if (appName == OriginAppEnum.AdminApp.ToString() || appName == "admin")
                return "admin_";

            if (appName == OriginAppEnum.ContactApp.ToString() || appName == "contact")
                return "contact_";

            return "";
        }
    }
   
}
