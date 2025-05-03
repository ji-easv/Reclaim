namespace Reclaim.Infrastructure.EventBus.EventBus;

public interface IDomainEventBus
{
    Task Publish<T>(T domainEvent) where T : IDomainEvent;
    Task Subscribe<T>(Func<T, Task> action) where T : IDomainEvent;
    Task Unsubscribe<T>(Func<T, Task> action) where T : IDomainEvent;
}