using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class UserReadEntity
{
    [BsonElement("_id")]
    public required ObjectId Id { get; set; }
    
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required bool IsDeleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}