using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Reclaim.Domain.Entities.Write;

public class ListingWriteEntity
{
    [MaxLength(24)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [MaxLength(128)]
    public required string Title { get; set; }
    
    [MaxLength(1024)]
    public string? Content { get; set; }
    
    public required decimal Price { get; set; }
    public required bool IsDeleted { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    
    // Navigation properties
    [MaxLength(24)]
    public required string UserId { get; set; }
    public UserWriteEntity? User { get; set; }
    
    [MaxLength(24)]
    public string? OrderId { get; set; }
}