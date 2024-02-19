
namespace Kayord.Pos.Entities;
public class Extra
{
    public int ExtraId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public List<OrderItemExtra>? OrderItemExtras { get; set; }
}