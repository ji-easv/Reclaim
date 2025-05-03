using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Reclaim.Domain.Entities.Write;

public class MediaWriteEntity
{
    [MaxLength(24)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public required Guid ObjectKey { get; set; } = Guid.CreateVersion7();
    public required string MimeType { get; set; }
    public required long SizeBytes { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    // Navigation properties
    [MaxLength(24)]
    public required string ListingId { get; set; }
    public ListingWriteEntity? Listing { get; set; }
}