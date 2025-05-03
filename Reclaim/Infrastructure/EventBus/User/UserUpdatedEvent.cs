using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.User;

public class UserUpdatedEvent : IDomainEvent
{
    public required UserWriteEntity UserWriteEntity { get; set; }
}