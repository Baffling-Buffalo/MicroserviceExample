using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public partial class UserGroup
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("UserGroups")]
        public virtual Group Group { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserGroups")]
        public virtual ApplicationUser User { get; set; }
    }
}
