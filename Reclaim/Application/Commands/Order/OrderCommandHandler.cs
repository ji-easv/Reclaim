using System.Data;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Order;

public class OrderCommandHandler(IUnitOfWork unitOfWork, IOrderWriteRepository orderWriteRepository)
    : ICommandHandler<CreateOrderCommand, OrderWriteEntity>,
        ICommandHandler<UpdateOrderCommand, OrderWriteEntity>,
        ICommandHandler<DeleteOrderCommand, OrderWriteEntity>
{
    public async Task<OrderWriteEntity> HandleAsync(CreateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        var existingOrder = await orderWriteRepository.GetByIdAsync(command.Id);
        if (existingOrder is not null)
        {
            throw new InsertionConflictException($"Order with ID {command.Id} already exists.");
        }
        
        var order = new OrderWriteEntity
        {
            UserId = command.UserId,
            Listings = command.Listings,
            IsDeleted = false
        };
        
        var createdOrder = await orderWriteRepository.AddAsync(order);
        await unitOfWork.CommitAsync();
        return createdOrder;
    }

    public async Task<OrderWriteEntity> HandleAsync(DeleteOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        var order = await orderWriteRepository.GetByIdAsync(command.Id);
        if (order is null)
        {
            throw new NotFoundException($"Order with ID {command.Id} not found.");
        }
        var deletedEntity = await orderWriteRepository.DeleteAsync(order);
        await unitOfWork.CommitAsync();
        return deletedEntity;
    }

    public async Task<OrderWriteEntity> HandleAsync(UpdateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        var order = await orderWriteRepository.GetByIdAsync(command.Id);
        if (order is null)
        {
            throw new NotFoundException($"Order with ID {command.Id} not found.");
        }
        order.UserId = command.UserId;
        order.Listings = command.Listings;
        order.Status = command.Status;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        
        var updatedOrder = await orderWriteRepository.UpdateAsync(order);
        await unitOfWork.CommitAsync();
        return updatedOrder;
    }
}