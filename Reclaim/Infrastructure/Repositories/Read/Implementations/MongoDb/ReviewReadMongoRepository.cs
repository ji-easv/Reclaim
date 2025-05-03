using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class ReviewReadMongoRepository(MongoDbContext mongoDbContext) : IReviewReadRepository
{
}