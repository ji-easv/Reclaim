using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.Contexts;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    public DbSet<UserWriteEntity> Users { get; set; }
    public DbSet<ListingWriteEntity> Listings { get; set; }
    public DbSet<OrderWriteEntity> Orders { get; set; }
    public DbSet<ReviewWriteEntity> Reviews { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserWriteEntity>(entity =>
        {
           
        });
        
        modelBuilder.Entity<ListingWriteEntity>(entity =>
        {

        });
        
        modelBuilder.Entity<OrderWriteEntity>(entity =>
        {

        });
        
        modelBuilder.Entity<ReviewWriteEntity>(entity =>
        {

        });
        
        base.OnModelCreating(modelBuilder);
    }
}