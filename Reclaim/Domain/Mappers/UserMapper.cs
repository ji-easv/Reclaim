using MongoDB.Bson;
using Reclaim.Application.Commands.User;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class UserMapper
{
    public static UserGetDto ToGetDto(this UserWriteEntity userWriteEntity)
    {
        return new UserGetDto
        {
            Id = userWriteEntity.Id,
            Name = userWriteEntity.Name,
            CreatedAt = userWriteEntity.CreatedAt
        };
    }

    public static UserGetDto ToGetDto(this UserReadEntity userReadEntity)
    {
        return new UserGetDto
        {
            Id = userReadEntity.Id.ToString(),
            Name = userReadEntity.Name,
            CreatedAt = userReadEntity.CreatedAt
        };
    }
    
    public static UserReadEntity ToReadEntity(this UserWriteEntity userWriteEntity)
    {
        return new UserReadEntity
        {
            Id = ObjectId.Parse(userWriteEntity.Id),
            Name = userWriteEntity.Name,
            CreatedAt = userWriteEntity.CreatedAt,
            UpdatedAt = userWriteEntity.UpdatedAt,
            Email = userWriteEntity.Email,
            IsDeleted = userWriteEntity.IsDeleted
        };
    }
}