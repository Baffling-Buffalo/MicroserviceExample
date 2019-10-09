using Identity.API.IntegrationEvents.Events;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Data;
using Identity.API;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.IntegrationEvents.Handlers
{
    public class ContactsDeletedIntegrationEventHandler : IIntegrationEventHandler<ContactsDeletedIntegrationEvent>
    {
        private readonly ILogger<ContactsDeletedIntegrationEventHandler> _logger;
        private ApplicationDbContext dbContext;
        private readonly IEventBus eventBus;

        public ContactsDeletedIntegrationEventHandler(
            ILogger<ContactsDeletedIntegrationEventHandler> logger,
            ApplicationDbContext dbContext,
            IEventBus eventBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext;
            this.eventBus = eventBus;
        }

        public async Task Handle(ContactsDeletedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("CorrelationId", @event.CorrelationId))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                // get ids of affected users
                var usernames = new List<string>();
                // remove contact ids from users
                await dbContext.Users.Where(u => u.ContactId.HasValue && @event.ContactIds.Contains(u.ContactId.Value))
                                     .ForEachAsync(u => { u.ContactId = null; usernames.Add(u.UserName); });

                dbContext.AddAuditCustomField("CorrelationId", @event.CorrelationId);
                dbContext.AddAuditCustomField("AuditUsername", @event.User);
                await dbContext.SaveChangesAsync();

                // send notification to users affected
                eventBus.Publish(new NotificationIntegrationEvent("You can no longer receive or send forms", NotificationIntegrationEvent.NotificationType.Warning)
                    .ToUsers(usernames)
                    .WithCorrelationId(@event.CorrelationId)
                    .ByUser(@event.User));
            }
        }
    }
}
