using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Repositories.Read.Interfaces;

public interface IMediaReadRepository
{
    Task<List<MediaReadEntity>> AddRangeAsync(List<MediaReadEntity> mediaReadEntities);
    Task<List<MediaReadEntity>> DeleteRangeAsync(string listingId, List<string> mediaIds);
    
    Task<List<MediaReadEntity>> GetMediaForListingAsync(string listingId);
}