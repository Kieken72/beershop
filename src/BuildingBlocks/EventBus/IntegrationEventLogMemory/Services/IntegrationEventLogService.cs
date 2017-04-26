using System.Collections.Generic;
using System.Linq;
using EventBus.Events;

namespace IntegrationEventLogMemory.Services
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private List<IntegrationEventLogEntry> _eventLog;

        public IntegrationEventLogService()
        {
            _eventLog = new List<IntegrationEventLogEntry>();
        }

        public void SaveEvent(IntegrationEvent @event)
        {

            var eventLogEntry = new IntegrationEventLogEntry(@event);
            _eventLog.Add(eventLogEntry);
        }

        public void MarkEventAsPublished(IntegrationEvent @event)
        {
            var eventLogEntry = _eventLog.Single(ie => ie.EventId == @event.Id);
            eventLogEntry.TimesSent++;
            eventLogEntry.State = EventStateEnum.Published;
        }
    }
}