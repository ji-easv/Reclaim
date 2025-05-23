﻿using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.Order;

public class CreateOrderCommand : ICommand<OrderWriteEntity>
{
    
    [MaxLength(24)]
    public required string UserId { get; set; }
    
    public required List<string> Listings { get; set; } = [];
}