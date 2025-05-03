using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.User;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Services.Implementations;

public class UserService(
    UserCommandHandler commandHandler,
    UserQueryHandler queryHandler,
    IDomainEventBus domainEventBus
) : IUserService
{
    public Task<UserGetDto> CreateUserAsync(CreateUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> UpdateUserAsync(UpdateUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> DeleteUserAsync(DeleteUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> GetUserByIdAsync(GetUserByIdQuery query)
    {
        throw new NotImplementedException();
    }
}