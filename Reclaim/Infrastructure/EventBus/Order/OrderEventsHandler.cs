using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.EventBus.Order;

public class OrderEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Subscribe<OrderCreatedEvent>(HandleOrderCreatedEvent);
        await domainEventBus.Subscribe<OrderUpdatedEvent>(HandleOrderUpdatedEvent);
        await domainEventBus.Subscribe<OrderDeletedEvent>(HandleOrderDeletedEvent);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Unsubscribe<OrderCreatedEvent>(HandleOrderCreatedEvent);
        await domainEventBus.Unsubscribe<OrderUpdatedEvent>(HandleOrderUpdatedEvent);
        await domainEventBus.Unsubscribe<OrderDeletedEvent>(HandleOrderDeletedEvent);
    }
    
    private async Task HandleOrderCreatedEvent(OrderCreatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IOrderReadRepository>();
        var readEntity = arg.OrderWriteEntity.ToReadEntity();
        await readRepository.AddAsync(readEntity);
    }
    
    private async Task HandleOrderUpdatedEvent(OrderUpdatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IOrderReadRepository>();
        var readEntity = arg.OrderWriteEntity.ToReadEntity();
        await readRepository.UpdateAsync(readEntity);
    }
    
    private async Task HandleOrderDeletedEvent(OrderDeletedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IOrderReadRepository>();
        var existingEntity = await readRepository.GetByIdAsync(arg.OrderId);
        existingEntity!.IsDeleted = true;
        existingEntity.UpdatedAt = arg.DeletedAt;
        await readRepository.UpdateAsync(existingEntity);
    }
}