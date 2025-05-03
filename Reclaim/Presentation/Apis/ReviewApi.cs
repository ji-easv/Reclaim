using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reclaim.Application.Commands.Review;
using Reclaim.Application.Queries.Review;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;

namespace Reclaim.Presentation.Apis;

public static class ReviewApi
{
    public static RouteGroupBuilder AddReviewApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/review")
            .WithTags("Review")
            .WithName("ReviewApi");
        
        api.MapPost("/create", CreateReviewAsync);
        
        api.MapPut("/update", UpdateReviewAsync);
        
        api.MapDelete("/delete/{reviewId}", DeleteReviewAsync);
        
        api.MapGet("/user/{userId}", GetReviewsWrittenByUserAsync);
        
        api.MapGet("/seller/{userId}", GetReviewsForSellerAsync);
        
        return api;
    }
    
    private static async Task<Results<Ok<ReviewGetDto>, ProblemHttpResult>> CreateReviewAsync(
        [FromServices] IReviewService reviewService,
        [FromBody] CreateReviewCommand createReviewCommand
    )
    {
        var result = await reviewService.CreateReviewAsync(createReviewCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<ReviewGetDto>, ProblemHttpResult>> UpdateReviewAsync(
        [FromServices] IReviewService reviewService,
        [FromBody] UpdateReviewCommand updateReviewCommand
    )
    {
        var result = await reviewService.UpdateReviewAsync(updateReviewCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<ReviewGetDto>, ProblemHttpResult>> DeleteReviewAsync(
        [FromServices] IReviewService reviewService,
        [FromRoute] string reviewId // TODO: Replace with actual type
    )
    {
        var result = await reviewService.DeleteReviewAsync(new DeleteReviewCommand
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ReviewGetDto>>, ProblemHttpResult>> GetReviewsWrittenByUserAsync(
        [FromServices] IReviewService reviewService,
        [FromRoute] string userId // TODO: Replace with actual type
    )
    {
        var result = await reviewService.GetReviewsWrittenByUserAsync(new GetReviewsWrittenByUserId
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ReviewGetDto>>, ProblemHttpResult>> GetReviewsForSellerAsync(
        [FromServices] IReviewService reviewService,
        [FromRoute] string userId // TODO: Replace with actual type
    )
    {
        var result = await reviewService.GetReviewsForSellerAsync(new GetReviewsForSellerQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
}