using InventorySystem.Domain.models.sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.config;

public class SaleConfig : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(key => key.SaleId);
        builder
            .HasMany(sale => sale.SaleDetails)
            .WithOne(detail => detail.Sale);
    }
}