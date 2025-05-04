namespace Reclaim.Infrastructure.Repositories.Write.Interfaces;

public interface IWriteCrudRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, bool includeDeleted = false);
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}