using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;

namespace Reclaim.Presentation.Apis;

public static class OrderApi
{
    public static RouteGroupBuilder AddOrderApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/order")
            .WithTags("Order");
        
        api.MapPost("/", CreateOrderAsync)
            .WithName("CreateOrder");
        
        api.MapPut("/", UpdateOrderAsync)
            .WithName("UpdateOrder");
        
        api.MapDelete("/{orderId:length(24)}", DeleteOrderAsync)
            .WithName("DeleteOrder");
        
        api.MapGet("/{orderId:length(24)}", GetOrderByIdAsync)
            .WithName("GetOrderById");
        
        api.MapGet("/user/{userId:length(24)}", GetOrdersByUserIdAsync)
            .WithName("GetOrdersByUserId");

        return api;
    }
    
    private static async Task<Results<Ok<OrderGetDto>, ProblemHttpResult>> CreateOrderAsync(
        [FromServices] IOrderService orderService,
        [FromBody] CreateOrderCommand createOrderCommand
    )
    {
        var result = await orderService.CreateOrderAsync(createOrderCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<OrderGetDto>, ProblemHttpResult>> UpdateOrderAsync(
        [FromServices] IOrderService orderService,
        [FromBody] UpdateOrderCommand updateOrderCommand
    )
    {
        var result = await orderService.UpdateOrderAsync(updateOrderCommand);
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<OrderGetDto>, ProblemHttpResult>> DeleteOrderAsync(
        [FromServices] IOrderService orderService,
        [FromRoute] string orderId
    )
    {
        var result = await orderService.DeleteOrderAsync(new DeleteOrderCommand
        {
            Id = orderId
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<OrderGetDto>, ProblemHttpResult>> GetOrderByIdAsync(
        [FromServices] IOrderService orderService,
        [FromRoute] string orderId
    )
    {
        var result = await orderService.GetOrderByIdAsync(new GetOrderByIdQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<OrderGetDto>>, ProblemHttpResult>> GetOrdersByUserIdAsync(
        [FromServices] IOrderService orderService,
        [FromRoute] string userId
    )
    {
        var result = await orderService.GetOrdersForUserAsync(new GetOrdersByUserIdQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
}