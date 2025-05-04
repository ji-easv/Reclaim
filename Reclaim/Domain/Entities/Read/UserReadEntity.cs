using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class UserReadEntity
{
    [BsonId]
    public required ObjectId Id { get; set; }
    
    [BsonRequired]
    public required string Name { get; set; }
    
    [BsonRequired]
    public required string Email { get; set; }
    
    [BsonRequired]
    public required bool IsDeleted { get; set; }

    [BsonRequired]
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}