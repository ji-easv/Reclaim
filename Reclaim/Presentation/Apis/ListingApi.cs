using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;

namespace Reclaim.Presentation.Apis;

public static class ListingApi
{
    public static RouteGroupBuilder AddListingApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/listing")
            .WithTags("Listing");
        
        api.MapPost("/", CreateListingAsync)
            .WithName("CreateListing");

        api.MapPut("/", UpdateListingAsync)
            .WithName("UpdateListing");

        api.MapDelete("/{listingId:length(24)}", DeleteListingAsync)
            .WithName("DeleteListing");

        api.MapGet("/{listingId:length(24)}", GetListingByIdAsync)
            .WithName("GetListingById");
        
        api.MapGet("/user/{userId:length(24)}", GetListingsByUserIdAsync)
            .WithName("GetListingsByUserId");
        
        api.MapGet("/latest", GetLatestListingsAsync)
            .WithName("GetLatestListings");
        
        return api;
    }

    private static async Task<Results<Ok<ListingGetDto>, ProblemHttpResult>> CreateListingAsync(
        [FromServices] IListingService listingService,
        [FromBody] CreateListingCommand createListingCommand
    )
    {
        var result = await listingService.CreateListingAsync(createListingCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<ListingGetDto>, ProblemHttpResult>> UpdateListingAsync(
        [FromServices] IListingService listingService,
        [FromBody] UpdateListingCommand updateListingCommand
    )
    {
        var result = await listingService.UpdateListingAsync(updateListingCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<ListingGetDto>, ProblemHttpResult>> DeleteListingAsync(
        [FromServices] IListingService listingService,
        [FromRoute] string listingId
    )
    {
        var result = await listingService.DeleteListingAsync(new DeleteListingCommand
        {
            Id = listingId
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<ListingGetDto>, ProblemHttpResult>> GetListingByIdAsync(
        [FromServices] IListingService listingService,
        [FromRoute] string listingId
    )
    {
        var query = new GetListingByIdQuery
        {
            Id = listingId
        };
        var result = await listingService.GetListingByIdAsync(query);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ListingGetDto>>, ProblemHttpResult>> GetListingsByUserIdAsync(
        [FromServices] IListingService listingService,
        [FromRoute] string userId
    )
    {
        var query = new GetListingsByUserIdQuery
        {
            UserId = userId
        };
        var result = await listingService.GetListingsForUserAsync(query);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ListingGetDto>>, ProblemHttpResult>> GetLatestListingsAsync(
        [FromServices] IListingService listingService
    )
    {
        var result = await listingService.GetLatestListingsAsync(new GetLatestListingsQuery());
        return TypedResults.Ok(result);
    }
}