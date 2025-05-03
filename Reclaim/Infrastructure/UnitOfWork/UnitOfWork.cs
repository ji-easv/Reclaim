using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Reclaim.Infrastructure.Contexts;

namespace Reclaim.Infrastructure.UnitOfWork;

public class UnitOfWork(PostgresDbContext context) : IUnitOfWork, IDisposable
{
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel)
    {
        var transaction = await context.Database.BeginTransactionAsync(isolationLevel);
        return transaction;
    }

    public async Task CommitAsync()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await context.Database.RollbackTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}