using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Review;

public class UpdateReviewCommand : ICommand<ReviewWriteEntity>
{
    [MaxLength(24)]
    public required string ReviewId { get; set; }
    public required string Content { get; set; }
    public required int Rating { get; set; }
    public required string AuthorId { get; set; }
    public required string SellerId { get; set; }
}