using System.Data;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Listing;

public class ListingCommandHandler(
    IListingWriteRepository listingWriteRepository,
    IUserWriteRepository userWriteRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<CreateListingCommand, ListingWriteEntity>,
        ICommandHandler<UpdateListingCommand, ListingWriteEntity>,
        ICommandHandler<DeleteListingCommand, ListingWriteEntity>
{
    public async Task<ListingWriteEntity> HandleAsync(CreateListingCommand command)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            var user = await userWriteRepository.GetByIdAsync(command.UserId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {command.UserId} not found.");
            }

            var createdListing = await listingWriteRepository.AddAsync(command.ToEntity());
            await unitOfWork.CommitAsync();
            return createdListing;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<ListingWriteEntity> HandleAsync(DeleteListingCommand command)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            var listing = await listingWriteRepository.GetByIdAsync(command.Id);
            if (listing == null)
            {
                throw new NotFoundException($"Listing with ID {command.Id} not found.");
            }

            var deletedListing = await listingWriteRepository.DeleteAsync(listing);
            await unitOfWork.CommitAsync();
            return deletedListing;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<ListingWriteEntity> HandleAsync(UpdateListingCommand command)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            var listing = await listingWriteRepository.GetByIdAsync(command.Id);
            if (listing == null)
            {
                throw new NotFoundException($"Listing with ID {command.Id} not found.");
            }

            listing.Title = command.Title;
            listing.Content = command.Content;
            listing.Price = command.Price;

            var updatedListing = await listingWriteRepository.UpdateAsync(listing);
            await unitOfWork.CommitAsync();
            return updatedListing;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}