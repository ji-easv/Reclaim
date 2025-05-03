using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.User;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;

namespace Reclaim.Presentation.Apis;

public static class UserApi
{
    public static RouteGroupBuilder AddUserApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/user")
            .WithTags("User")
            .WithName("UserApi");
        
        api.MapPost("/create", CreateUserAsync);
        
        api.MapPut("/update", UpdateUserAsync);
        
        api.MapDelete("/delete/{userId}", DeleteUserAsync);
        
        api.MapGet("/{userId}", GetUserByIdAsync);
        
        return api;
    }
    
    private static async Task<Results<Ok<UserGetDto>, ProblemHttpResult>> GetUserByIdAsync(
        [FromServices] IUserService userService,
        [FromRoute] string userId // TODO: Replace with actual type
    )
    {
        var result = await userService.GetUserByIdAsync(new GetUserByIdQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<UserGetDto>, ProblemHttpResult>> CreateUserAsync(
        [FromServices] IUserService userService,
        [FromBody] CreateUserCommand createUserCommand
    )
    {
        var result = await userService.CreateUserAsync(createUserCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<UserGetDto>, ProblemHttpResult>> UpdateUserAsync(
        [FromServices] IUserService userService,
        [FromBody] UpdateUserCommand updateUserCommand
    )
    {
        var result = await userService.UpdateUserAsync(updateUserCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<UserGetDto>, ProblemHttpResult>> DeleteUserAsync(
        [FromServices] IUserService userService,
        [FromRoute] string userId // TODO: Replace with actual type
    )
    {
        var result = await userService.DeleteUserAsync(new DeleteUserCommand
        {
            
        });
        return TypedResults.Ok(result);
    }
}