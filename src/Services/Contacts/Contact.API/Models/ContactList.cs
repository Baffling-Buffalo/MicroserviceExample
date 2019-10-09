using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models
{
    [Table("contact_list")]
    public partial class ContactList
    {
        [Column("f_contact")]
        public int FContact { get; set; }
        [Column("f_list")]
        public int FList { get; set; }

        [ForeignKey("FContact")]
        [InverseProperty("ContactLists")]
        public virtual Contact FContactNavigation { get; set; }
        [ForeignKey("FList")]
        [InverseProperty("ContactLists")]
        public virtual List FListNavigation { get; set; }
    }
}