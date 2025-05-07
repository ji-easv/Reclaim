using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Application.Queries.Order;

public class OrderQueryHandler(IOrderReadRepository orderReadRepository) : IQueryHandler<GetOrdersByUserIdQuery, IEnumerable<OrderReadEntity>>,
    IQueryHandler<GetOrderByIdQuery, OrderReadEntity>
{
    public async Task<OrderReadEntity> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await orderReadRepository.GetByIdAsync(query.OrderId);
        if (order is null)
        {
            throw new NotFoundException($"Order with ID {query.OrderId} not found.");
        }
        return order;
    }

    public async Task<IEnumerable<OrderReadEntity>> HandleAsync(GetOrdersByUserIdQuery query)
    {
        return await orderReadRepository.GetAllAsync(query.UserId);
    }
}