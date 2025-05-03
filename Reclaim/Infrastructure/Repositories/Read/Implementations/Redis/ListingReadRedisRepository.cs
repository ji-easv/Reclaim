using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.Redis;

public class ListingReadRedisRepository(
    RedisContext redisContext,
    TimeSpan timeToLive,
    IListingReadRepository persistentRepository) : IListingReadRepository
{
}