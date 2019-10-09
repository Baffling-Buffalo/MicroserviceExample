using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models
{
    [Table("contact")]
    public partial class Contact
    {
        public Contact()
        {
            ContactLists = new HashSet<ContactList>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Column("phone")]
        [StringLength(50)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Column("active")]
        public bool Active { get; set; }

        [InverseProperty("FContactNavigation")]
        public virtual ICollection<ContactList> ContactLists { get; set; }
    }
}