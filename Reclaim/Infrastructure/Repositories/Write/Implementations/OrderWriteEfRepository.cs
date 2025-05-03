using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class OrderWriteEfRepository(PostgresDbContext dbContext) : IOrderWriteRepository
{
    public Task<OrderWriteEntity?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderWriteEntity>> GetAllAsync()
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

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}