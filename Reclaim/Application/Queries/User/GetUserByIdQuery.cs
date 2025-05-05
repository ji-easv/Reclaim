using Reclaim.Domain.Entities.Read;

namespace Reclaim.Application.Queries.User;

public class GetUserByIdQuery : IQuery<UserReadEntity>
{
    public required string UserId { get; set; }
}