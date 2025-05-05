using MongoDB.Bson;
using MongoDB.Driver;
using Reclaim.Domain.Entities.Read;
using Reclaim.Infrastructure.Contexts;
using Reclaim.Infrastructure.Repositories.Read.Interfaces;

namespace Reclaim.Infrastructure.Repositories.Read.Implementations.MongoDb;

public class MediaReadMongoRepository(MongoDbContext dbContext) : IMediaReadRepository
{
    public async Task<List<MediaReadEntity>> AddRangeAsync(List<MediaReadEntity> mediaReadEntities)
    {
        var listing = await dbContext.Listings.Find(x => x.Id == mediaReadEntities.First().ListingId).FirstAsync();
        listing.Media = mediaReadEntities;
        await dbContext.Listings.ReplaceOneAsync(x => x.Id == listing.Id, listing);
        return mediaReadEntities;
    }

    public async Task<List<MediaReadEntity>> DeleteRangeAsync(string listingId, List<string> mediaIds)
    {
        var listing = await dbContext.Listings.Find(x => x.Id == ObjectId.Parse(listingId)).FirstAsync();
        listing.Media = listing.Media.Where(x => !mediaIds.Contains(x.Id.ToString())).ToList();
        await dbContext.Listings.ReplaceOneAsync(x => x.Id == listing.Id, listing);
        return listing.Media;
    }

    public async Task<List<MediaReadEntity>> GetMediaForListingAsync(string listingId)
    {
        var media = await dbContext.Listings
            .Find(x => x.Id == ObjectId.Parse(listingId))
            .Project(x => x.Media)
            .FirstOrDefaultAsync();

        return media ?? [];
    }
}