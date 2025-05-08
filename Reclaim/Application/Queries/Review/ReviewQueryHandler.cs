using Reclaim.Domain.DTOs;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Application.Queries.Review;

public class ReviewQueryHandler(IReviewReadRepository reviewReadRepository) : IQueryHandler<GetReviewsForSellerQuery, IEnumerable<ReviewGetDto>>,
    IQueryHandler<GetReviewsWrittenByUserId, IEnumerable<ReviewGetDto>>
{
    public async Task<IEnumerable<ReviewGetDto>> HandleAsync(GetReviewsWrittenByUserId query)
    {
        var reviews = await reviewReadRepository.GetByUserIdAsync(query.UserId);

        return reviews.Select(r => r.ToDto());
    }

    public async Task<IEnumerable<ReviewGetDto>> HandleAsync(GetReviewsForSellerQuery query)
    {
        var reviews = await reviewReadRepository.GetBySellerIdAsync(query.SellerId);

        return reviews.Select(r => r.ToDto());
    }
}