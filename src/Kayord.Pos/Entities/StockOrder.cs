namespace Kayord.Pos.Entities;

public class StockOrder : AuditableEntity
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int StockOrderStatusId { get; set; }
    public StockOrderStatus StockOrderStatus { get; set; } = default!;
    public int DivisionId { get; set; }
    public Division Division { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = default!;
    public List<StockOrderItem>? StockOrderItems { get; set; }
}