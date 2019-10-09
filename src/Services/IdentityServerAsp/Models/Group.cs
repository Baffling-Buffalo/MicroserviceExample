using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public partial class Group
    {
        public Group()
        {
            ChildGroups = new HashSet<Group>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        [InverseProperty("ChildGroups")]
        public virtual Group Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Group> ChildGroups { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        internal void IsSubOf()
        {
            throw new NotImplementedException();
        }
    }
}
