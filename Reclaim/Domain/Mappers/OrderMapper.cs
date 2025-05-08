using MongoDB.Bson;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class OrderMapper
{
    public static OrderGetDto ToGetDto(this OrderWriteEntity orderWriteEntity)
    {
        return new OrderGetDto
        {
            Id = orderWriteEntity.Id,
            UserId = orderWriteEntity.UserId,
            TotalPrice = orderWriteEntity.TotalPrice,
            Status = orderWriteEntity.Status,
            CreatedAt = orderWriteEntity.CreatedAt,
            UpdatedAt = orderWriteEntity.UpdatedAt,
            IsDeleted = orderWriteEntity.IsDeleted,
            Listings = orderWriteEntity.Listings.Select(l => l.ToDto()).ToList()
        };
    }

    public static OrderGetDto ToGetDto(this OrderReadEntity orderReadEntity, List<ListingGetDto> listingDtos)
    {
        return new OrderGetDto
        {
            Id = orderReadEntity.Id.ToString(),
            UserId = orderReadEntity.UserId.ToString(),
            TotalPrice = orderReadEntity.TotalAmount,
            Status = orderReadEntity.Status,
            CreatedAt = orderReadEntity.CreatedAt,
            UpdatedAt = orderReadEntity.UpdatedAt,
            IsDeleted = orderReadEntity.IsDeleted,
            Listings = listingDtos
        };
    }

    public static OrderReadEntity ToReadEntity(this OrderWriteEntity orderWriteEntity)
    {
        return new OrderReadEntity
        {
            Id = ObjectId.Parse(orderWriteEntity.Id),
            UserId = ObjectId.Parse(orderWriteEntity.UserId),
            TotalAmount = orderWriteEntity.TotalPrice,
            Status = orderWriteEntity.Status,
            CreatedAt = orderWriteEntity.CreatedAt,
            UpdatedAt = orderWriteEntity.UpdatedAt,
            IsDeleted = orderWriteEntity.IsDeleted,
            Listings = orderWriteEntity.Listings.Select(l => l.ToReadEntity()).ToList()
        };
    }
}