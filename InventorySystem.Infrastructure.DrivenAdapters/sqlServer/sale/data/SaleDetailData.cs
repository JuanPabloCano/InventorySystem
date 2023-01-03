using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;

public class SaleDetailData
{
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }
    public int Amount { get; set; }
    public ProductData? ProductData { get; set; }
    public SaleData? SaleData { get; set; }
}