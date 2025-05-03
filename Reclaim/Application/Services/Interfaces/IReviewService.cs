using Reclaim.Application.Commands.Review;
using Reclaim.Application.Queries.Review;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewGetDto> CreateReviewAsync(CreateReviewCommand command);
    Task<ReviewGetDto> UpdateReviewAsync(UpdateReviewCommand command);
    Task<ReviewGetDto> DeleteReviewAsync(DeleteReviewCommand command);
    Task<List<ReviewGetDto>> GetReviewsWrittenByUserAsync(GetReviewsWrittenByUserId query);
    Task<List<ReviewGetDto>> GetReviewsForSellerAsync(GetReviewsForSellerQuery query);
}