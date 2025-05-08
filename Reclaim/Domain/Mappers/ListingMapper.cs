using MongoDB.Bson;
using Reclaim.Application.Commands.Listing;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class ListingMapper
{
    public static ListingGetDto ToDto(this ListingWriteEntity entity)
    {
        return new ListingGetDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
            Price = entity.Price,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            User = entity.User?.ToGetDto() ?? throw new InvalidOperationException("User cannot be null"),
            Media = []
        };
    }
    
    public static ListingGetDto ToDto(this ListingReadEntity entity, List<MediaGetDto> media)
    {
        return new ListingGetDto
        {
            Id = entity.Id.ToString(),
            Title = entity.Title,
            Content = entity.Content,
            Price = entity.Price,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            User = entity.User?.ToGetDto() ?? throw new InvalidOperationException("User cannot be null"),
            Media = media
        };
    }

    public static ListingWriteEntity ToEntity(this CreateListingCommand command)
    {
        return new ListingWriteEntity
        {
            Title = command.Title,
            Content = command.Content,
            Price = command.Price,
            UserId = command.UserId,
            IsDeleted = false
        };
    }

    public static ListingReadEntity ToReadEntity(this ListingWriteEntity writeEntity, List<MediaWriteEntity> media)
    {
        return new ListingReadEntity
        {
            Id = ObjectId.Parse(writeEntity.Id),
            Title = writeEntity.Title,
            Content = writeEntity.Content,
            Price = writeEntity.Price,
            CreatedAt = writeEntity.CreatedAt,
            UpdatedAt = writeEntity.UpdatedAt,
            IsDeleted = writeEntity.IsDeleted,
            User = writeEntity.User?.ToReadEntity() ?? throw new InvalidOperationException("User cannot be null"),
            Media = media.Select(m => m.ToReadEntity()).ToList()
        };
    }
    
    public static ListingReadEntity ToReadEntity(this ListingWriteEntity writeEntity)
    {
        return new ListingReadEntity
        {
            Id = ObjectId.Parse(writeEntity.Id),
            Title = writeEntity.Title,
            Content = writeEntity.Content,
            Price = writeEntity.Price,
            CreatedAt = writeEntity.CreatedAt,
            UpdatedAt = writeEntity.UpdatedAt,
            IsDeleted = writeEntity.IsDeleted,
            User = writeEntity.User?.ToReadEntity() ?? throw new InvalidOperationException("User cannot be null"),
            Media = writeEntity.Media.Select(m => m.ToReadEntity()).ToList()
        };
    }
}