using BuildingBlocks.EventBusProjects.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.IntegrationEvents.Events
{
    public class ContactsDeletedIntegrationEvent : IntegrationEvent
    {
        public ContactsDeletedIntegrationEvent(int[] contactIds)
        {
            ContactIds = contactIds;
        }
        public int[] ContactIds { get; set; }
    }
}
