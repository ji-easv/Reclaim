using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.Redis;

public class OrderReadRedisRepository(
    RedisContext redisContext,
    TimeSpan timeToLive,
    IOrderReadRepository persistantRepository) : IOrderReadRepository
{
}