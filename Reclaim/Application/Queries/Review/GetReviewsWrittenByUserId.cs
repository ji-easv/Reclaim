using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Review;

public class GetReviewsWrittenByUserId : IQuery<IEnumerable<ReviewGetDto>>
{
    [MaxLength(24)]
    public required string UserId { get; set; }
}