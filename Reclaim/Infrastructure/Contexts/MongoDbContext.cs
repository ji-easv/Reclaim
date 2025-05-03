using MongoDB.Driver;
using Reclaim.Domain.DTOs;

namespace Reclaim.Infrastructure.Contexts;

public class MongoDbContext
{
    public MongoDbContext(string connectionString, string databaseName)
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
        Database = new MongoClient(mongoClientSettings).GetDatabase(databaseName);

        Listings = Database.GetCollection<ListingGetDto>("Listings");
        Orders = Database.GetCollection<OrderGetDto>("Orders");
        Users = Database.GetCollection<UserGetDto>("Users");
    }

    private IMongoCollection<ListingGetDto> Listings { get; }
    private IMongoCollection<OrderGetDto> Orders { get; }
    private IMongoCollection<UserGetDto> Users { get; }

    public IMongoDatabase Database { get; }
}