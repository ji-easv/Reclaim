using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.EventBus.Media;

public class MediaDeletedEvent : IDomainEvent
{
    public required List<MediaWriteEntity> MediaWriteEntityList { get; set; }
}