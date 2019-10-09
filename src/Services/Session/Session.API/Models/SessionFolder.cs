using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.API.Models
{
    [Table("session_folder")]
    public partial class SessionFolder
    {
        public SessionFolder()
        {
            SessionDocuments = new HashSet<SessionDocument>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("folder_name")]
        [StringLength(50)]
        public string FolderName { get; set; }
        [Column("f_session_main")]
        public int FSessionMain { get; set; }

        [ForeignKey("FSessionMain")]
        [InverseProperty("SessionFolders")]
        public virtual SessionMain FSessionMainNavigation { get; set; }
        [InverseProperty("FSessionFolderNavigation")]
        public virtual ICollection<SessionDocument> SessionDocuments { get; set; }
    }
}