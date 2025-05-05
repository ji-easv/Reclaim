using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Application.Queries.Listing;

public class ListingQueryHandler(IListingReadRepository listingReadRepository) :
    IQueryHandler<GetLatestListingsQuery, IEnumerable<ListingReadEntity>>,
    IQueryHandler<GetListingsByUserIdQuery, IEnumerable<ListingReadEntity>>,
    IQueryHandler<GetListingByIdQuery, ListingReadEntity>
{
    public async Task<IEnumerable<ListingReadEntity>> HandleAsync(GetLatestListingsQuery query)
    {
        return await listingReadRepository.GetLatestAsync(query.Page, query.PageSize);
    }

    public async Task<ListingReadEntity> HandleAsync(GetListingByIdQuery query)
    {
        var listing = await listingReadRepository.GetByIdAsync(query.Id, query.IncludeDeleted);
        if (listing == null)
        {
            throw new NotFoundException($"Listing with ID {query.Id} not found");
        }
        
        return listing;
    }

    public async Task<IEnumerable<ListingReadEntity>> HandleAsync(GetListingsByUserIdQuery query)
    {
       return await listingReadRepository.GetByUserIdAsync(query.UserId);
    }
}