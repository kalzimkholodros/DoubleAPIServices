using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.UnitPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }
} 