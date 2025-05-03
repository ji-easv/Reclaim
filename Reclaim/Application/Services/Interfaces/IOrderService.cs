using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Order;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Services.Interfaces;

public interface IOrderService
{
    Task<OrderGetDto> CreateOrderAsync(CreateOrderCommand command);
    Task<OrderGetDto> UpdateOrderAsync(UpdateOrderCommand command);
    Task<OrderGetDto> DeleteOrderAsync(DeleteOrderCommand command);
    Task<OrderGetDto> GetOrderByIdAsync(GetOrderByIdQuery query);
    Task<List<OrderGetDto>> GetOrdersForUserAsync(GetOrdersByUserIdQuery query);
}