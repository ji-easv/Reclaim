using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Order;

public class DeleteOrderCommand : ICommand<OrderWriteEntity>
{
    [MaxLength(24)]
    public required string Id { get; set; }
}