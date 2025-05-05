using MongoDB.Bson;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class MediaMapper
{
    public static MediaReadEntity ToReadEntity(this MediaWriteEntity writeEntity)
    {
        return new MediaReadEntity
        {
            Id = ObjectId.Parse(writeEntity.Id),
            CreatedAt = writeEntity.CreatedAt,
            ListingId = ObjectId.Parse(writeEntity.ListingId),
            MimeType = writeEntity.MimeType,
            ObjectKey = writeEntity.ObjectKey,
            SizeBytes = writeEntity.SizeBytes
        };
    }
    
    public static MediaGetDto ToGetDto(this MediaReadEntity entity, string signedUrl)
    {
        return new MediaGetDto
        {
            Id = entity.Id.ToString(),
            ListingId = entity.ListingId.ToString(),
            SignedUrl = signedUrl
        };
    }
}