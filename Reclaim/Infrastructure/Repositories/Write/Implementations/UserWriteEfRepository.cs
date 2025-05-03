using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class UserWriteEfRepository(PostgresDbContext dbContext) : IUserWriteRepository
{
    public async Task<UserWriteEntity?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserWriteEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
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

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<UserWriteEntity?> GetByEmailAsync(string email)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}