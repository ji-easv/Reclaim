using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Repositories.Read.Interfaces;

public interface IReviewReadRepository
{
    Task<ReviewReadEntity?> GetByIdAsync(string id);
    Task<List<ReviewReadEntity>> GetBySellerIdAsync(string sellerId);
    Task<List<ReviewReadEntity>> GetByUserIdAsync(string userId);
    Task<ReviewReadEntity> AddAsync(ReviewReadEntity review);
    Task<ReviewReadEntity> UpdateAsync(ReviewReadEntity entity);
}