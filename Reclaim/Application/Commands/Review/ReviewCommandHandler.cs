using System.ComponentModel.DataAnnotations;
using System.Data;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.Review;

public class ReviewCommandHandler(
    IReviewWriteRepository reviewWriteRepository,
    IUserWriteRepository userWriteRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReviewCommand, ReviewWriteEntity>,
        ICommandHandler<UpdateReviewCommand, ReviewWriteEntity>,
        ICommandHandler<DeleteReviewCommand, ReviewWriteEntity>
{
    public async Task<ReviewWriteEntity> HandleAsync(CreateReviewCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            if (command.AuthorId == command.SellerId)
            {
                throw new CustomValidationException("Author and seller cannot be the same.");
            }

            // Validate users exist
            var author = await userWriteRepository.GetByIdAsync(command.AuthorId);
            if (author is null)
            {
                throw new NotFoundException($"Author with ID {command.AuthorId} not found.");
            }

            var seller = await userWriteRepository.GetByIdAsync(command.SellerId);
            if (seller is null)
            {
                throw new NotFoundException($"Seller with ID {command.SellerId} not found.");
            }

            // Validate rating range
            if (command.Rating is < 1 or > 5)
            {
                throw new CustomValidationException($"Rating must be between 1 and 5.");
            }

            // Create the review entity
            var review = new ReviewWriteEntity
            {
                Content = command.Content,
                Rating = command.Rating,
                AuthorId = command.AuthorId,
                SellerId = command.SellerId,
                IsDeleted = false
            };

            // Add to database
            var createdReview = await reviewWriteRepository.AddAsync(review);
            await unitOfWork.CommitAsync();

            return createdReview;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<ReviewWriteEntity> HandleAsync(DeleteReviewCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        
        try
        {
            // Get the review
            var review = await reviewWriteRepository.GetByIdAsync(command.ReviewId);
            if (review is null)
            {
                throw new NotFoundException($"Review with ID {command.ReviewId} not found.");
            }

            // Delete the review
            var deletedReview = await reviewWriteRepository.DeleteAsync(review);
            await unitOfWork.CommitAsync();

            return deletedReview;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<ReviewWriteEntity> HandleAsync(UpdateReviewCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            // Get the review
            var review = await reviewWriteRepository.GetByIdAsync(command.ReviewId);
            if (review is null)
            {
                throw new NotFoundException($"Review with ID {command.ReviewId} not found.");
            }

            // Validate rating range
            if (command.Rating is < 1 or > 5)
            {
                throw new CustomValidationException($"Rating must be between 1 and 5.");
            }

            // Update the review
            review.Content = command.Content;
            review.Rating = command.Rating;
            review.UpdatedAt = DateTimeOffset.UtcNow;

            // Save changes
            var updatedReview = await reviewWriteRepository.UpdateAsync(review);
            await unitOfWork.CommitAsync();

            return updatedReview;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}