namespace Reclaim.Domain.DTOs;

public class ReviewGetDto
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public required int Rating { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } 
    public required bool IsDeleted { get; set; }
    public required UserGetDto Author { get; set; }
    public UserGetDto? Seller { get; set; }
}