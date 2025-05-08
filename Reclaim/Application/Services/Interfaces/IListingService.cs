using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Queries.Listing;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Services.Interfaces;

public interface IListingService
{
    Task<ListingGetDto> CreateListingAsync(CreateListingCommand command);
    Task<ListingGetDto> UpdateListingAsync(UpdateListingCommand command);
    Task<ListingGetDto> DeleteListingAsync(DeleteListingCommand command);
    Task<ListingGetDto> GetListingByIdAsync(GetListingByIdQuery query);
    Task<List<ListingGetDto>> GetListingsForUserAsync(GetListingsByUserIdQuery query);
    Task<List<ListingGetDto>> GetLatestListingsAsync(GetLatestListingsQuery query);
    Task<List<MediaGetDto>> GetSignedMediaAsync(List<MediaReadEntity> media);
}