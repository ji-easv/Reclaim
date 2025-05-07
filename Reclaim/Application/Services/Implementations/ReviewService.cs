using Reclaim.Application.Commands.Review;
using Reclaim.Application.Queries.Review;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Review;

namespace Reclaim.Application.Services.Implementations;

public class ReviewService(
    ReviewCommandHandler commandHandler,
    ReviewQueryHandler queryHandler,
    IDomainEventBus domainEventBus
    ) : IReviewService
{
    public async Task<ReviewGetDto> CreateReviewAsync(CreateReviewCommand command)
    {
        var review = await commandHandler.HandleAsync(command);
        await domainEventBus.Publish(new ReviewCreatedEvent
        {
            ReviewWriteEntity = review
        });
        return review.ToDto();

    }

    public async Task<ReviewGetDto> UpdateReviewAsync(UpdateReviewCommand command)
    {
        var review = await commandHandler.HandleAsync(command);
        await domainEventBus.Publish(new ReviewUpdatedEvent
        {
            ReviewWriteEntity = review
        });
        return review.ToDto();
    }

    public async Task<ReviewGetDto> DeleteReviewAsync(DeleteReviewCommand command)
    {
        var review = await commandHandler.HandleAsync(command);
        await domainEventBus.Publish(new ReviewDeletedEvent
        {
            ReviewId = command.ReviewId,
            DeletedAt = review.UpdatedAt!.Value
        });
        return review.ToDto();
    }

    public async Task<List<ReviewGetDto>> GetReviewsWrittenByUserAsync(GetReviewsWrittenByUserId query)
    {
        var reviews = await queryHandler.HandleAsync(query);
        return reviews.ToList();
    }

    public async Task<List<ReviewGetDto>> GetReviewsForSellerAsync(GetReviewsForSellerQuery query)
    {
        var reviews = await queryHandler.HandleAsync(query);
        return reviews.ToList();
    }
}