using System.Text.Json.Serialization;
using InventorySystem.Domain.models.sale;

namespace InventorySystem.Domain.models.product;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Stock { get; set; }
    public bool Enabled { get; set; }
    [JsonIgnore] public virtual List<SaleDetail>? SaleDetails { get; set; }
}