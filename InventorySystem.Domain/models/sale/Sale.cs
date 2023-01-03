namespace InventorySystem.Domain.models.sale;

public class Sale
{
    public Guid SaleId { get; set; }
    public DateTime Date { get; set; }
    public string? IdType { get; set; }
    public string? ClientId { get; set; }
    public string? ClientName { get; set; }
    public List<SaleDetail>? SaleDetails { get; set; }
}