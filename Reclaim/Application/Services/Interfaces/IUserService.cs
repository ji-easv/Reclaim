using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.User;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserGetDto> CreateUserAsync(CreateUserCommand command);
    Task<UserGetDto> UpdateUserAsync(UpdateUserCommand command);
    Task<UserGetDto> DeleteUserAsync(DeleteUserCommand command);
    Task<UserGetDto> GetUserByIdAsync(GetUserByIdQuery query);
}