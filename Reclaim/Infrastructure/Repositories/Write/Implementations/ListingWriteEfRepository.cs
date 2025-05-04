using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class ListingWriteEfRepository(PostgresDbContext dbContext) : IListingWriteRepository
{
    public Task<ListingWriteEntity?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListingWriteEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ListingWriteEntity> AddAsync(ListingWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ListingWriteEntity> UpdateAsync(ListingWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(ListingWriteEntity entity)
    {
        throw new NotImplementedException();
    }
}