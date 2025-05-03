using Reclaim.Application.Commands.Review;
using Reclaim.Application.Queries.Review;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Services.Implementations;

public class ReviewService(
    ReviewCommandHandler commandHandler,
    ReviewQueryHandler queryHandler,
    IDomainEventBus domainEventBus
    ) : IReviewService
{
    public Task<ReviewGetDto> CreateReviewAsync(CreateReviewCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewGetDto> UpdateReviewAsync(UpdateReviewCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewGetDto> DeleteReviewAsync(DeleteReviewCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewGetDto>> GetReviewsWrittenByUserAsync(GetReviewsWrittenByUserId query)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewGetDto>> GetReviewsForSellerAsync(GetReviewsForSellerQuery query)
    {
        throw new NotImplementedException();
    }
}