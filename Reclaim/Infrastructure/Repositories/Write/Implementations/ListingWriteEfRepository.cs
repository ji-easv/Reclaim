using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class ListingWriteEfRepository(PostgresDbContext dbContext) : IListingWriteRepository
{
    public async Task<ListingWriteEntity?> GetByIdAsync(string id)
    {
        var listing = await dbContext.Listings
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        return listing;
    }

    public async Task<IEnumerable<ListingWriteEntity>> GetAllAsync(bool includeDeleted = false)
    {
        var listings = await dbContext.Listings
            .Include(l => l.User)
            .Where(l => includeDeleted || !l.IsDeleted)
            .ToListAsync();
        return listings;
    }

    public async Task<ListingWriteEntity> AddAsync(ListingWriteEntity entity)
    {
        var result = await dbContext.Listings.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        var listingWithUser = await dbContext.Listings
            .Include(l => l.User)
            .FirstAsync(l => l.Id == result.Entity.Id);
       
        return listingWithUser;
    }

    public async Task<ListingWriteEntity> UpdateAsync(ListingWriteEntity entity)
    {
        var result = dbContext.Listings.Update(entity);
        await dbContext.SaveChangesAsync();
        var listingWithUser = await dbContext.Listings
            .Include(l => l.User)
            .FirstAsync(l => l.Id == result.Entity.Id);
        return listingWithUser;
    }

    public async Task<DateTimeOffset> DeleteAsync(ListingWriteEntity entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        dbContext.Listings.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity.UpdatedAt.Value;
    }
}