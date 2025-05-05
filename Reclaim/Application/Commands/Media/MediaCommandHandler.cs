using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Repositories.S3;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Application.Commands.Media;

public class MediaCommandHandler(
    IMediaWriteRepository mediaWriteRepository,
    IObjectStorageRepository objectStorageRepository
)
    : ICommandHandler<CreateMediaCommand, List<MediaWriteEntity>>,
        ICommandHandler<DeleteMediaCommand, List<MediaWriteEntity>>
{
    public async Task<List<MediaWriteEntity>> HandleAsync(CreateMediaCommand command)
    {
        var mediaWriteEntities = new List<MediaWriteEntity>();

        foreach (var file in command.Files)
        {
            var s3Response = await objectStorageRepository.UploadFileAsync(
                file.FileName,
                file.OpenReadStream(),
                file.ContentType
            );

            mediaWriteEntities.Add(
                new MediaWriteEntity
                {
                    ListingId = command.ListingId,
                    MimeType = command.Files.First().ContentType,
                    SizeBytes = command.Files.Sum(f => f.Length),
                    CreatedAt = DateTimeOffset.UtcNow,
                    ObjectKey = s3Response.ObjectKey
                });
        }

        await mediaWriteRepository.AddRangeAsync(mediaWriteEntities);
        return mediaWriteEntities;
    }

    public async Task<List<MediaWriteEntity>> HandleAsync(DeleteMediaCommand command)
    {
        var mediaToDelete = new List<MediaWriteEntity>();
        foreach (var mediaId in command.MediaIds)
        {
            var mediaWriteEntity = await mediaWriteRepository.GetByIdAsync(mediaId);
            if (mediaWriteEntity == null)
            {
                throw new Exception("Media not found");
            }

            mediaToDelete.Add(mediaWriteEntity);
            await objectStorageRepository.DeleteFileAsync(mediaWriteEntity.ObjectKey);
        }
        
        await mediaWriteRepository.DeleteRangeAsync(mediaToDelete);

        return mediaToDelete;
    }
}