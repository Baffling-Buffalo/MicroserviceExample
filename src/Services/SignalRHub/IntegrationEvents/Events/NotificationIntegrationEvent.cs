using BuildingBlocks.EventBusProjects.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.IntegrationEvents.Events
{
    public class NotificationIntegrationEvent : IntegrationEvent
    {
        public NotificationIntegrationEvent(string message, NotificationType type)
        {
            Message = message;
            Type = type.ToString();
        }

        public string Message { get; set; }
        public string Type { get; set; }
        public List<string> Usernames { get; set; }

        public enum NotificationType
        {
            Success,
            Info,
            Error,
            Warning
        }

        public NotificationIntegrationEvent ToUsers(List<string> usernames)
        {
            this.Usernames = usernames;
            return this;
        }

        public NotificationIntegrationEvent ToUser(string username)
        {
            this.Usernames = new List<string>() { username };
            return this;
        }
    }
}
