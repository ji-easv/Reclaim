using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Repositories.Read.Interfaces;

public interface IOrderReadRepository
{
    Task<OrderReadEntity?> GetByIdAsync(string id);
    Task<IEnumerable<OrderReadEntity>> GetAllAsync(string userId);
    Task<OrderReadEntity> AddAsync(OrderReadEntity entity);
    Task<OrderReadEntity> UpdateAsync(OrderReadEntity entity);
    
    Task<DateTimeOffset> DeleteAsync(OrderReadEntity entity);
    
    
}