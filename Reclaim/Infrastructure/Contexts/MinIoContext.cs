using Minio;
using Minio.DataModel.Args;

namespace Reclaim.Infrastructure.Contexts;

public class MinIoContext(IMinioClient minIoClient)
{
    public const string ListingBucketName = "listing-media";
    public IMinioClient MinioClient { get; } = minIoClient;

    private async Task<bool> BucketExistsAsync(string bucketName)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
        return await MinioClient.BucketExistsAsync(bucketExistsArgs);
    }
}