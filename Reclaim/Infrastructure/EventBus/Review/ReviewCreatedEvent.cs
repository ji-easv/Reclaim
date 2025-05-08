using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Review;

public class ReviewCreatedEvent : IDomainEvent
{
    public required ReviewWriteEntity ReviewWriteEntity { get; set; }
}