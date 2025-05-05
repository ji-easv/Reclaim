using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Listing;

public class GetListingByIdQuery : IQuery<ListingReadEntity>
{
    public required string Id { get; init; }
    public bool IncludeDeleted { get; init; } = false;
}