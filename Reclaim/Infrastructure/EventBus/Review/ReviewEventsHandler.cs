namespace Reclaim.Infrastructure.EventBus.Review;

public class ReviewEventsHandler(IDomainEventBus domainEventBus, IServiceProvider serviceProvider) : IEventHandler
{
    private async Task HandleReviewCreatedEvent(ReviewCreatedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleReviewUpdatedEvent(ReviewUpdatedEvent arg)
    {
        throw new NotImplementedException();
    }

    private async Task HandleReviewDeletedEvent(ReviewDeletedEvent arg)
    {
        throw new NotImplementedException();
    }
    
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
}