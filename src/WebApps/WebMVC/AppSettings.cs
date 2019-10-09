using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC
{
    public class AppSettings
    {
        //public Connectionstrings ConnectionStrings { get; set; }
        public string ApiGWUrl { get; set; }
        public string IdentityUrl { get; set; }
        public string CallBackUrl { get; set; }
        public string AllowedHosts { get; set; } // not sure
        public string SignalrHubUrl { get; set; }
        public string DefaultCulture { get; set; }
        public string DatepickerCulture { get; set; }
        //public Logging Logging { get; set; }
    }
}
