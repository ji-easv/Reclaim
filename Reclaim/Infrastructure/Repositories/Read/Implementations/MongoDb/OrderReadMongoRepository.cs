using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class OrderReadMongoRepository(MongoDbContext mongoDbContext) : IOrderReadRepository
{

    public async Task<OrderReadEntity?> GetByIdAsync(string id)
    {
        return await mongoDbContext.Orders.Find(order => order.Id.ToString() == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderReadEntity>> GetAllAsync(string userId)
    {
        return await mongoDbContext.Orders
            .Find(order => order.UserId.ToString() == userId)
            .ToListAsync();
        
    }

    public async Task<OrderReadEntity> AddAsync(OrderReadEntity entity)
    {
        await mongoDbContext.Orders.InsertOneAsync(entity);
        return entity;
    }

    public async Task<OrderReadEntity> UpdateAsync(OrderReadEntity entity)
    {
        var filter = Builders<OrderReadEntity>.Filter.Eq(order => order.Id, entity.Id);
        var update = Builders<OrderReadEntity>.Update
            .Set(order => order.Status, entity.Status)
            .Set(order => order.UpdatedAt, entity.UpdatedAt)
            .Set(order => order.IsDeleted, entity.IsDeleted);
        await mongoDbContext.Orders.UpdateOneAsync(filter, update);
        return entity;
    }
    
}