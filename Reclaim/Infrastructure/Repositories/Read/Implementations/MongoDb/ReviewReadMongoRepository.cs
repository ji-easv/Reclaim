using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class ReviewReadMongoRepository(MongoDbContext mongoDbContext) : IReviewReadRepository
{
    public async Task<ReviewReadEntity?> GetByIdAsync(string id)
    {
        return await mongoDbContext.Reviews
            .Find(review => review.Id.ToString() == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ReviewReadEntity>> GetBySellerIdAsync(string sellerId)
    {
        return await mongoDbContext.Reviews
            .Find(review => review.SellerId.ToString() == sellerId)
            .ToListAsync();
    }
    
    public async Task<List<ReviewReadEntity>> GetByUserIdAsync(string userId)
    {
        return await mongoDbContext.Reviews
            .Find(review => review.Author.Id.ToString() == userId)
            .ToListAsync();
    }

    public async Task<ReviewReadEntity> AddAsync(ReviewReadEntity review)
    {
        await mongoDbContext.Reviews.InsertOneAsync(review);
        return review;
    }

    public async Task<ReviewReadEntity> UpdateAsync(ReviewReadEntity entity)
    {
        var filter = Builders<ReviewReadEntity>.Filter.Eq(review => review.Id, entity.Id);
        var update = Builders<ReviewReadEntity>.Update
            .Set(review => review.Content, entity.Content)
            .Set(review => review.Rating, entity.Rating)
            .Set(review => review.UpdatedAt, entity.UpdatedAt)
            .Set(user => user.IsDeleted, entity.IsDeleted)
            .Set(review => review.Author, entity.Author)
            .Set(review => review.SellerId, entity.SellerId);

        await mongoDbContext.Reviews.UpdateOneAsync(filter, update);
        return entity;
    }
}