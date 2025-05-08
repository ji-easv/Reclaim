using System.ComponentModel.DataAnnotations;

namespace Reclaim.Domain.DTOs;

public class MediaGetDto
{
    [Length(24,24)]
    public required string Id { get; set; }
    [Length(24,24)]
    public required string ListingId { get; set; }
    public required string SignedUrl { get; set; }
}