using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;

namespace Reclaim.Infrastructure.Contexts;

public class MongoDbContext
{
    public IMongoCollection<ListingReadEntity> Listings { get; }
    public IMongoCollection<OrderReadEntity> Orders { get; }
    public IMongoCollection<UserReadEntity> Users { get; }
    public IMongoCollection<ReviewReadEntity> Reviews { get; }

    public IMongoDatabase Database { get; }
    
    public MongoDbContext(string connectionString, string databaseName)
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
        Database = new MongoClient(mongoClientSettings).GetDatabase(databaseName);

        Listings = Database.GetCollection<ListingReadEntity>("Listings");
        Orders = Database.GetCollection<OrderReadEntity>("Orders");
        Users = Database.GetCollection<UserReadEntity>("Users");
        Reviews = Database.GetCollection<ReviewReadEntity>("Reviews");
    }
}