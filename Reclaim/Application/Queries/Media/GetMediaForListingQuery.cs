using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Media;

public class GetMediaForListingQuery : IQuery<List<MediaReadEntity>>
{
    public required string ListingId { get; set; }
}