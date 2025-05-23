﻿using System.Data;
using Reclaim.Domain.Entities.Write;
using Reclaim.Domain.Exceptions;
using Reclaim.Infrastructure.Repositories.Write.Interfaces;
using Reclaim.Infrastructure.UnitOfWork;

namespace Reclaim.Application.Commands.User;

public class UserCommandHandler(
    IUserWriteRepository userWriteRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<CreateUserCommand, UserWriteEntity>,
        ICommandHandler<UpdateUserCommand, UserWriteEntity>,
        ICommandHandler<DeleteUserCommand, UserWriteEntity>
{
    public async Task<UserWriteEntity> HandleAsync(CreateUserCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var existingUser = await userWriteRepository.GetByEmailAsync(command.Email);
            if (existingUser is not null)
            {
                throw new InsertionConflictException($"User with email {command.Email} already exists.");
            }

            var user = new UserWriteEntity
            {
                Name = command.Name,
                Email = command.Email,
                IsDeleted = false
            };

            var createdUser = await userWriteRepository.AddAsync(user);
            await unitOfWork.CommitAsync();
            return createdUser;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<UserWriteEntity> HandleAsync(DeleteUserCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var user = await userWriteRepository.GetByIdAsync(command.UserId);
            if (user is null)
            {
                throw new NotFoundException($"User with ID {command.UserId} not found.");
            }

            var deletedEntity = await userWriteRepository.DeleteAsync(user);
            await unitOfWork.CommitAsync();
            return deletedEntity;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<UserWriteEntity> HandleAsync(UpdateUserCommand command)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var user = await userWriteRepository.GetByIdAsync(command.UserId);
            if (user is null)
            {
                throw new NotFoundException($"User with ID {command.UserId} not found.");
            }

            user.Name = command.Name;
            user.UpdatedAt = DateTimeOffset.UtcNow;

            var result = await userWriteRepository.UpdateAsync(user);
            await unitOfWork.CommitAsync();
            return result;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}