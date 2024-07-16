namespace Kayord.Pos.Entities;

public class InventoryStock
{
    public int InventoryId { get; set; }
    public Inventory Inventory { get; set; } = default!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public decimal Quantity { get; set; }
}