using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.User;

public class UserQueryHandler : IQueryHandler<GetUserByIdQuery, UserReadEntity>
{
    public async Task<UserReadEntity> HandleAsync(GetUserByIdQuery query)
    {
        throw new NotImplementedException();
    }
}