using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Review;

public class ReviewUpdatedEvent : IDomainEvent
{
    public required ReviewWriteEntity ReviewWriteEntity { get; set; }
}