namespace Reclaim.Presentation.Apis;

public static class ReviewApi
{
    public static RouteGroupBuilder AddReviewApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/review");

        return api;
    }
}