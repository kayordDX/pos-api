namespace Kayord.Pos.Entities;

public class MenuItemStock
{
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public int StockItemId { get; set; }
    public StockItem StockItem { get; set; } = default!;
    public decimal Amount { get; set; }
}