using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reclaim.Application.Commands.Media;
using Reclaim.Application.Queries.Media;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;

namespace Reclaim.Presentation.Apis;

public static class MediaApi
{
    public static RouteGroupBuilder AddMediaApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/media")
            .WithTags("Media");

        api.MapPost("/", CreateMediaAsync)
            .WithName("CreateMedia")
            .DisableAntiforgery(); // For convenience, don't attempt IRL 

        api.MapDelete("/", DeleteMediaAsync)
            .WithName("DeleteMedia");
        
        api.MapGet("/listing/{listingId:length(24)}", GetMediaForListingAsync)
            .WithName("GetMediaForListing");

        return api;
    }

    private static async Task<Results<Ok<List<MediaGetDto>>, ProblemHttpResult>> GetMediaForListingAsync(
        [FromServices] IMediaService mediaService,
        [FromRoute] string listingId
        )
    {
        var result = await mediaService.GetMediaForListingAsync(new GetMediaForListingQuery
        {
            ListingId = listingId
        });
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<List<MediaGetDto>>, ProblemHttpResult>> CreateMediaAsync(
        [FromServices] IMediaService mediaService,
        [FromForm] string listingId,
        [FromForm] IFormFileCollection files,
        HttpRequest request
        )
    {
        var result = await mediaService.CreateMediaAsync(new CreateMediaCommand
        {
            ListingId = listingId,
            Files = files.ToList()
        });
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<List<MediaGetDto>>, ProblemHttpResult>> DeleteMediaAsync(
        [FromServices] IMediaService mediaService,
        [FromBody] DeleteMediaCommand deleteMediaCommand
    )
    {
        var result = await mediaService.DeleteMediaAsync(deleteMediaCommand);
        return TypedResults.Ok(result);
    }
}