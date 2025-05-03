using Microsoft.EntityFrameworkCore;
using Reclaim.Domain.Entities.Write;

namespace Reclaim.Infrastructure.Contexts;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    public DbSet<UserWriteEntity> Users { get; set; }
    public DbSet<ListingWriteEntity> Listings { get; set; }
    public DbSet<OrderWriteEntity> Orders { get; set; }
    public DbSet<ReviewWriteEntity> Reviews { get; set; }
    public DbSet<MediaWriteEntity> Media { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserWriteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<ListingWriteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderWriteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne<UserWriteEntity>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(o => o.Listings)
                .WithOne()
                .HasForeignKey(e => e.Id)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ReviewWriteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Seller)
                .WithMany()
                .HasForeignKey(e => e.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<MediaWriteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Listing)
                .WithMany()
                .HasForeignKey(e => e.ListingId);
        });

        base.OnModelCreating(modelBuilder);
    }
}