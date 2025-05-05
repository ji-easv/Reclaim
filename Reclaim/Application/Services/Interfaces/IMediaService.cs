using Reclaim.Application.Commands.Media;
using Reclaim.Application.Queries.Media;
using Reclaim.Domain.DTOs;

namespace Reclaim.Application.Services.Interfaces;

public interface IMediaService
{
    Task<List<MediaGetDto>> CreateMediaAsync(CreateMediaCommand command);
    Task<List<MediaGetDto>> DeleteMediaAsync(DeleteMediaCommand command);
    Task<List<MediaGetDto>> GetMediaForListingAsync(GetMediaForListingQuery query);
    Task<string> GetSignedUrlByObjectKeyAsync(Guid objectKey);
}