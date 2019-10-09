using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models
{
    [Table("list")]
    public partial class List
    {
        public List()
        {
            ContactLists = new HashSet<ContactList>();
            ChildLists = new HashSet<List>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("list_name")]
        [StringLength(50)]
        public string ListName { get; set; }
        [Column("description")]
        [StringLength(250)]
        public string Description { get; set; }
        [Column("parent_id")]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        [InverseProperty("ChildLists")]
        public virtual List Parent { get; set; }
        [InverseProperty("FListNavigation")]
        public virtual ICollection<ContactList> ContactLists { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<List> ChildLists { get; set; }
    }
}