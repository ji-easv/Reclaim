using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Listing;

public class DeleteListingCommand : ICommand<ListingWriteEntity>
{
    public required string Id { get; set; }
}