using System.ComponentModel.DataAnnotations;
using InventorySystem.Domain.models.sale;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.product.data;

public class ProductData
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field is required")]
    [StringLength(maximumLength: 255, MinimumLength = 2, ErrorMessage = "Max {255}, Min {2}")]
    public string? Name { get; set; }
    
    [Range(0, 150)]
    public int Stock { get; set; }
    
    public bool Enabled { get; set; } = true;
    
    public List<SaleDetail>? SaleDetails { get; set; }
}