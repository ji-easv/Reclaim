using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Queries.Listing;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Services.Interfaces;

public interface IListingService
{
    Task<ListingGetDto> CreateListingAsync(CreateListingCommand command);
    Task<ListingGetDto> UpdateListingAsync(UpdateListingCommand command);
    Task<ListingGetDto> DeleteListingAsync(DeleteListingCommand command);
    Task<ListingGetDto> GetListingByIdAsync(GetListingByIdQuery query);
    Task<List<ListingGetDto>> GetListingsForUserAsync(GetListingsByUserIdQuery query);
    Task<List<ListingGetDto>> GetLatestListingsAsync(GetLatestListingsQuery query);
}