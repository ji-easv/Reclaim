using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class UserWriteEfRepository(PostgresDbContext dbContext) : IUserWriteRepository
{
    public async Task<UserWriteEntity?> GetByIdAsync(string id)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<UserWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        return await dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserWriteEntity> AddAsync(UserWriteEntity entity)
    {
        var result = await dbContext.Users.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<UserWriteEntity> UpdateAsync(UserWriteEntity entity)
    {
        var result = dbContext.Users.Update(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<DateTimeOffset> DeleteAsync(UserWriteEntity entity)
    {
        // Soft delete (erase personal data)
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        entity.Email = string.Empty;
        entity.Name = string.Empty;
        
        dbContext.Users.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity.UpdatedAt.Value;
    }

    public async Task<UserWriteEntity?> GetByEmailAsync(string email)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}