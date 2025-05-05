using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Media;

public class CreateMediaCommand : ICommand<List<MediaWriteEntity>>
{
    public required string ListingId {get;set;}
    public required List<FormFile> Files { get; set; }
}