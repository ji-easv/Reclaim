using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Listing;

public class ListingUpdatedEvent : IDomainEvent
{
    public required ListingWriteEntity ListingWriteEntity { get; init; }
}