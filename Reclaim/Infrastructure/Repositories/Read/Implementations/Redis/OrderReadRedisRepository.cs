using System.Text.Json;
using Reclaim.Domain.Converters;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;
using StackExchange.Redis;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.Redis;

public class OrderReadRedisRepository(
    RedisContext redisContext,
    TimeSpan timeToLive,
    IOrderReadRepository persistantRepository) : IOrderReadRepository
{
    private IDatabase Db => redisContext.Database;
    private static JsonSerializerOptions JsonSerializerOptions => new()
    {
        Converters =
        {
            new ObjectIdJsonConverter()
        }
    };
    
    public async Task<OrderReadEntity?> GetByIdAsync(string id)
    {
        // Try to get the entity from Redis cache first
        var cachedData = await Db.StringGetAsync(id);
        
        if (cachedData.HasValue)
        {
            // If found in cache, deserialize and return
            return JsonSerializer.Deserialize<OrderReadEntity>(cachedData, JsonSerializerOptions);
        }
        
        // If not in cache, get from persistent repository
        var entity = await persistantRepository.GetByIdAsync(id);
        
        // If found in persistent store, cache it for future requests
        if (entity != null)
        {
            await IndexAsync(entity);
        }
        
        return entity;
    }

    public async Task<IEnumerable<OrderReadEntity>> GetAllAsync(string userId)
    {
        // First try to get all orders from the persistent repository
        var orders = await persistantRepository.GetAllAsync(userId);
        
        // Cache all retrieved orders
        foreach (var order in orders)
        {
            await IndexAsync(order);
        }
        
        return orders;
    }

    public async Task<OrderReadEntity> AddAsync(OrderReadEntity entity)
    {
        var result = await persistantRepository.AddAsync(entity);
        await IndexAsync(result);
        return result;
    }

    public async Task<OrderReadEntity> UpdateAsync(OrderReadEntity entity)
    {
        var result = await persistantRepository.UpdateAsync(entity);
        await IndexAsync(result);
        return result;
    }

    public async Task<DateTimeOffset> DeleteAsync(OrderReadEntity entity)
    {
        // Delete from the persistent repository first
        var deletedAt = await persistantRepository.DeleteAsync(entity);
        
        // Remove from Redis cache
        var orderId = entity.Id.ToString();
        await Db.KeyDeleteAsync(orderId);
        
        return deletedAt;
    }
    
    private async Task IndexAsync(OrderReadEntity entity)
    {
        var orderId = entity.Id.ToString();
        var serializedOrder = JsonSerializer.Serialize(entity, JsonSerializerOptions);
        await Db.StringSetAsync(orderId, serializedOrder, timeToLive);
    }
}
