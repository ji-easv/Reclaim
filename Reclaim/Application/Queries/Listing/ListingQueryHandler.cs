using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Listing;

public class ListingQueryHandler : 
    IQueryHandler<GetLatestListingsQuery, IEnumerable<ListingGetDto>>, 
    IQueryHandler<GetListingsByUserIdQuery, IEnumerable<ListingGetDto>>, 
    IQueryHandler<GetListingByIdQuery, ListingGetDto>
{
    public Task<IEnumerable<ListingGetDto>> HandleAsync(GetLatestListingsQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingGetDto>> HandleAsync(GetListingsByUserIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<ListingGetDto> HandleAsync(GetListingByIdQuery query)
    {
        throw new NotImplementedException();
    }
}