using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.API.Models
{
    [Table("session_contact")]
    public partial class SessionContact
    {
        public SessionContact()
        {
            SessionDocumentRoles = new HashSet<SessionDocumentRole>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("fill_order")]
        public int FillOrder { get; set; }
        [Column("doc_completed")]
        public bool DocCompleted { get; set; }
        [Column("f_contact")]
        public int FContact { get; set; }
        [Column("f_session_document")]
        public int FSessionDocument { get; set; }
        [Required]
        [Column("doc_fs_single")]
        [StringLength(250)]
        public string DocFsSingle { get; set; }

        [ForeignKey("FSessionDocument")]
        [InverseProperty("SessionContacts")]
        public virtual SessionDocument FSessionDocumentNavigation { get; set; }
        [InverseProperty("FSessionContactNavigation")]
        public virtual ICollection<SessionDocumentRole> SessionDocumentRoles { get; set; }
    }
}