using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.User;

public class UpdateUserCommand : ICommand<UserWriteEntity>
{
    [MaxLength(24)]
    public required string UserId { get; set; } = string.Empty;
    
    public required string Name { get; set; } = string.Empty;
}