namespace Reclaim.Infrastructure.Repositories.S3;

public interface IObjectStorageRepository
{
    Task<FileUploadResponse> UploadFileAsync(string fileName, Stream fileStream, string contentType);
    Task DeleteFileAsync(Guid objectKey);
    Task<string> GetSignedFileUrlAsync(Guid objectKey);
    Task<bool> DoesFileExistAsync(Guid objectKey);
}