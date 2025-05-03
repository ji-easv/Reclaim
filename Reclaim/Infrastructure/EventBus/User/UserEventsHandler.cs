using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

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
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IUserReadRepository>();
        var readEntity = arg.UserWriteEntity.ToReadEntity();
        await readRepository.AddAsync(readEntity);
    }

    private async Task HandleUserUpdatedEvent(UserUpdatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IUserReadRepository>();
        var readEntity = arg.UserWriteEntity.ToReadEntity();
        await readRepository.UpdateAsync(readEntity);
    }

    private async Task HandleUserDeletedEvent(UserDeletedEvent arg)
    {       
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IUserReadRepository>();
        var existingEntity = await readRepository.GetByIdAsync(arg.UserId);
        existingEntity!.IsDeleted = true;
        await readRepository.UpdateAsync(existingEntity);
    }
}