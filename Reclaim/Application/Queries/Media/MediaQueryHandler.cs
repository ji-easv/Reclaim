using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;
using Reclaim.Infrastructure.Repositories.S3;

namespace Reclaim.Application.Queries.Media;

public class MediaQueryHandler(
    IMediaReadRepository mediaReadRepository,
    IObjectStorageRepository objectStorageRepository
    ) 
    : IQueryHandler<GetMediaForListingQuery, List<MediaReadEntity>>,
        IQueryHandler<GetSignedUrlByObjectKeyQuery, string>
{ 
    public async Task<List<MediaReadEntity>> HandleAsync(GetMediaForListingQuery query)
    {
        return await mediaReadRepository.GetMediaForListingAsync(query.ListingId);
    }

    public async Task<string> HandleAsync(GetSignedUrlByObjectKeyQuery query)
    {
        return await objectStorageRepository.GetSignedFileUrlAsync(query.ObjectKey);
    }
}