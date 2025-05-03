using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Commands.User;

public class UserCommandHandler(IDomainEventBus eventBus) : ICommandHandler<CreateUserCommand, UserWriteEntity>,
    ICommandHandler<UpdateUserCommand, UserWriteEntity>,
    ICommandHandler<DeleteUserCommand, UserWriteEntity>
{
    public Task<UserWriteEntity> HandleAsync(CreateUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<UserWriteEntity> HandleAsync(DeleteUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<UserWriteEntity> HandleAsync(UpdateUserCommand command)
    {
        throw new NotImplementedException();
    }
}