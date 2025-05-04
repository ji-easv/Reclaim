using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class MediaReadEntity
{
    [BsonId]
    public required ObjectId Id { get; set; }
    
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid ObjectKey { get; set; }
    
    [BsonRequired]
    public required string MimeType { get; set; }
    
    [BsonRequired]
    public required long SizeBytes { get; set; }
    
    [BsonRequired]
    public required DateTimeOffset CreatedAt { get; set; }
    
    // Navigation properties
    [BsonRequired]
    public required ObjectId ListingId { get; set; }
}