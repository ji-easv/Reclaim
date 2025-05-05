using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Write.Implementations;

public class ReviewWriteEfRepository(PostgresDbContext dbContext) : IReviewWriteRepository
{
    public Task<ReviewWriteEntity?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewWriteEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReviewWriteEntity> AddAsync(ReviewWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewWriteEntity> UpdateAsync(ReviewWriteEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<DateTimeOffset> DeleteAsync(ReviewWriteEntity entity)
    {
        throw new NotImplementedException();
    }
}