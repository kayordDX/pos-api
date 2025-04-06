using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class StockAllocateItemDTO
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public StockDTO Stock { get; set; } = default!;
    public int DivisionId { get; set; }
    public DivisionDTO Division { get; set; } = default!;
    public decimal AllocateAmount { get; set; }
    public decimal Actual { get; set; }
    public int StockAllocateItemStatusId { get; set; }
    public StockAllocateItemStatusDTO StockAllocateItemStatus { get; set; } = default!;
    public DateTime Completed { get; set; }
}