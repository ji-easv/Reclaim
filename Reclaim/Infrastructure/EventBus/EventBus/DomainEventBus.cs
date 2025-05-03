using System.Collections.Concurrent;

namespace Reclaim.Infrastructure.EventBus;

public class DomainEventBus : IDomainEventBus
{
    private readonly ConcurrentDictionary<Type, List<Func<IDomainEvent, Task>>> _handlers = new();
    
    public async Task Publish<T>(T domainEvent) where T : IDomainEvent
    {
        if (_handlers.TryGetValue(typeof(T), out var handlers))
        {
            foreach (var handler in handlers)
            {
                await handler(domainEvent);
            }
        }
    }

    public Task Subscribe<T>(Func<T, Task> action) where T : IDomainEvent
    {
        if (!_handlers.ContainsKey(typeof(T)))
        {
            _handlers[typeof(T)] = [];
        }

        _handlers[typeof(T)].Add(async bus => action((T)bus));
        
        return Task.CompletedTask;
    }

    public Task Unsubscribe<T>(Func<T, Task> action) where T : IDomainEvent
    {
        if (_handlers.TryGetValue(typeof(T), out var handlers))
        {
            handlers.RemoveAll(handler => handler.Method.Name == action.Method.Name);
        }

        return Task.CompletedTask;
    }
}