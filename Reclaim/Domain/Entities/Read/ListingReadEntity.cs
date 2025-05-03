using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class ListingReadEntity
{
    [BsonElement("_id")]
    public required ObjectId Id { get; set; }
   
    public required string Title { get; set; }
    public string? Content { get; set; }
   
    public required decimal Price { get; set; }
    public required bool IsDeleted { get; set; }
    
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public required UserReadEntity User { get; set; }
    public required List<MediaReadEntity> Media { get; set; } = [];
}