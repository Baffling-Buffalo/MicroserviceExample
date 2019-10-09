using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuildingBlocks.EventBusProjects.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(string correlationId)
        {
            CorrelationId = correlationId;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public string CorrelationId { get; set; }
        public string User { get; set; }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }



        public IntegrationEvent WithCorrelationId(string correlationId)
        {
            this.CorrelationId = correlationId;
            return this;
        }

        public IntegrationEvent ByUser(string username)
        {
            User = username;
            return this;
        }
    }
}
