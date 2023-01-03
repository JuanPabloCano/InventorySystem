using System.ComponentModel.DataAnnotations;
using InventorySystem.Domain.models.sale;

namespace InventorySystem.Infrastructure.DrivenAdapters.sqlServer.sale.data;

public class SaleData
{
    public Guid SaleId { get; set; }
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "The field is required")]
    [StringLength(maximumLength: 3, MinimumLength = 2,
        ErrorMessage = "The field must contain 2 or 3 characters {CC, TI, CE, NIT, NIP")]
    public string? IdType { get; set; }


    [Required(ErrorMessage = "The field is required")]
    [StringLength(10, ErrorMessage = "The field must contain 10 digits")]
    public string? ClientId { get; set; }


    [Required(ErrorMessage = "The field is required")]
    [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "The field must contain at least 3 characters")]
    public string? ClientName { get; set; }


    public List<SaleDetail>? SaleDetails { get; set; }
}