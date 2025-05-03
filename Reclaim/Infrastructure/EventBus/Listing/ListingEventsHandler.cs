using Reclaim.Infrastructure.EventBus.Events.Listing;

namespace Reclaim.Infrastructure.EventBus.Listing;

public class ListingEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    private async Task HandleListingCreatedEvent(ListingCreatedEvent arg)
    {
        throw new NotImplementedException();
    }
    
    private Task HandleListingUpdatedEvent(ListingUpdatedEvent arg)
    {
        throw new NotImplementedException();
    }
    
    private Task HandleListingDeletedEvent(ListingDeletedEvent arg)
    {
        throw new NotImplementedException();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Subscribe<ListingCreatedEvent>(HandleListingCreatedEvent);
        await domainEventBus.Subscribe<ListingUpdatedEvent>(HandleListingUpdatedEvent);
        await domainEventBus.Subscribe<ListingDeletedEvent>(HandleListingDeletedEvent);
    }
    
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Unsubscribe<ListingCreatedEvent>(HandleListingCreatedEvent);
        await domainEventBus.Unsubscribe<ListingUpdatedEvent>(HandleListingUpdatedEvent);
        await domainEventBus.Unsubscribe<ListingDeletedEvent>(HandleListingDeletedEvent);
    }

}