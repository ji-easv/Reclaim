using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Listing;

public class GetListingByIdQuery : IQuery<ListingGetDto>
{
    public required string Id { get; init; }
}