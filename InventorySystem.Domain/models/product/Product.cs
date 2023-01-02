namespace InventorySystem.Domain.models.product;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public bool Enabled { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
}