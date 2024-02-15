using Kayord.Pos.Entities;

namespace Kayord.Pos.DTO;
public class OrderItemExtraDTO
{
    public int OrderItemExtraId { get; set; }
    public int OrderItemId { get; set; }
    public int ExtraId { get; set; }
    public ExtraDTO Extra { get; set; } = default!;

}