using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.EventBus;

namespace Reclaim.Application.Commands.Order;

public class OrderCommandHandler(IDomainEventBus domainEventBus)
    : ICommandHandler<CreateOrderCommand, OrderWriteEntity>,
        ICommandHandler<UpdateOrderCommand, OrderWriteEntity>,
        ICommandHandler<DeleteOrderCommand, OrderWriteEntity>
{
    public Task<OrderWriteEntity> HandleAsync(CreateOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> HandleAsync(UpdateOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderWriteEntity> HandleAsync(DeleteOrderCommand command)
    {
        throw new NotImplementedException();
    }
}