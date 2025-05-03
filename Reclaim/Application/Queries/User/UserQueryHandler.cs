using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Queries.User;

public class UserQueryHandler : IQueryHandler<GetUserByIdQuery, UserGetDto>
{
    public Task<UserGetDto> HandleAsync(GetUserByIdQuery query)
    {
        throw new NotImplementedException();
    }
}