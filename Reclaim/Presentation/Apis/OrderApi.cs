namespace Reclaim.Presentation.Apis;

public static class OrderApi
{
    public static RouteGroupBuilder AddOrderApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/order");

        return api;
    }
}