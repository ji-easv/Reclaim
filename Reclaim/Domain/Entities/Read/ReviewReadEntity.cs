using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class ReviewReadEntity
{
    [BsonId]
    public required ObjectId Id { get; set; }
    
    [BsonRequired]
    public required string Content { get; set; }
    
    [BsonRequired]
    public required int Rating { get; set; }
    
    [BsonRequired]
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } 
    
    [BsonRequired]
    public required UserReadEntity Author { get; set; }
    
    [BsonRequired]
    public required ObjectId SellerId { get; set; }
}