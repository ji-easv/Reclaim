using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class ListingReadEntity
{
    [BsonId]
    public required ObjectId Id { get; set; }
   
    [BsonRequired]
    public required string Title { get; set; }
    public string? Content { get; set; }
   
    [BsonRequired]
    public required decimal Price { get; set; }
    
    [BsonRequired]
    public required bool IsDeleted { get; set; }
    
    [BsonRequired]
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    [BsonRequired]
    public required UserReadEntity User { get; set; }
    
    [BsonRequired]
    public required List<MediaReadEntity> Media { get; set; } = [];
}