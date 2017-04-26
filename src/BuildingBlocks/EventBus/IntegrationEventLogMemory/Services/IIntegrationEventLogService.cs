using EventBus.Events;

namespace IntegrationEventLogMemory.Services
{
    public interface IIntegrationEventLogService
    {
        void SaveEvent(IntegrationEvent @event);
        void MarkEventAsPublished(IntegrationEvent @event);
    }
}