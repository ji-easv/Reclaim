namespace Reclaim.Application.Queries.Media;

public class GetSignedUrlByObjectKeyQuery : IQuery<string>
{
    public required Guid ObjectKey { get; set; }
}