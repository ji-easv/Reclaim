using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Listing;

public class CreateListingCommand : ICommand<ListingWriteEntity>
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required decimal Price { get; set; }
    public required string UserId { get; set; }
}