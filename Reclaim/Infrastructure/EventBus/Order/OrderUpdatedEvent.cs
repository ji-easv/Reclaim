using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Order;

public class OrderUpdatedEvent : IDomainEvent
{
    public required OrderWriteEntity OrderWriteEntity { get; set; }
}