using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.Repositories.Write.Interfaces;

public interface IUserWriteRepository : IWriteCrudRepository<UserWriteEntity>
{
    Task<UserWriteEntity?> GetByEmailAsync(string email);
}