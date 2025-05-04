using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Reclaim.Domain.Entities.Write;

public class UserWriteEntity
{
    [MaxLength(24)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [MaxLength(128)]
    public required string Name { get; set; }
    
    [MaxLength(128)]
    public required string Email { get; set; }
    
    public required bool IsDeleted { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}