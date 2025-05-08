using System.Data;
using MongoDB.Bson;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Order;

public class OrderCommandHandler(IUnitOfWork unitOfWork, IOrderWriteRepository orderWriteRepository, IListingWriteRepository listingWriteRepository)
    : ICommandHandler<CreateOrderCommand, OrderWriteEntity>,
        ICommandHandler<UpdateOrderCommand, OrderWriteEntity>,
        ICommandHandler<DeleteOrderCommand, OrderWriteEntity>
{
    public async Task<OrderWriteEntity> HandleAsync(CreateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);
        var orderId = ObjectId.GenerateNewId().ToString();
        var listings = new List<ListingWriteEntity>();
        
        foreach (var listingId in command.Listings)
        {
            var listing = await listingWriteRepository.GetByIdAsync(listingId);
            if (listing is null)
            {
                throw new NotFoundException($"Listing with ID {listingId} not found.");
            }
            if (listing.OrderId != null)
            {
                throw new AlreadyBoughtException($"Listing with ID {listingId} is already bought.");
            }
            listing.OrderId = orderId;
            listings.Add(listing);
        }
        
        var order = new OrderWriteEntity
        {
            Id = orderId,
            UserId = command.UserId,
            Listings = listings,
            IsDeleted = false
        };
        
        var createdOrder = await orderWriteRepository.AddAsync(order);
        await unitOfWork.CommitAsync();
        return createdOrder;
    }
    public async Task<OrderWriteEntity> HandleAsync(UpdateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);
        
        var order = await orderWriteRepository.GetByIdAsync(command.Id);
        
        var listings = new List<ListingWriteEntity>();
        
        foreach (var listingId in command.Listings)
        {
            var listing = await listingWriteRepository.GetByIdAsync(listingId);
            if (listing is null)
            {
                throw new NotFoundException($"Listing with ID {listingId} not found.");
            }
            if (listing.OrderId != null)
            {
                throw new AlreadyBoughtException($"Listing with ID {listingId} is already bought.");
            }
            
            listings.Add(listing);
        }
        
        order.UserId = command.UserId;
        order.Listings = listings;
        order.Status = command.Status;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        
        var updatedOrder = await orderWriteRepository.UpdateAsync(order);
        await unitOfWork.CommitAsync();
        return updatedOrder;
    }
    
    public async Task<OrderWriteEntity> HandleAsync(DeleteOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);
        var order = await orderWriteRepository.GetByIdAsync(command.Id);
        if (order is null)
        {
            throw new NotFoundException($"Order with ID {command.Id} not found.");
        }
        var deletedEntity = await orderWriteRepository.DeleteAsync(order);
        await unitOfWork.CommitAsync();
        return deletedEntity;
    }


}