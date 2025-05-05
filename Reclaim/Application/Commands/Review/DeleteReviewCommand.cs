using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Review;

public class DeleteReviewCommand : ICommand<ReviewWriteEntity>
{
    [MaxLength(24)]
    public required string ReviewId { get; set; }
}