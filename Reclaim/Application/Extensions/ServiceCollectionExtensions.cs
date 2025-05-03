using Microsoft.EntityFrameworkCore;
using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Commands.Order;
using Reclaim.Application.Commands.Review;
using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Queries.Review;
using Reclaim.Application.Queries.User;
using Reclaim.Application.Services.Implementations;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Implementations;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;
using Reclaim.Infrastructure.Repositories.Write.Implementations;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;

namespace Reclaim.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommandAndQueryHandlers(this IServiceCollection services)
    {
        // Register all command handlers
        services.AddScoped<ListingCommandHandler>();
        services.AddScoped<OrderCommandHandler>();
        services.AddScoped<ReviewCommandHandler>();
        services.AddScoped<UserCommandHandler>();

        // Register all query handlers
        services.AddScoped<ListingQueryHandler>();
        services.AddScoped<OrderQueryHandler>();
        services.AddScoped<ReviewQueryHandler>();
        services.AddScoped<UserQueryHandler>();
    }

    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringSection = configuration.GetSection("ConnectionStrings");
        var mongoDbConnectionString = connectionStringSection["MongoDb"] ?? throw new ArgumentException("MongoDB connection string is null");
        var mongoDbDatabaseName = connectionStringSection["MongoDbDatabaseName"] ?? throw new ArgumentException("MongoDB database name is null");
        services.AddSingleton<MongoDbContext>(_ => new MongoDbContext(mongoDbConnectionString, mongoDbDatabaseName));
        
        var redisConnectionString = connectionStringSection["Redis"] ?? throw new ArgumentException("Redis connection string is null");
        services.AddSingleton<RedisContext>(_ => new RedisContext(redisConnectionString));
        
        var postgresConnectionString = connectionStringSection["Postgres"] ?? throw new ArgumentException("Postgres connection string is null");
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseNpgsql(postgresConnectionString);
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        // Read Repositories
        services.AddScoped<IUserReadRepository, UserReadMongoRepository>();
        services.AddScoped<IListingReadRepository, ListingReadMongoRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadMongoRepository>();
        services.AddScoped<IReviewReadRepository, ReviewReadMongoRepository>();
        
        // Write Repositories
        services.AddScoped<IUserWriteRepository, UserWriteEfRepository>();
        services.AddScoped<IListingWriteRepository, ListingWriteEfRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteEfRepository>();
        services.AddScoped<IReviewWriteRepository, ReviewWriteEfRepository>();
        
        // Cache Repositories
        /* TODO: uncomment when Redis is implemented
         services.AddScoped<IPostCacheRepository, RedisPostCacheRepository>(provider =>
       {
           var redisContext = provider.GetService<RedisContext>();
           return new RedisPostCacheRepository(redisContext, TimeSpan.FromSeconds(30));
       });
        */
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IListingService, ListingService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
    }
}