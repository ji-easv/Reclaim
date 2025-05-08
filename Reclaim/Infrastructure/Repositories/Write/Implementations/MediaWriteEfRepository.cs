using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class MediaWriteEfRepository(PostgresDbContext dbContext) : IMediaWriteRepository
{
    public async Task<MediaWriteEntity?> GetByIdAsync(string id, bool includeDeleted = false)
    {
        var media = await dbContext.Media
            .FirstOrDefaultAsync(x => x.Id == id);

        return media;
    }

    public async Task<IEnumerable<MediaWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        var media = await dbContext.Media
            .ToListAsync();

        return media;
    }

    public async Task<MediaWriteEntity> AddAsync(MediaWriteEntity entity)
    {
        var result = await dbContext.Media.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<MediaWriteEntity> UpdateAsync(MediaWriteEntity entity)
    {
        var result = dbContext.Update(entity);
        await dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<MediaWriteEntity> DeleteAsync(MediaWriteEntity entity)
    {
        var result = dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<MediaWriteEntity>> AddRangeAsync(List<MediaWriteEntity> mediaWriteEntities)
    {
        await dbContext.Media.AddRangeAsync(mediaWriteEntities);
        await dbContext.SaveChangesAsync();

        return mediaWriteEntities;
    }

    public async Task<List<MediaWriteEntity>> DeleteRangeAsync(List<MediaWriteEntity> mediaWriteEntities)
    {
        dbContext.Media.RemoveRange(mediaWriteEntities);
        await dbContext.SaveChangesAsync();

        return mediaWriteEntities;
    }
}