using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRHub.Hubs;
using Serilog.Context;
using SignalRHub.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.IntegrationEvents.Handlers
{
    public class NotificationIntegrationEventHandler : IIntegrationEventHandler<NotificationIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<NotificationIntegrationEventHandler> _logger;

        public NotificationIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<NotificationIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(NotificationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("CorrelationId", @event.CorrelationId))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                foreach (var user in @event.Usernames)
                {
                    await _hubContext.Clients.User(user).SendAsync("Notification", new { Message = @event.Message, Type = @event.Type });
                }
            }
        }
    }
}
