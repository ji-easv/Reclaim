using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.EventBus.Review;

public class ReviewEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Subscribe<ReviewCreatedEvent>(HandleReviewCreatedEvent);
        await domainEventBus.Subscribe<ReviewUpdatedEvent>(HandleReviewUpdatedEvent);
        await domainEventBus.Subscribe<ReviewDeletedEvent>(HandleReviewDeletedEvent);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await domainEventBus.Unsubscribe<ReviewCreatedEvent>(HandleReviewCreatedEvent);
        await domainEventBus.Unsubscribe<ReviewUpdatedEvent>(HandleReviewUpdatedEvent);
        await domainEventBus.Unsubscribe<ReviewDeletedEvent>(HandleReviewDeletedEvent);
    }

    private async Task HandleReviewCreatedEvent(ReviewCreatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IReviewReadRepository>();
        var readEntity = arg.ReviewWriteEntity.ToReadEntity();
        await readRepository.AddAsync(readEntity);
    }

    private async Task HandleReviewUpdatedEvent(ReviewUpdatedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IReviewReadRepository>();
        var readEntity = arg.ReviewWriteEntity.ToReadEntity();
        await readRepository.UpdateAsync(readEntity);
    }

    private async Task HandleReviewDeletedEvent(ReviewDeletedEvent arg)
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider.GetService<IReviewReadRepository>();
        var existingEntity = await readRepository.GetByIdAsync(arg.ReviewId);
        existingEntity.IsDeleted = true;
        existingEntity.UpdatedAt = arg.DeletedAt;
        await readRepository.UpdateAsync(existingEntity);
    }
}