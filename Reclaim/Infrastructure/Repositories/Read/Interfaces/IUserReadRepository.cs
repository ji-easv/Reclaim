using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Repositories.Read.Interfaces;

public interface IUserReadRepository
{
    Task<UserReadEntity?> GetByIdAsync(string id);
    Task<UserReadEntity> AddAsync(UserReadEntity entity);
    Task<UserReadEntity> UpdateAsync(UserReadEntity entity);
}