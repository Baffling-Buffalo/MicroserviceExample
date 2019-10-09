using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibraries;
using Log.API.Models;
using Newtonsoft.Json;

namespace Log.API.IntegrationEvents.Handlers
{
    public class AuditLogDeletedIntegrationEventHandler : IIntegrationEventHandler<AuditLogIntegrationEvent>
    {
        private readonly ILogger<AuditLogDeletedIntegrationEventHandler> _logger;
        private AuditLogDbContext dbContext;
        private readonly IEventBus eventBus;

        public AuditLogDeletedIntegrationEventHandler(
            ILogger<AuditLogDeletedIntegrationEventHandler> logger,
            AuditLogDbContext dbContext,
            IEventBus eventBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext;
            this.eventBus = eventBus;
        }

        public async Task Handle(AuditLogIntegrationEvent @event)
        {
            using (LogContext.PushProperty("CorrelationId", @event.CorrelationId))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                List<Models.AuditLog> logs = new List<Models.AuditLog>();
                try
                {
                    // Mapping from SharedLibraries.AuditLog (which all apps use to send logs) to Log.API.Models.AuditLog which should have same properties
                    var json = JsonConvert.SerializeObject(@event.AuditLogs);
                    logs = JsonConvert.DeserializeObject<List<Models.AuditLog>>(json);

                    await dbContext.AuditLogs.AddRangeAsync(logs);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
    }
}
