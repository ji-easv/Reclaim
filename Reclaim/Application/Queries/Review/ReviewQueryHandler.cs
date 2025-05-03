using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Review;

public class ReviewQueryHandler : IQueryHandler<GetReviewsForSellerQuery, IEnumerable<ReviewGetDto>>,
    IQueryHandler<GetReviewsWrittenByUserId, IEnumerable<ReviewGetDto>>
{
    public Task<IEnumerable<ReviewGetDto>> HandleAsync(GetReviewsWrittenByUserId query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewGetDto>> HandleAsync(GetReviewsForSellerQuery query)
    {
        throw new NotImplementedException();
    }
}