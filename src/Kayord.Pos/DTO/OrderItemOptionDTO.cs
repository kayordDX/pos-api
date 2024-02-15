namespace Kayord.Pos.DTO;
public class OrderItemOptionDTO
{
    public int OrderItemOptionId { get; set; }
    public int OrderItemId { get; set; }
    public int OptionId { get; set; }
    public OptionDTO Option { get; set; } = default!;


}