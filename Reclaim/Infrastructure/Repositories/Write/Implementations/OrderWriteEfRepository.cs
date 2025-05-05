using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class OrderWriteEfRepository(PostgresDbContext dbContext) : IOrderWriteRepository
{
    public async Task<OrderWriteEntity?> GetByIdAsync(string id, bool includeDeleted = false)
    {
        return await dbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == id && (includeDeleted || !x.IsDeleted));
    }

    public async Task<IEnumerable<OrderWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        return await Task.FromResult(dbContext.Orders
            .AsNoTracking()
            .ToListAsync());
    }

    public Task<OrderWriteEntity> AddAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> UpdateAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> DeleteAsync(OrderWriteEntity entity)
    {
        throw new NotImplementedException();
    }
}