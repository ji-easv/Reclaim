using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Enums;

namespace Reclaim.Application.Commands.Order;

public class UpdateOrderCommand : ICommand<OrderWriteEntity> 
{
    [MaxLength(24)]
    public required string Id { get; set; }
    
    public required OrderStatus Status { get; set; }
}