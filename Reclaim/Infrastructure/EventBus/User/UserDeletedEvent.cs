namespace Reclaim.Infrastructure.EventBus.User;

public class UserDeletedEvent : IDomainEvent
{
    public required string UserId { get; set; }
}