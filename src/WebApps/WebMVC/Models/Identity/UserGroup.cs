using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
        public UserGroup ParentGroup { get; set; }
        public int ChildGroupCount { get; set; }
        public List<UserGroup> ChildGroups{ get; set; }
    }
}
