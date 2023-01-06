using System.Text.Json.Serialization;
using InventorySystem.Domain.models.product;

namespace InventorySystem.Domain.models.sale;

public class SaleDetail
{
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }
    public int Amount { get; set; }
    public Product? Product { get; set; }
    [JsonIgnore] public Sale? Sale { get; set; }
}