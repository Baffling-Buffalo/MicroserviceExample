using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.UserGroup
{
    public class UserGroupDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentGroup { get; set; }
        public string ChildGroups { get; set; }
    }
}
