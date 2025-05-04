using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Listing;

namespace Reclaim.Application.Services.Implementations;

public class ListingService(
    ListingQueryHandler queryHandler,
    ListingCommandHandler commandHandler,
    IDomainEventBus domainEventBus
) : IListingService
{
    public async Task<ListingGetDto> CreateListingAsync(CreateListingCommand command)
    {
        var listing = await commandHandler.HandleAsync(command);
        var listingDto = listing.ToDto();
        await domainEventBus.Publish(new ListingCreatedEvent
        {
            ListingWriteEntity = listing
        });

        return listingDto;
    }

    public async Task<ListingGetDto> UpdateListingAsync(UpdateListingCommand command)
    {
        var listing = await commandHandler.HandleAsync(command);
        var listingDto = listing.ToDto();
        await domainEventBus.Publish(new ListingUpdatedEvent
        {
            ListingWriteEntity = listing
        });

        return listingDto;
    }

    public async Task<ListingGetDto> DeleteListingAsync(DeleteListingCommand command)
    {
        var listing = await commandHandler.HandleAsync(command);
        var listingDto = listing.ToDto();
        await domainEventBus.Publish(new ListingDeletedEvent
        {
            ListingWriteEntity = listing
        });

        return listingDto;
    }

    public async Task<ListingGetDto> GetListingByIdAsync(GetListingByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ListingGetDto>> GetListingsForUserAsync(GetListingsByUserIdQuery query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ListingGetDto>> GetLatestListingsAsync(GetLatestListingsQuery query)
    {
        throw new NotImplementedException();
    }
}