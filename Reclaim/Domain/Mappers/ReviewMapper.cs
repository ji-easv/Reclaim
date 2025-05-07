using MongoDB.Bson;
using Reclaim.Application.Commands.Review;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class ReviewMapper
{
    public static ReviewGetDto ToDto(this ReviewWriteEntity entity)
    {
        return new ReviewGetDto
        {
            Id = entity.Id,
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Rating = entity.Rating,
            IsDeleted = entity.IsDeleted,
            Author = entity.Author?.ToGetDto() ?? throw new InvalidOperationException("Author cannot be null"),
            Seller = entity.Seller?.ToGetDto() ?? throw new InvalidOperationException("Seller cannot be null"),
        };
    }
    
    public static ReviewGetDto ToDto(this ReviewReadEntity entity)
    {
        return new ReviewGetDto
        {
            Id = entity.Id.ToString(),
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Rating = entity.Rating,
            IsDeleted = entity.IsDeleted,
            Author = entity.Author?.ToGetDto() ?? throw new InvalidOperationException("Author cannot be null"),
        };
    }

    public static ReviewWriteEntity ToEntity(this CreateReviewCommand command)
    {
        return new ReviewWriteEntity
        {
            Content = command.Content,
            IsDeleted = false,
            Rating = command.Rating,
            AuthorId = command.AuthorId,
            SellerId = command.SellerId
        };
    }

    public static ReviewReadEntity ToReadEntity(this ReviewWriteEntity writeEntity)
    {
        return new ReviewReadEntity
        {
            Id = ObjectId.Parse(writeEntity.Id),
            Content = writeEntity.Content,
            CreatedAt = writeEntity.CreatedAt,
            UpdatedAt = writeEntity.UpdatedAt,
            IsDeleted = writeEntity.IsDeleted,
            Rating = writeEntity.Rating,
            Author = writeEntity.Author?.ToReadEntity() ?? throw new InvalidOperationException("Author cannot be null"),
            SellerId = ObjectId.Parse(writeEntity.SellerId)
        };
    }
}