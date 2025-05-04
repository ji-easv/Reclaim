using MongoDB.Bson;
using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class ListingReadMongoRepository(MongoDbContext dbContext) : IListingReadRepository
{
    public async Task<ListingReadEntity> AddAsync(ListingReadEntity arg)
    {
        await dbContext.Listings.InsertOneAsync(arg);
        return arg;
    }

    public async Task<ListingReadEntity> UpdateAsync(ListingReadEntity arg)
    {
        var filter = Builders<ListingReadEntity>.Filter.Eq(e => e.Id, arg.Id);
        var update = Builders<ListingReadEntity>.Update
            .Set(e => e.Title, arg.Title)
            .Set(e => e.Content, arg.Content)
            .Set(e => e.Price, arg.Price)
            .Set(e => e.UpdatedAt, DateTimeOffset.UtcNow);

        await dbContext.Listings.UpdateOneAsync(filter, update);
        return arg;
    }

    public async Task<DateTimeOffset> DeleteAsync(ListingReadEntity arg)
    {
        var filter = Builders<ListingReadEntity>.Filter.Eq(e => e.Id, arg.Id);
        var update = Builders<ListingReadEntity>.Update
            .Set(e => e.IsDeleted, true)
            .Set(e => e.UpdatedAt, DateTimeOffset.UtcNow);

        await dbContext.Listings.UpdateOneAsync(filter, update);
        return arg.UpdatedAt ?? DateTimeOffset.UtcNow;
    }

    public async Task<ListingReadEntity?> GetByIdAsync(string id)
    {
        var filter = Builders<ListingReadEntity>.Filter.Eq(e => e.Id, ObjectId.Parse(id));
        return await dbContext.Listings.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ListingReadEntity>> GetLatestAsync(int skip = 0, int take = 100)
    {
        var filter = Builders<ListingReadEntity>.Filter.Eq(e => e.IsDeleted, false);
        return await dbContext.Listings.Find(filter)
            .SortByDescending(e => e.CreatedAt)
            .Skip(skip)
            .Limit(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByUserIdAsync(string userId)
    {
        var filter = Builders<ListingReadEntity>.Filter.Eq(e => e.User.Id, ObjectId.Parse(userId));
        return await dbContext.Listings.Find(filter)
            .SortByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var filter = Builders<ListingReadEntity>.Filter.And(
            Builders<ListingReadEntity>.Filter.Gte(e => e.Price, minPrice),
            Builders<ListingReadEntity>.Filter.Lte(e => e.Price, maxPrice)
        );

        return await dbContext.Listings.Find(filter)
            .SortByDescending(e => e.CreatedAt)
            .ToListAsync();
    }
}