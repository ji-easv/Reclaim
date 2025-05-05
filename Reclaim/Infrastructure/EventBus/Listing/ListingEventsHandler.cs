using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.EventBus.Listing;

public class ListingEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
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

    private async Task HandleListingCreatedEvent(ListingCreatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var listingReadRepository = scope.ServiceProvider.GetRequiredService<IListingReadRepository>();
        await listingReadRepository.AddAsync(arg.ListingWriteEntity.ToReadEntity([]));
    }

    private async Task HandleListingUpdatedEvent(ListingUpdatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var listingReadRepository = scope.ServiceProvider.GetRequiredService<IListingReadRepository>();
        await listingReadRepository.UpdateAsync(arg.ListingWriteEntity.ToReadEntity([]));
    }

    private async Task HandleListingDeletedEvent(ListingDeletedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var listingReadRepository = scope.ServiceProvider.GetRequiredService<IListingReadRepository>();
        await listingReadRepository.DeleteAsync(arg.ListingWriteEntity.ToReadEntity([]));
    }
}