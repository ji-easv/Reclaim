using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Repositories.Read.Interfaces;

public interface IListingReadRepository
{
    Task<ListingReadEntity> AddAsync(ListingReadEntity arg);
    Task<ListingReadEntity> UpdateAsync(ListingReadEntity arg);
    Task<DateTimeOffset> DeleteAsync(ListingReadEntity arg);
    
    Task<ListingReadEntity?> GetByIdAsync(string id);
    Task<IEnumerable<ListingReadEntity>> GetLatestAsync(int skip = 0, int take = 100);
    Task<IEnumerable<ListingReadEntity>> GetByUserIdAsync(string userId);
    Task<IEnumerable<ListingReadEntity>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}