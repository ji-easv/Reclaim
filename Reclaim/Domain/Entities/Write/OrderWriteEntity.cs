using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Reclaim.Domain.Enums;

namespace Reclaim.Domain.Entities.Write;

public class OrderWriteEntity
{
    [MaxLength(24)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public required bool IsDeleted { get; set; }
    
    // Navigation properties
    [MaxLength(24)]
    public required string UserId { get; set; }
    
    public required List<ListingWriteEntity> Listings { get; set; } = [];
} 