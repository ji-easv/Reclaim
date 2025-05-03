using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class MediaReadEntity
{
    [BsonElement("_id")]
    public required MediaId Id { get; set; }
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid ObjectKey { get; set; }
    public required string MimeType { get; set; }
    public required long SizeBytes { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    
    // Navigation properties
    public required string ListingId { get; set; }
}

public class MediaId
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    public required string Id { get; set; }
}