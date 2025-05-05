using Reclaim.Domain.Entities.Read;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Application.Queries.User;

public class UserQueryHandler(IUserReadRepository userReadRepository) : IQueryHandler<GetUserByIdQuery, UserReadEntity>
{
    public async Task<UserReadEntity> HandleAsync(GetUserByIdQuery query)
    {
        var user = await userReadRepository.GetByIdAsync(query.UserId);
        if (user is null)
        {
            throw new NotFoundException($"User with ID {query.UserId} not found.");
        }

        return user;
    }
}