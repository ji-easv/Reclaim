using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Order;

namespace Reclaim.Application.Services.Implementations;

public class OrderService(
    OrderCommandHandler commandHandler, 
    OrderQueryHandler queryHandler, 
    IDomainEventBus eventBus
    ) : IOrderService
{
    public async Task<OrderGetDto> CreateOrderAsync(CreateOrderCommand command)
    {
        var order = await commandHandler.HandleAsync(command);
        await eventBus.Publish(new OrderCreatedEvent
        {
            OrderWriteEntity = order
        });
        return order.ToGetDto();
    }

    public async Task<OrderGetDto> UpdateOrderAsync(UpdateOrderCommand command)
    {
       var order = await commandHandler.HandleAsync(command);
         await eventBus.Publish(new OrderUpdatedEvent
         {
              OrderWriteEntity = order
         });
        return order.ToGetDto();
        
    }

    public async Task<OrderGetDto> DeleteOrderAsync(DeleteOrderCommand command)
    {
        var order = await commandHandler.HandleAsync(command);
        await eventBus.Publish(new OrderDeletedEvent
        {
            OrderId = command.Id,
          
        });
        return order.ToGetDto();
    }

    public async Task<OrderGetDto> GetOrderByIdAsync(GetOrderByIdQuery query)
    {
        var order = await queryHandler.HandleAsync(query);
        return order.ToGetDto();
    }

    public async Task<List<OrderGetDto>> GetOrdersForUserAsync(GetOrdersByUserIdQuery query)
    {
        var orders = await queryHandler.HandleAsync(query);
        return orders.Select(o => o.ToGetDto()).ToList();
    }
}