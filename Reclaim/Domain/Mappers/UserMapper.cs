using MongoDB.Bson;
using Reclaim.Application.Commands.User;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Domain.Mappers;

public static class UserMapper
{
    public static UserGetDto ToGetDto(this UserWriteEntity user)
    {
        return new UserGetDto
        {
            Id = user.Id,
            Name = user.Name,
            CreatedAt = user.CreatedAt
        };
    }
    
    public static UserReadEntity ToReadEntity(this UserWriteEntity user)
    {
        return new UserReadEntity
        {
            Id = ObjectId.Parse(user.Id),
            Name = user.Name,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Email = user.Email,
            IsDeleted = user.IsDeleted
        };
    }
}