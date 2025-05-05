using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Queries.Media;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Listing;

namespace Reclaim.Application.Services.Implementations;

public class ListingService(
    ListingQueryHandler queryHandler,
    ListingCommandHandler commandHandler,
    IDomainEventBus domainEventBus,
    IMediaService mediaService
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
        var listing = await queryHandler.HandleAsync(query);
        var signedMedia = await GetSignedMediaAsync(listing.Media);
        return listing.ToDto(signedMedia);
    }

    public async Task<List<ListingGetDto>> GetListingsForUserAsync(GetListingsByUserIdQuery query)
    {
        var listings = await queryHandler.HandleAsync(query);
        var listingDtos = new List<ListingGetDto>();
        
        foreach (var listing in listings)
        {
            var signedMedia = await GetSignedMediaAsync(listing.Media);
            listingDtos.Add(listing.ToDto(signedMedia));
        }

        return listingDtos;
    }

    public async Task<List<ListingGetDto>> GetLatestListingsAsync(GetLatestListingsQuery query)
    {
        var listings = await queryHandler.HandleAsync(query);
        var listingDtos = new List<ListingGetDto>();
        
        foreach (var listing in listings)
        {
            var signedMedia = await GetSignedMediaAsync(listing.Media);
            listingDtos.Add(listing.ToDto(signedMedia));
        }
        
        return listingDtos;
    }
    
    private async Task<List<MediaGetDto>> GetSignedMediaAsync(List<MediaReadEntity> media)
    {
        var signedMedia = new List<MediaGetDto>();
        foreach (var mediaItem in media)
        {
            var signedUrl = await mediaService.GetSignedUrlByObjectKeyAsync(mediaItem.ObjectKey);
            signedMedia.Add(mediaItem.ToGetDto(signedUrl));
        }

        return signedMedia;
    }
}