using Reclaim.Application.Commands.Order;
using Reclaim.Application.Queries.Media;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Domain.DTOs;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Mappers;
using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Order;

namespace Reclaim.Application.Services.Implementations;

public class OrderService(
    OrderCommandHandler orderCommandHandler, 
    OrderQueryHandler orderQueryHandler,
    IDomainEventBus eventBus,
    IListingService listingService
    ) : IOrderService
{
    public async Task<OrderGetDto> CreateOrderAsync(CreateOrderCommand command)
    {
        var order = await orderCommandHandler.HandleAsync(command);
        await eventBus.Publish(new OrderCreatedEvent
        {
            OrderWriteEntity = order
        });

        return order.ToGetDto();
    }

    public async Task<OrderGetDto> UpdateOrderAsync(UpdateOrderCommand command)
    {
       var order = await orderCommandHandler.HandleAsync(command);
         await eventBus.Publish(new OrderUpdatedEvent
         {
              OrderWriteEntity = order
         });
       
         return order.ToGetDto();
    }

    public async Task<OrderGetDto> DeleteOrderAsync(DeleteOrderCommand command)
    {
        var order = await orderCommandHandler.HandleAsync(command);
        await eventBus.Publish(new OrderDeletedEvent
        {
            OrderId = command.Id,
            DeletedAt = order.UpdatedAt!.Value
        });
        
        return order.ToGetDto();
    }

    public async Task<OrderGetDto> GetOrderByIdAsync(GetOrderByIdQuery query)
    {
        var order = await orderQueryHandler.HandleAsync(query);
        
        var listingTasks = order.Listings.Select(async listing =>
        {
            var media = await listingService.GetSignedMediaAsync(listing.Media);
            return listing.ToDto(media);
        });

        var listingDtos = await Task.WhenAll(listingTasks);

        return order.ToGetDto(listingDtos.ToList());
    }

    public async Task<List<OrderGetDto>> GetOrdersForUserAsync(GetOrdersByUserIdQuery query)
    {
        var orders = await orderQueryHandler.HandleAsync(query);
        
        // Flatten all listings with their parent order
        var listingOrderPairs = orders
            .SelectMany(order => order.Listings.Select(listing => (order, listing)))
            .ToList();

        // Fetch all listing DTOs in parallel
        var listingDtoTasks = listingOrderPairs.Select(async pair =>
        {
            var media = await listingService.GetSignedMediaAsync(pair.listing.Media);
            return (pair.order, ListingDto: pair.listing.ToDto(media));
        });

        var listingDtoResults = await Task.WhenAll(listingDtoTasks);

        // Group listing DTOs by order
        var orderIdToListings = listingDtoResults
            .GroupBy(x => x.order)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ListingDto).ToList());

        // Build final order DTOs
        var orderDtos = orders.Select(order =>
            order.ToGetDto(orderIdToListings.TryGetValue(order, out var listings) ? listings : new List<ListingGetDto>())
        ).ToList();
        
        return orderDtos;
    }
}