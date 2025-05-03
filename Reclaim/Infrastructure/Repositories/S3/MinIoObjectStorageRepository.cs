using Minio.DataModel.Args;
using Minio.Exceptions;
using Reclaim.Infrastructure.Contexts;

namespace Reclaim.Infrastructure.Repositories.S3;

public class MinIoObjectStorageRepository(MinIoContext context) : IObjectStorageRepository
{
    public async Task<FileUploadResponse> UploadFileAsync(string fileName, Stream fileStream, string contentType)
    {
        var objectKey = Guid.NewGuid();
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(MinIoContext.ListingBucketName)
            .WithObject(objectKey.ToString())
            .WithObjectSize(fileStream.Length)
            .WithStreamData(fileStream)
            .WithContentType(contentType);

        var response = await context.MinioClient.PutObjectAsync(putObjectArgs);

        return new FileUploadResponse
        {
            FileName = fileName,
            Size = response.Size,
            ObjectKey = objectKey
        };
    }

    public async Task DeleteFileAsync(Guid objectKey)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(MinIoContext.ListingBucketName)
            .WithObject(objectKey.ToString());

        await context.MinioClient.RemoveObjectAsync(removeObjectArgs);
    }

    public async Task<string> GetSignedFileUrlAsync(Guid objectKey)
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(MinIoContext.ListingBucketName)
            .WithObject(objectKey.ToString())
            .WithExpiry(3600); // URL valid for 1 hour

        return await context.MinioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
    }

    public async Task<bool> DoesFileExistAsync(Guid objectKey)
    {
        var statObjectArgs = new StatObjectArgs()
            .WithBucket(MinIoContext.ListingBucketName)
            .WithObject(objectKey.ToString());

        try
        {
            await context.MinioClient.StatObjectAsync(statObjectArgs);
            return true;
        }
        catch (MinioException)
        {
            return false;
        }
    }
}