namespace Kayord.Pos.Entities;
public class OrderItemOption
{
    public int OrderItemOptionId { get; set; }
    public int OrderItemId { get; set; }
    public int OptionId { get; set; }
    public Option Option { get; set; } = default!;

}