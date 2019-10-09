using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Models
{
    public partial class AuditLog
    {
        [Column(TypeName = "datetime")]
        public DateTime AuditDate { get; set; }
        [Required]
        [StringLength(50)]
        public string AuditAction { get; set; }
        [Required]
        [StringLength(50)]
        public string TableName { get; set; }
        [Required]
        [Column("TablePK")]
        [StringLength(50)]
        public string TablePk { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EntityType { get; set; }
        [Required]
        [StringLength(256)]
        public string AuditUsername { get; set; }
        [Required]
        public string AuditData { get; set; }
        [Required]
        [StringLength(50)]
        public string CorrelationId { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
    }
}