using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Review;

public class GetReviewsForSellerQuery : IQuery<IEnumerable<ReviewGetDto>>
{
    [MaxLength(24)]
    public required string SellerId { get; set; }
}