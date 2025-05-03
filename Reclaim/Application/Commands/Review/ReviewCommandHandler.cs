using Reclaim.Domain.Entities.Write;
using Reclaim.Infrastructure.EventBus.EventBus;

namespace Reclaim.Application.Commands.Review;

public class ReviewCommandHandler(IDomainEventBus eventBus) : ICommandHandler<CreateReviewCommand, ReviewWriteEntity>,
    ICommandHandler<UpdateReviewCommand, ReviewWriteEntity>,
    ICommandHandler<DeleteReviewCommand, ReviewWriteEntity>
{
    public Task<ReviewWriteEntity> HandleAsync(CreateReviewCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewWriteEntity> HandleAsync(DeleteReviewCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewWriteEntity> HandleAsync(UpdateReviewCommand command)
    {
        throw new NotImplementedException();
    }
}