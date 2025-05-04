namespace Reclaim.Infrastructure.Repositories.Write.Interfaces;

public interface IWriteCrudRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id);
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<DateTimeOffset> DeleteAsync(TEntity entity);
}