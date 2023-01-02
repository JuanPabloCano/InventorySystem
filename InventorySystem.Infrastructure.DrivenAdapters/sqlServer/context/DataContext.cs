using InventorySystem.Domain.models.product;
using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.config;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;

public class DataContext : DbContext
{
    private const string DbUri =
        "Server=localhost;Database=InventorySystem;Trusted_Connection=True;TrustServerCertificate=True";

    public DbSet<Product> _products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DbUri);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfig());
    }
}