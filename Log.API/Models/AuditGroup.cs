using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.API.Models
{
    /// <summary>
    /// Group of audits related by correlationId
    /// </summary>
    public class AuditGroup
    {
        public string CorrelationId { get; set; }
        public List<AuditLog> Logs { get; set; }
        public string Action { get; set; }
        public DateTime CreationDate { get; set; }
        public string EntityType { get; set; }
        public string Username { get; set; }
        public string ActionType { get; set; }
    }
}