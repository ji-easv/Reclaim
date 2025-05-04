using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class OrderWriteEfRepository(PostgresDbContext dbContext) : IOrderWriteRepository
{
    public Task<OrderWriteEntity?> GetByIdAsync(string id, bool includeDeleted = false)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> AddAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> UpdateAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<DateTimeOffset> DeleteAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }
}