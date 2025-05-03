using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class UserWriteEfRepository(PostgresDbContext dbContext) : IUserWriteRepository
{
    public Task<UserWriteEntity?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserWriteEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserWriteEntity> AddAsync(UserWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserWriteEntity> UpdateAsync(UserWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}