using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Reclaim.Domain.Entities.Write;

public class ReviewWriteEntity
{
    [MaxLength(24)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [MinLength(1), MaxLength(512)]
    public required string Content { get; set; }
    public required int Rating { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } 
    public required bool IsDeleted { get; set; }
    
    // Navigation properties
    [MaxLength(24)]
    public required string AuthorId { get; set; }
    public virtual UserWriteEntity? Author { get; set; }
    
    [MaxLength(24)]
    public required string SellerId { get; set; }
    public virtual UserWriteEntity? Seller { get; set; }
}