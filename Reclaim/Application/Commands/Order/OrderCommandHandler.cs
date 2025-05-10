using System.Data;
using MongoDB.Bson;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Order;

public class OrderCommandHandler(
    IUnitOfWork unitOfWork, 
    IOrderWriteRepository orderWriteRepository, 
    IListingWriteRepository listingWriteRepository
    )
    : ICommandHandler<CreateOrderCommand, OrderWriteEntity>,
        ICommandHandler<UpdateOrderCommand, OrderWriteEntity>,
        ICommandHandler<DeleteOrderCommand, OrderWriteEntity>
{
    public async Task<OrderWriteEntity> HandleAsync(CreateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);

        try
        {
            var orderId = ObjectId.GenerateNewId().ToString();
            var listings = new List<ListingWriteEntity>();
            var totalPrice = 0m;
            
            if (command.Listings.Count == 0)
            {
                throw new CustomValidationException("No listings provided for the order.");
            }
        
            foreach (var listingId in command.Listings)
            {
                var listing = await listingWriteRepository.GetByIdAsync(listingId);
                if (listing is null)
                {
                    throw new NotFoundException($"Listing with ID {listingId} not found.");
                }
            
                if (listing.UserId == command.UserId)
                {
                    throw new CustomValidationException($"User {command.UserId} cannot buy their own listing.");
                }
            
                if (listing.OrderId != null)
                {
                    throw new AlreadyBoughtException($"Listing with ID {listingId} is already bought.");
                }
            
                totalPrice += listing.Price;
                listing.OrderId = orderId;
                listings.Add(listing);
            }
        
            var order = new OrderWriteEntity
            {
                Id = orderId,
                UserId = command.UserId,
                Listings = listings,
                IsDeleted = false,
                TotalPrice = totalPrice
            };
        
            var createdOrder = await orderWriteRepository.AddAsync(order);
            await unitOfWork.CommitAsync();
            return createdOrder;
        } 
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
    
    public async Task<OrderWriteEntity> HandleAsync(UpdateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);

        try
        {
            var order = await orderWriteRepository.GetByIdAsync(command.Id);
        
            if (order is null)
            {
                throw new NotFoundException($"Order with ID {command.Id} not found.");
            }
        
            order.Status = command.Status;
            order.UpdatedAt = DateTimeOffset.UtcNow;
        
            var updatedOrder = await orderWriteRepository.UpdateAsync(order);
            await unitOfWork.CommitAsync();
            return updatedOrder;
        } 
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
    
    public async Task<OrderWriteEntity> HandleAsync(DeleteOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var order = await orderWriteRepository.GetByIdAsync(command.Id);
            if (order is null)
            {
                throw new NotFoundException($"Order with ID {command.Id} not found.");
            }
            var deletedEntity = await orderWriteRepository.DeleteAsync(order);
            await unitOfWork.CommitAsync();
            return deletedEntity;
        } 
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}