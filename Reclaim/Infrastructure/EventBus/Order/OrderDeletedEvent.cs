namespace Reclaim.Infrastructure.EventBus.Order;

public class OrderDeletedEvent : IDomainEvent
{
    public required string OrderId { get; set; }
    public required DateTimeOffset DeletedAt { get; set; }
}