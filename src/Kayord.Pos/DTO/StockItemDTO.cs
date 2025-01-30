using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class StockItemDTO
{
    public DivisionDTO Division { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}