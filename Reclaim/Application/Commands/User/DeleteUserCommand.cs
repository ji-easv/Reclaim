using System.ComponentModel.DataAnnotations;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Application.Commands.User;

public class DeleteUserCommand : ICommand<UserWriteEntity>
{
    [MaxLength(24)]
    public required string UserId { get; set; }
}