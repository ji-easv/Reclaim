namespace Reclaim.Domain.DTOs;

public class UserGetDto
{
    public required string Id { get; set; } 
    public required string Name { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}