namespace Reclaim.Infrastructure.EventBus.Review;

public class ReviewDeletedEvent : IDomainEvent
{
    public required string ReviewId { get; set; }
    public required DateTimeOffset DeletedAt { get; set; }
}