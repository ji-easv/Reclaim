
using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.Order;

public class GetOrderByIdQuery : IQuery<OrderReadEntity>
{
    public required string OrderId { get; set; }

}