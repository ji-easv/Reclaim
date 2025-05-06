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
        return await dbContext.Orders
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrderWriteEntity> AddAsync(OrderWriteEntity entity)
    {
        var result = await dbContext.Orders.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<OrderWriteEntity> UpdateAsync(OrderWriteEntity entity)
    {
        var result = dbContext.Orders.Update(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<OrderWriteEntity> DeleteAsync(OrderWriteEntity entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        
        dbContext.Orders.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}