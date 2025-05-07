using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Order;

public class GetOrdersByUserIdQuery : IQuery<IEnumerable<OrderReadEntity>>
{
    public required string UserId { get; init; }
}