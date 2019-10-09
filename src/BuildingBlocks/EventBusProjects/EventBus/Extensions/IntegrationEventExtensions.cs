using BuildingBlocks.EventBusProjects.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.EventBusProjects.EventBus.Extensions
{
    public static class IntegrationEventExtensions
    {
        public static IntegrationEvent WithCorrelationId(this IntegrationEvent integrationEvent, string correlationId)
        {
            integrationEvent.CorrelationId = correlationId;
            return integrationEvent;
        }
    }
}
