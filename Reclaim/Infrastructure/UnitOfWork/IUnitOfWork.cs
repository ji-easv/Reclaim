using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Reclaim.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
    Task CommitAsync();
    Task RollbackAsync();
}