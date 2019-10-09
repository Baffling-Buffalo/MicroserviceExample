using System;
using System.Collections.Generic;

namespace SharedLibraries
{
    public partial class AuditLog
    {
        public DateTime? AuditDate { get; set; }
        public string AuditAction { get; set; }
        public string TableName { get; set; }
        public string TablePk { get; set; }
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string AuditUsername { get; set; }
        public string AuditData { get; set; }
        public string CorrelationId { get; set; }
        public string Description { get; set; }
        public string ApplicationName { get; set; }
    }
}