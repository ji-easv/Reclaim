using Reclaim.Application.Commands.Media;
using Reclaim.Application.Queries.Media;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Media;

namespace Reclaim.Application.Services.Implementations;

public class MediaService(
    MediaCommandHandler mediaCommandHandler,
    MediaQueryHandler mediaQueryHandler,
    IDomainEventBus domainEventBus
) : IMediaService
{
    public async Task<List<MediaGetDto>> CreateMediaAsync(CreateMediaCommand command)
    {
        var mediaWriteEntity = await mediaCommandHandler.HandleAsync(command);
        await domainEventBus.Publish(new MediaCreatedEvent { MediaWriteEntityList = mediaWriteEntity });

        var mediaGetDtoList = new List<MediaGetDto>();
        foreach (var mediaWrite in mediaWriteEntity)
        {
            var mediaGetDto = new MediaGetDto
            {
                Id = mediaWrite.Id,
                ListingId = mediaWrite.ListingId,
                SignedUrl = await GetSignedUrlByObjectKeyAsync(mediaWrite.ObjectKey)
            };
            mediaGetDtoList.Add(mediaGetDto);
        }

        return mediaGetDtoList;
    }

    public async Task<List<MediaGetDto>> DeleteMediaAsync(DeleteMediaCommand command)
    {
        var mediaWriteEntity = await mediaCommandHandler.HandleAsync(command);
        await domainEventBus.Publish(new MediaDeletedEvent { MediaWriteEntityList = mediaWriteEntity });

        return mediaWriteEntity.Select(mediaWrite => new MediaGetDto
            { Id = mediaWrite.Id, ListingId = mediaWrite.ListingId, SignedUrl = string.Empty }).ToList();
    }

    public async Task<List<MediaGetDto>> GetMediaForListingAsync(GetMediaForListingQuery query)
    {
        var mediaReadEntities = await mediaQueryHandler.HandleAsync(query);
        var mediaGetDtoList = new List<MediaGetDto>();

        foreach (var mediaRead in mediaReadEntities)
        {
            var mediaGetDto = new MediaGetDto
            {
                Id = mediaRead.Id.ToString(),
                ListingId = mediaRead.ListingId.ToString(),
                SignedUrl = await GetSignedUrlByObjectKeyAsync(mediaRead.ObjectKey)
            };
            mediaGetDtoList.Add(mediaGetDto);
        }

        return mediaGetDtoList;
    }

    private async Task<string> GetSignedUrlByObjectKeyAsync(Guid objectKey)
    {
        var getSignedUrlByObjectKeyQuery = new GetSignedUrlByObjectKeyQuery
        {
            ObjectKey = objectKey
        };

        return await mediaQueryHandler.HandleAsync(getSignedUrlByObjectKeyQuery);
    }
}