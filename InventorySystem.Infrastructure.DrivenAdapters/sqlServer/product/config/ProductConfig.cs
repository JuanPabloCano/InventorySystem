using InventorySystem.Domain.models.product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(product => product.Id);
        builder
            .HasMany(product => product.SaleDetails)
            .WithOne(detail => detail.Product);
    }
}