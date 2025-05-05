using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.User;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.User;

namespace Reclaim.Application.Services.Implementations;

public class UserService(
    UserCommandHandler commandHandler,
    UserQueryHandler queryHandler,
    IDomainEventBus domainEventBus
) : IUserService
{
    public async Task<UserGetDto> CreateUserAsync(CreateUserCommand command)
    {
        var user = await commandHandler.HandleAsync(command);
        await domainEventBus.Publish(new UserCreatedEvent
        {
            UserWriteEntity = user
        });
        return user.ToGetDto();
    }

    public async Task<UserGetDto> UpdateUserAsync(UpdateUserCommand command)
    {
        var user = await commandHandler.HandleAsync(command);
        await domainEventBus.Publish(new UserUpdatedEvent
        {
            UserWriteEntity = user
        });
        return user.ToGetDto();
    }

    public async Task<UserGetDto> DeleteUserAsync(DeleteUserCommand command)
    {
        var user = await commandHandler.HandleAsync(command);
         await domainEventBus.Publish(new UserDeletedEvent
        {
            UserId = command.UserId,
            DeletedAt = user.UpdatedAt!.Value
        });
        return user.ToGetDto();
    }

    public async Task<UserGetDto> GetUserByIdAsync(GetUserByIdQuery query)
    {
        var user = await queryHandler.HandleAsync(query);
        return user.ToGetDto();
    }
}