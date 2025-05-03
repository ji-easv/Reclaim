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
            .WithTags("Order")
            .WithName("OrderApi");
        
        api.MapPost("/create", CreateOrderAsync);
        
        api.MapPut("/update", UpdateOrderAsync);
        
        api.MapDelete("/delete/{orderId}", DeleteOrderAsync);
        
        api.MapGet("/{orderId}", GetOrderByIdAsync);
        
        api.MapGet("/user/{userId}", GetOrdersByUserIdAsync);

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
        [FromRoute] string orderId // TODO: Replace with actual type
    )
    {
        var result = await orderService.DeleteOrderAsync(new DeleteOrderCommand
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<OrderGetDto>, ProblemHttpResult>> GetOrderByIdAsync(
        [FromServices] IOrderService orderService,
        [FromRoute] string orderId // TODO: Replace with actual type
    )
    {
        var result = await orderService.GetOrderByIdAsync(new GetOrderByIdQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
    
    private static async Task<Results<Ok<List<OrderGetDto>>, ProblemHttpResult>> GetOrdersByUserIdAsync(
        [FromServices] IOrderService orderService,
        [FromRoute] string userId // TODO: Replace with actual type
    )
    {
        var result = await orderService.GetOrdersForUserAsync(new GetOrdersByUserIdQuery
        {
            
        });
        return TypedResults.Ok(result);
    }
}