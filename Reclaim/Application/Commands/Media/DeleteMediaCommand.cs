using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Media;

public class DeleteMediaCommand : ICommand<List<MediaWriteEntity>>
{
    public required List<string> MediaIds { get; set; }
}