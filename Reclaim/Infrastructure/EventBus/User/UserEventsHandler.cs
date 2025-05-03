using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Infrastructure.EventBus.User;

public class UserEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Subscribe<UserCreatedEvent>(HandleUserCreatedEvent);
        await domainEventBus.Subscribe<UserUpdatedEvent>(HandleUserUpdatedEvent);
        await domainEventBus.Subscribe<UserDeletedEvent>(HandleUserDeletedEvent);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Unsubscribe<UserCreatedEvent>(HandleUserCreatedEvent);
        await domainEventBus.Unsubscribe<UserUpdatedEvent>(HandleUserUpdatedEvent);
        await domainEventBus.Unsubscribe<UserDeletedEvent>(HandleUserDeletedEvent);
    }

    private async Task HandleUserCreatedEvent(UserCreatedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleUserUpdatedEvent(UserUpdatedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleUserDeletedEvent(UserDeletedEvent arg)
    {
        throw new NotImplementedException();
    }
}