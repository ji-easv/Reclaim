namespace Reclaim.Presentation.Apis;

public static class UserApi
{
    public static RouteGroupBuilder AddUserApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/user");

        return api;
    }
}