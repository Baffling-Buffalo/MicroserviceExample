using BuildingBlocks.EventBusProjects.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibraries
{
    public class AuditLogIntegrationEvent : IntegrationEvent
    {
        public List<AuditLog> AuditLogs { get; set; }
    }
}
