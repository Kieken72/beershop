using System;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogMemory.Services;

namespace Catalog.API.IntegrationEvents
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly IIntegrationEventLogService _eventLogService;

        public CatalogIntegrationEventService(IEventBus eventBus,
            IIntegrationEventLogService integrationEventLogService)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
             _eventLogService.MarkEventAsPublished(evt);
        }

    }
}
