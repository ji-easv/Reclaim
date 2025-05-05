using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Listing;

public class GetListingsByUserIdQuery : IQuery<IEnumerable<ListingReadEntity>>
{
    public required string UserId { get; init; }
    public bool IncludeDeleted { get; init; } = false;
}