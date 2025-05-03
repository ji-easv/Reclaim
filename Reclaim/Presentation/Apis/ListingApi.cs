namespace Reclaim.Presentation.Apis;

public static class ListingApi
{
    public static RouteGroupBuilder AddListingApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/listing");

        return api;
    }
}