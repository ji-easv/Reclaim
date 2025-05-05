using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Listing;

public class ListingCreatedEvent : IDomainEvent
{
    public required ListingWriteEntity ListingWriteEntity { get; set; }
}