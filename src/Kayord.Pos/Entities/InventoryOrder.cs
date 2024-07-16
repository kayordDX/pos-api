namespace Kayord.Pos.Entities;

public class InventoryOrder
{
    public int Id { get; set; }
    public string GRVCode { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public int InventoryId { get; set; }
    public Inventory Inventory { get; set; } = default!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public decimal Quantity { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; } = default!;
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime ReceivedDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public string SupplierDeliveredName { get; set; } = string.Empty;
}