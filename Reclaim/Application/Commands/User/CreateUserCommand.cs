using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.User;

public class CreateUserCommand : ICommand<UserWriteEntity>
{
    [MaxLength(128)]
    public required string Name { get; set; }
    
    [MaxLength(128)]
    public required string Email { get; set; }
}