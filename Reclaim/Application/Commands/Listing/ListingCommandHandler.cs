using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.EventBus;

namespace Reclaim.Application.Commands.Listing;

public class ListingCommandHandler(IDomainEventBus domainEventBus) : ICommandHandler<CreateListingCommand, ListingWriteEntity>,
    ICommandHandler<UpdateListingCommand, ListingWriteEntity>, ICommandHandler<DeleteListingCommand, ListingWriteEntity>
{
    public Task<ListingWriteEntity> HandleAsync(CreateListingCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ListingWriteEntity> HandleAsync(UpdateListingCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ListingWriteEntity> HandleAsync(DeleteListingCommand command)
    {
        throw new NotImplementedException();
    }
}