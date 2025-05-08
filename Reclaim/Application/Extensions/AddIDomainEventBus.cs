using Reclaim.Infrastructure.EventBus.EventBus;
using Reclaim.Infrastructure.EventBus.Listing;
using Reclaim.Infrastructure.EventBus.Media;
using Reclaim.Infrastructure.EventBus.Order;
using Reclaim.Infrastructure.EventBus.Review;
using Reclaim.Infrastructure.EventBus.User;

namespace Reclaim.Application.Extensions;

public static class AddIDomainEventBus
{
    public static void AddDomainEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();
        // Registering the event handlers as hosted services
        services.AddHostedService<ListingEventsHandler>();
        services.AddHostedService<UserEventsHandler>();
        services.AddHostedService<ReviewEventsHandler>();
        services.AddHostedService<OrderEventsHandler>();
        services.AddHostedService<MediaEventsHandler>();
    }
}