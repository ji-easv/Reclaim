using StackExchange.Redis;

namespace Reclaim.Infrastructure.Contexts;

public class RedisContext(string connectionString)
{
    private readonly IConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);
    
    public IDatabase Database => _redis.GetDatabase();
}