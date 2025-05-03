namespace Reclaim.Infrastructure.EventBus.Order;

public class OrderEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    private async Task HandleOrderDeletedEvent(OrderDeletedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleOrderUpdatedEvent(OrderUpdatedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleOrderCreatedEvent(OrderCreatedEvent arg)
    {
        throw new NotImplementedException();
    }
    
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
}