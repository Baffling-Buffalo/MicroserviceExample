using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("group")]
    public partial class Group
    {
        public Group()
        {
            ChildGroups = new HashSet<Group>();
            OzItemGroups = new HashSet<OzItemGroup>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("group_name")]
        [StringLength(50)]
        public string GroupName { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("parentId")]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        [InverseProperty("ChildGroups")]
        public virtual Group Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Group> ChildGroups { get; set; }
        [InverseProperty("FGroup")]
        public virtual ICollection<OzItemGroup> OzItemGroups { get; set; }
    }
}