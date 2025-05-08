using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.EventBus.Media;

public class MediaEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    private Task HandleMediaCreatedEvent(MediaCreatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var mediaReadRepository = scope.ServiceProvider.GetRequiredService<IMediaReadRepository>();
        return mediaReadRepository.AddRangeAsync(arg.MediaWriteEntityList.Select(x => x.ToReadEntity()).ToList());
    }

    private Task HandleMediaDeletedEvent(MediaDeletedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var mediaReadRepository = scope.ServiceProvider.GetRequiredService<IMediaReadRepository>();

        if (arg.MediaWriteEntityList.Count == 0)
        {
            return Task.CompletedTask;
        }
        
        var listingId = arg.MediaWriteEntityList.First().ListingId;
        return mediaReadRepository.DeleteRangeAsync(listingId,arg.MediaWriteEntityList.Select(x => x.Id).ToList());
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Subscribe<MediaCreatedEvent>(HandleMediaCreatedEvent);
        await domainEventBus.Subscribe<MediaDeletedEvent>(HandleMediaDeletedEvent);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Unsubscribe<MediaCreatedEvent>(HandleMediaCreatedEvent);
        await domainEventBus.Unsubscribe<MediaDeletedEvent>(HandleMediaDeletedEvent);
    }
}