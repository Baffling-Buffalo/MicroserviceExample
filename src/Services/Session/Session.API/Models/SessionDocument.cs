using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.API.Models
{
    [Table("session_document")]
    public partial class SessionDocument
    {
        public SessionDocument()
        {
            SessionContacts = new HashSet<SessionContact>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("f_oz_item")]
        public int FOzItem { get; set; }
        [Column("f_session_folder")]
        public int FSessionFolder { get; set; }
        [Column("oz_item_content")]
        [StringLength(250)]
        public string OzItemContent { get; set; }
        [Required]
        [Column("doc_fs_all")]
        [StringLength(250)]
        public string DocFsAll { get; set; }

        [ForeignKey("FSessionFolder")]
        [InverseProperty("SessionDocuments")]
        public virtual SessionFolder FSessionFolderNavigation { get; set; }
        [InverseProperty("FSessionDocumentNavigation")]
        public virtual ICollection<SessionContact> SessionContacts { get; set; }
    }
}