using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Review;

public class CreateReviewCommand : ICommand<ReviewWriteEntity>
{
    public required string Content { get; set; }
    public required int Rating { get; set; }
    public required string AuthorId { get; set; }
    public required string SellerId { get; set; }
}