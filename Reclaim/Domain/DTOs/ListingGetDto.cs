namespace Reclaim.Domain.DTOs;

public class ListingGetDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required decimal Price { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public required UserGetDto User { get; set; }
    public required List<MediaGetDto> Media { get; set; } 
}