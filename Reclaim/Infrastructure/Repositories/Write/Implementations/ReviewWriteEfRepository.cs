using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class ReviewWriteEfRepository(PostgresDbContext dbContext) : IReviewWriteRepository
{
    public async Task<ReviewWriteEntity?> GetByIdAsync(string id, bool includeDeleted = false)
    {
        return await dbContext.Reviews
            .Include(r => r.Author)
            .Include(r => r.Seller)
            .Where(r => includeDeleted || !r.IsDeleted)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<ReviewWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        var query = dbContext.Reviews
            .Include(r => r.Author)
            .Include(r => r.Seller)
            .Where(r => includeDeleted || !r.IsDeleted)
            .AsQueryable();
        
        return await query.ToListAsync();
    }

    public async Task<ReviewWriteEntity> AddAsync(ReviewWriteEntity entity)
    {
        var result = await dbContext.Reviews.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        var createdReview = await dbContext.Reviews
            .Include(r => r.Author)
            .Include(r => r.Seller)
            .FirstAsync(l => l.Id == result.Entity.Id);
       
        return createdReview;
    }

    public async Task<ReviewWriteEntity> UpdateAsync(ReviewWriteEntity entity)
    {
        var result = dbContext.Reviews.Update(entity);
        
        var updatedReview = await dbContext.Reviews
            .Include(r => r.Author)
            .Include(r => r.Seller)
            .FirstAsync(l => l.Id == result.Entity.Id);
       
        return updatedReview;
    }

    public async Task<ReviewWriteEntity> DeleteAsync(ReviewWriteEntity entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        dbContext.Reviews.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}