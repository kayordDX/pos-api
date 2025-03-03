namespace Kayord.Pos.Entities;

public class StockItemAudit
{
    public long Id { get; set; }
    public int StockItemId { get; set; }
    public DateTime Updated { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal FromActual { get; set; }
    public decimal ToActual { get; set; }
    public int StockItemAuditTypeId { get; set; }
    public StockItemAuditType StockItemAuditType { get; set; } = default!;
    public int? OrderItemId { get; set; }
}