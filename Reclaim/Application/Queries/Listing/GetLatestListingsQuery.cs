using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Listing;

public class GetLatestListingsQuery : IQuery<IEnumerable<ListingReadEntity>>
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 100;
}