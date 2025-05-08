using Minio;
using Minio.DataModel.Args;

namespace Reclaim.Infrastructure.Contexts;

public class MinIoContext(IMinioClient minIoClient)
{
    public const string ListingBucketName = "listing-media";
    public IMinioClient MinioClient { get; } = minIoClient;

    public async Task InitializeAsync()
    {
        await CreateBucketIfNotExistsAsync(ListingBucketName);
    }
    
    private async Task<bool> BucketExistsAsync(string bucketName)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
        return await MinioClient.BucketExistsAsync(bucketExistsArgs);
    }
    
    private async Task<bool> CreateBucketIfNotExistsAsync(string bucketName)
    {
        if (await BucketExistsAsync(bucketName)) return true;

        var makeBucketArgs = new MakeBucketArgs()
            .WithBucket(bucketName);
        await MinioClient.MakeBucketAsync(makeBucketArgs);
        return true;
    }
}