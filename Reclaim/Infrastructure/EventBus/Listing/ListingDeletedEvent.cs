using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Listing;

public class ListingDeletedEvent : IDomainEvent
{
    public required ListingWriteEntity ListingWriteEntity { get; init; }
}