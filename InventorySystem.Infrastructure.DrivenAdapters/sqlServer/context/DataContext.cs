using InventorySystem.Domain.models.product;
using InventorySystem.Domain.models.sale;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.config;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.config;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;

public class DataContext : DbContext
{
    private const string DbUri =
        "Server=localhost;Database=InventorySystem;Trusted_Connection=True;TrustServerCertificate=True";

    public DbSet<Product>? Products { get; set; }

    public DbSet<Sale>? Sales { get; set; }

    public DbSet<SaleDetail>? SaleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DbUri);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new SaleConfig());
        modelBuilder.ApplyConfiguration(new SaleDetailConfig());
    }
}