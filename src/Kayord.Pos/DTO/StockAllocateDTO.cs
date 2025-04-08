using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class StockAllocateDTO : StockAllocateDTOBasic
{
    public List<StockAllocateItemDTO>? StockAllocateItems { get; set; }
}