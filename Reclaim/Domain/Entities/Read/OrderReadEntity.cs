using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Reclaim.Domain.Enums;

namespace Reclaim.Domain.Entities.Read;

public class OrderReadEntity
{
    [BsonId]
    public required ObjectId Id { get; set; }
    
    [BsonRequired]
    public required decimal TotalAmount { get; set; }
    
    [BsonRequired]
    public required OrderStatus Status { get; set; } 
    
    [BsonRequired]
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    [BsonRequired]
    public required bool IsDeleted { get; set; }
    
    // Navigation properties
    [BsonRequired]
    public required ObjectId UserId { get; set; }
    public List<ListingReadEntity> Listings { get; set; } = [];
}