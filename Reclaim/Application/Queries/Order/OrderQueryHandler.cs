using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Order;

public class OrderQueryHandler : IQueryHandler<GetOrdersByUserIdQuery, IEnumerable<OrderGetDto>>,
    IQueryHandler<GetOrderByIdQuery, OrderGetDto>
{
    public Task<OrderGetDto> HandleAsync(GetOrderByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderGetDto>> HandleAsync(GetOrdersByUserIdQuery query)
    {
        throw new NotImplementedException();
    }
}