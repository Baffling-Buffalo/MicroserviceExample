using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public class AppSettings
    {
        public string AppName { get; set; }
        public string ApiGWUrl { get; set; }
        public string AppUrl { get; set; }
        public string SignalrHubUrl { get; set; }
        public string DefaultCulture { get; set; }
        public string DatepickerCulture { get; set; }
        public string ActiveDirectoryDomain { get; set; }
        public string ContactAppUrl { get; set; }
        public string AdminAppUrl { get; set; }
        public Dictionary<string,string> ConnectionStrings { get; set; }
    }
}
