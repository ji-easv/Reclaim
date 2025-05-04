using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Listing;

public class GetListingsByUserIdQuery : IQuery<IEnumerable<ListingGetDto>>
{
    public required string UserId { get; init; }
}