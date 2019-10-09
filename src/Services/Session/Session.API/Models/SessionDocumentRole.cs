using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.API.Models
{
    [Table("session_document_role")]
    public partial class SessionDocumentRole
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("field_id")]
        public int FieldId { get; set; }
        [Required]
        [Column("field_type")]
        [StringLength(50)]
        public string FieldType { get; set; }
        [Column("field_description")]
        [StringLength(50)]
        public string FieldDescription { get; set; }
        [Column("f_session_contact")]
        public int FSessionContact { get; set; }

        [ForeignKey("FSessionContact")]
        [InverseProperty("SessionDocumentRoles")]
        public virtual SessionContact FSessionContactNavigation { get; set; }
    }
}