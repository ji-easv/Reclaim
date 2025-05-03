using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Services.Implementations;

public class ListingService(
    OrderQueryHandler queryHandler,
    OrderCommandHandler commandHandler,
    IDomainEventBus domainEventBus
    ) : IListingService
{
    public Task<ListingGetDto> CreateListingAsync(CreateListingCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ListingGetDto> UpdateListingAsync(UpdateListingCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ListingGetDto> DeleteListingAsync(DeleteListingCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ListingGetDto> GetListingByIdAsync(GetListingByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<List<ListingGetDto>> GetListingsForUserAsync(GetListingsByUserIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<List<ListingGetDto>> GetLatestListingsAsync(GetLatestListingsQuery query)
    {
        throw new NotImplementedException();
    }
}