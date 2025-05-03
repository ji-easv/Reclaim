using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Reclaim.Domain.Enums;

namespace Reclaim.Domain.Entities.Read;

public class OrderReadEntity
{
    [BsonElement("_id")]
    public required ObjectId Id { get; set; }
    
    public required decimal TotalAmount { get; set; }
    public required OrderStatus Status { get; set; } 
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public required bool IsDeleted { get; set; }
    
    // Navigation properties
    public required ObjectId UserId { get; set; }
    public List<ListingReadEntity> Listings { get; set; }
}