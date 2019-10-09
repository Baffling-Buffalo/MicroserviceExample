using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Role
{
    public class PermissionSubjectViewModel
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string[] PermissionClaimValues { get; set; }
        public string[] PermissionNames { get; set; }
    }
}
