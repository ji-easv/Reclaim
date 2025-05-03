using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Services.Implementations;

public class OrderService(
    OrderCommandHandler commandHandler, 
    OrderQueryHandler queryHandler, 
    IDomainEventBus eventBus
    ) : IOrderService
{
    public Task<OrderGetDto> CreateOrderAsync(CreateOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderGetDto> UpdateOrderAsync(UpdateOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderGetDto> DeleteOrderAsync(DeleteOrderCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<OrderGetDto> GetOrderByIdAsync(GetOrderByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderGetDto>> GetOrdersForUserAsync(GetOrdersByUserIdQuery query)
    {
        throw new NotImplementedException();
    }
}