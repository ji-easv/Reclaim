using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.Order;

public class GetOrdersByUserIdQuery : IQuery<IEnumerable<OrderGetDto>>
{
}