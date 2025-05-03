namespace Reclaim.Infrastructure.Repositories.S3;

public class FileUploadResponse
{
    public required string FileName { get; set; }
    public long Size { get; set; }
    public Guid ObjectKey { get; set; }
}