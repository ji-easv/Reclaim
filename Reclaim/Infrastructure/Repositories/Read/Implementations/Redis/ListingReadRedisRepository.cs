using System.Text.Json;
using Reclaim.Domain.Converters;
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
    private static JsonSerializerOptions JsonSerializerOptions => new()
    {
        Converters =
        {
            new ObjectIdJsonConverter()
        }
    };
    
    public async Task<ListingReadEntity> AddAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.AddAsync(arg);
        await IndexAsync(result);
        return result;
    }

    public async Task<ListingReadEntity> UpdateAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.UpdateAsync(arg);
        await IndexAsync(result);
        return result;
    }

    public async Task<DateTimeOffset> DeleteAsync(ListingReadEntity arg)
    {
        var result = await persistentRepository.DeleteAsync(arg);
        
        // Remove from cache
        var listingId = arg.Id.ToString();
        await Db.KeyDeleteAsync(listingId);
        
        // Remove from user cache
        var key = GetUserIdKey(arg.User.Id.ToString());
        await Db.KeyDeleteAsync(key);
        
        return result;
    }

    public async Task<ListingReadEntity?> GetByIdAsync(string id, bool includeDeleted = false)
    {
        var cachedListing = await Db.StringGetAsync(id);
        if (cachedListing.IsNullOrEmpty)
        {
            var result = await persistentRepository.GetByIdAsync(id, includeDeleted);
            if (result == null)
            {
                return null;
            }
            
            await IndexAsync(result);
            return result;
        }
        
        return JsonSerializer.Deserialize<ListingReadEntity>(cachedListing!, JsonSerializerOptions);
    }

    public async Task<IEnumerable<ListingReadEntity>> GetLatestAsync(int skip, int take = 100)
    {
        return await persistentRepository.GetLatestAsync(skip, take);
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByUserIdAsync(string userId, bool includeDeleted = false)
    {
        var key = GetUserIdKey(userId);
        var cachedListings = await Db.StringGetAsync(key);
        if (cachedListings.IsNullOrEmpty)
        {
            var result = (await persistentRepository.GetByUserIdAsync(userId, includeDeleted)).ToList();
            await Db.StringSetAsync(key, JsonSerializer.Serialize(result, JsonSerializerOptions), timeToLive);
            return result;
        }
        
        var listings = JsonSerializer.Deserialize<IEnumerable<ListingReadEntity>>(cachedListings!, JsonSerializerOptions);
        return listings ?? [];
    }

    public async Task<IEnumerable<ListingReadEntity>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await persistentRepository.GetByPriceRangeAsync(minPrice, maxPrice);
    }
    
    private async Task IndexAsync(ListingReadEntity listingReadEntity)
    {
        var listingId = listingReadEntity.Id.ToString();
        await Db.StringSetAsync(listingId, JsonSerializer.Serialize(listingReadEntity, JsonSerializerOptions), timeToLive);
    }
    
    private string GetUserIdKey(string userId)
    {
        return $"$userId:{userId}";
    }
}