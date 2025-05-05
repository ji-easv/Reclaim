using System.Data;
using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Order;

public class OrderCommandHandler(IDomainEventBus domainEventBus, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOrderCommand, OrderWriteEntity>,
        ICommandHandler<UpdateOrderCommand, OrderWriteEntity>,
        ICommandHandler<DeleteOrderCommand, OrderWriteEntity>
{
    public async Task<OrderWriteEntity> HandleAsync(CreateOrderCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        //var existingOrder = await orderWriteRepository.GetByIdAsync(command.UserId);
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> HandleAsync(DeleteOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> HandleAsync(UpdateOrderCommand command)
    {
        throw new NotImplementedException();
    }
}