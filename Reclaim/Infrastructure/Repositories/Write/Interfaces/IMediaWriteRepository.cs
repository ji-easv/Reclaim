using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.Repositories.Write.Interfaces;

public interface IMediaWriteRepository : IWriteCrudRepository<MediaWriteEntity>
{
    Task<List<MediaWriteEntity>> AddRangeAsync(List<MediaWriteEntity> mediaWriteEntities);
    Task<List<MediaWriteEntity>> DeleteRangeAsync(List<MediaWriteEntity> mediaWriteEntities);
}