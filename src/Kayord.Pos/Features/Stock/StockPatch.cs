namespace Kayord.Pos.Features.Stock;

public class StockPatch
{
    public int StockId { get; set; }
    public decimal Quantity { get; set; }
    public StockItemAuditType Type { get; set; }
}

public enum StockItemAuditType
{
    MenuItem = 1,
    Extra,
    Option,
    Bulk = 7
}