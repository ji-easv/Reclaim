using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class UserReadMongoRepository(MongoDbContext mongoDbContext) : IUserReadRepository
{
}