﻿using Microsoft.AspNetCore.Http.HttpResults;
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
            .WithTags("Review");
        
        api.MapPost("/", CreateReviewAsync)
            .WithName("CreateReview");
        
        api.MapPut("/", UpdateReviewAsync)
            .WithName("UpdateReview");
        
        api.MapDelete("/{reviewId:length(24)}", DeleteReviewAsync)
            .WithName("DeleteReview");
        
        api.MapGet("/user/{userId:length(24)}", GetReviewsWrittenByUserAsync)
            .WithName("GetReviewsWrittenByUser");
        
        api.MapGet("/seller/{userId:length(24)}", GetReviewsForSellerAsync)
            .WithName("GetReviewsForSeller");
        
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
        [FromRoute] string reviewId
    )
    {
        var result = await reviewService.DeleteReviewAsync(new DeleteReviewCommand
        {
            ReviewId = reviewId
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ReviewGetDto>>, ProblemHttpResult>> GetReviewsWrittenByUserAsync(
        [FromServices] IReviewService reviewService,
        [FromRoute] string userId
    )
    {
        var result = await reviewService.GetReviewsWrittenByUserAsync(new GetReviewsWrittenByUserId
        {
            UserId = userId
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<ReviewGetDto>>, ProblemHttpResult>> GetReviewsForSellerAsync(
        [FromServices] IReviewService reviewService,
        [FromRoute] string userId
    )
    {
        var result = await reviewService.GetReviewsForSellerAsync(new GetReviewsForSellerQuery
        {
            SellerId = userId
        });
        return TypedResults.Ok(result);
    }
}