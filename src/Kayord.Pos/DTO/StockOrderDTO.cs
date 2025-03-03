using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class StockOrderDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int StockOrderStatusId { get; set; }
    public StockOrderStatusDTO StockOrderStatus { get; set; } = default!;
    public int DivisionId { get; set; }
    public DivisionDTO Division { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public int SupplierId { get; set; }
    public SupplierDTO Supplier { get; set; } = default!;
    public List<StockOrderItemDTO>? StockOrderItems { get; set; }
}