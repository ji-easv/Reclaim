using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class UserReadMongoRepository(MongoDbContext mongoDbContext) : IUserReadRepository
{
    public async Task<UserReadEntity?> GetByIdAsync(string id)
    {
        return await mongoDbContext.Users
            .Find(user => user.Id.ToString() == id)
            .FirstOrDefaultAsync();
    }

    public async Task<UserReadEntity> AddAsync(UserReadEntity entity)
    {
        await mongoDbContext.Users.InsertOneAsync(entity);
        return entity;
    }

    public async Task<UserReadEntity> UpdateAsync(UserReadEntity entity)
    {
        var filter = Builders<UserReadEntity>.Filter.Eq(user => user.Id, entity.Id);
        var update = Builders<UserReadEntity>.Update
            .Set(user => user.Name, entity.Name)
            .Set(user => user.UpdatedAt, entity.UpdatedAt)
            .Set(user => user.IsDeleted, entity.IsDeleted);

        await mongoDbContext.Users.UpdateOneAsync(filter, update);
        return entity;
    }
}