
namespace Kayord.Pos.Entities;
public class Extra
{
    public int ExtraId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int ExtraGroupId { get; set; }
    public ExtraGroup ExtraGroup { get; set; } = default!;
    public List<OrderItemExtra>? OrderItemExtras { get; set; }

}