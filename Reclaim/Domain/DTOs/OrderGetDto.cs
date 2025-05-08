using Reclaim.Domain.Enums;

namespace Reclaim.Domain.DTOs;

public class OrderGetDto
{
    public required string Id { get; set; } 
    public required string UserId { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required DateTimeOffset? UpdatedAt { get; set; }
    public required OrderStatus Status { get; set; }
    public required decimal TotalPrice { get; set; }
    
    public required bool IsDeleted { get; set; }
    public List<ListingGetDto> Listings { get; set; } = [];
}