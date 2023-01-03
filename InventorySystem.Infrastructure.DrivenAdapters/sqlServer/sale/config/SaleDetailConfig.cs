using InventorySystem.Domain.models.sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.config;

public class SaleDetailConfig : IEntityTypeConfiguration<SaleDetail>
{
    public void Configure(EntityTypeBuilder<SaleDetail> builder)
    {
        builder.ToTable("SaleDetail");
        builder.HasKey(key => new { key.ProductId, key.SaleId });

        builder.
            HasOne(detail => detail.Product)
            .WithMany(product => product.SaleDetails);
        
        builder.
            HasOne(detail => detail.Sale)
            .WithMany(sale => sale.SaleDetails);
    }
}