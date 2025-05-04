using System.Text.Json;
using MongoDB.Bson;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;
using StackExchange.Redis;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.Redis;

public class ListingReadRedisRepository(
    RedisContext redisContext,
    TimeSpan timeToLive,
    IListingReadRepository persistentRepository) : IListingReadRepository
{
    private IDatabase Db => redisContext.Database;
    
    public async Task<ListingReadEntity> AddAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.AddAsync(arg);
        var listingId = arg.Id.ToString();
        await Db.StringSetAsync(listingId, result.ToJson(), timeToLive);
        return result;
    }

    public async Task<ListingReadEntity> UpdateAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.UpdateAsync(arg);
        var listingId = arg.Id.ToString();
        await Db.StringSetAsync(listingId, result.ToJson(), timeToLive);
        return result;
    }

    public async Task<DateTimeOffset> DeleteAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.DeleteAsync(arg);
        var listingId = arg.Id.ToString();
        await Db.KeyDeleteAsync(listingId);
        return result;
    }

    public async Task<ListingReadEntity?> GetByIdAsync(string id)
    {
        var cachedListing = await Db.StringGetAsync(id);
        if (cachedListing.IsNullOrEmpty)
        {
            var result = await persistentRepository.GetByIdAsync(id);
            if (result == null)
            {
                return null;
            }
            
            await Db.StringSetAsync(id, result.ToJson(), timeToLive);
            return result;
        }
        
        return JsonSerializer.Deserialize<ListingReadEntity>(cachedListing!);
    }

    public async Task<IEnumerable<ListingReadEntity>> GetLatestAsync(int skip, int take = 100)
    {
        return await persistentRepository.GetLatestAsync(skip, take);
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByUserIdAsync(string userId)
    {
        var cachedListings = await Db.StringGetAsync(userId);
        if (cachedListings.IsNullOrEmpty)
        {
            var result = (await persistentRepository.GetByUserIdAsync(userId)).ToList();
            await Db.StringSetAsync(userId, result.ToJson(), timeToLive);
            return result;
        }
        
        var listings = JsonSerializer.Deserialize<IEnumerable<ListingReadEntity>>(cachedListings!);
        return listings ?? [];
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await persistentRepository.GetByPriceRangeAsync(minPrice, maxPrice);
    }
}