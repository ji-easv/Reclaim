using Microsoft.EntityFrameworkCore;
using Minio;
using Reclaim.Application.Commands.Listing;
using Reclaim.Application.Commands.Media;
using Reclaim.Application.Commands.Order;
using Reclaim.Application.Commands.Review;
using Reclaim.Application.Commands.User;
using Reclaim.Application.Queries.Listing;
using Reclaim.Application.Queries.Media;
using Reclaim.Application.Queries.Order;
using Reclaim.Application.Queries.Review;
using Reclaim.Application.Queries.User;
using Reclaim.Application.Services.Implementations;
using Reclaim.Application.Services.Interfaces;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;
using Reclaim.Infrastructure.Repositories.Read.Implementations.Redis;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;
using Reclaim.Infrastructure.Repositories.S3;
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
        services.AddScoped<MediaCommandHandler>();

        // Register all query handlers
        services.AddScoped<ListingQueryHandler>();
        services.AddScoped<OrderQueryHandler>();
        services.AddScoped<ReviewQueryHandler>();
        services.AddScoped<UserQueryHandler>();
        services.AddScoped<MediaQueryHandler>();
    }

    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringSection = configuration.GetSection("ConnectionStrings");
        var mongoDbConnectionString = connectionStringSection["MongoDb"] ??
                                      throw new ArgumentException("MongoDB connection string is null");
        var mongoDbDatabaseName = connectionStringSection["MongoDbDatabaseName"] ??
                                  throw new ArgumentException("MongoDB database name is null");
        services.AddSingleton<MongoDbContext>(_ => new MongoDbContext(mongoDbConnectionString, mongoDbDatabaseName));

        var redisConnectionString = connectionStringSection["Redis"] ??
                                    throw new ArgumentException("Redis connection string is null");
        services.AddSingleton<RedisContext>(_ => new RedisContext(redisConnectionString));

        var postgresConnectionString = connectionStringSection["Postgres"] ??
                                       throw new ArgumentException("Postgres connection string is null");
        services.AddDbContext<PostgresDbContext>(options => { options.UseNpgsql(postgresConnectionString); });

        services.AddSingleton<MinIoContext>(_ =>
        {
            var minIoConfig = configuration.GetSection("MinIo");
            var minIoClient = new MinioClient()
                .WithEndpoint(minIoConfig["Endpoint"] ?? throw new ArgumentException("MinIO endpoint is null"))
                .WithCredentials(
                    minIoConfig["AccessKey"] ?? throw new ArgumentException("MinIO access key is null"),
                    minIoConfig["SecretKey"] ?? throw new ArgumentException("MinIO secret key is null"))
                .Build();

            var minIoContext = new MinIoContext(minIoClient);
            minIoContext.InitializeAsync();
            return minIoContext;
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        // Read Repositories
        services.AddScoped<IUserReadRepository, UserReadMongoRepository>();
        services.AddScoped<IReviewReadRepository, ReviewReadMongoRepository>();

        // Use the decorator pattern for wrapping the read repository with Redis caching
        services.AddScoped<ListingReadMongoRepository>();
        services.AddScoped<IListingReadRepository>(provider =>
        {
            var mongoRepository = provider.GetService<ListingReadMongoRepository>()
                                  ?? throw new ArgumentNullException(nameof(ListingReadMongoRepository),
                                      "Mongo repository is null");

            var redisContext = provider.GetService<RedisContext>()
                               ?? throw new ArgumentNullException(nameof(RedisContext), "Redis context is null");

            return new ListingReadRedisRepository(redisContext, TimeSpan.FromSeconds(30), mongoRepository);
        });

        services.AddScoped<OrderReadMongoRepository>();
        services.AddScoped<IOrderReadRepository>(provider =>
        {
            var mongoRepository = provider.GetService<OrderReadMongoRepository>()
                                  ?? throw new ArgumentNullException(nameof(ListingReadMongoRepository),
                                      "Mongo repository is null");

            var redisContext = provider.GetService<RedisContext>()
                               ?? throw new ArgumentNullException(nameof(RedisContext), "Redis context is null");

            return new OrderReadRedisRepository(redisContext, TimeSpan.FromSeconds(30), mongoRepository);
        });
        
        services.AddScoped<IMediaWriteRepository, MediaWriteEfRepository>();
        services.AddScoped<IMediaReadRepository, MediaReadMongoRepository>();

        // Write Repositories
        services.AddScoped<IUserWriteRepository, UserWriteEfRepository>();
        services.AddScoped<IListingWriteRepository, ListingWriteEfRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteEfRepository>();
        services.AddScoped<IReviewWriteRepository, ReviewWriteEfRepository>();

        // S3 Repositories
        services.AddScoped<IObjectStorageRepository, MinIoObjectStorageRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IListingService, ListingService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IMediaService, MediaService>();
    }
}