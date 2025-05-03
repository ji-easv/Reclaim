using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reclaim.Domain.Entities.Read;

public class ReviewReadEntity
{
    [BsonElement("_id")]
    public required ObjectId Id { get; set; }
    public required string Content { get; set; }
    public required int Rating { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } 
    public UserReadEntity? Author { get; set; }
    public required ObjectId SellerId { get; set; }
}