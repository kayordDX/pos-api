
namespace Kayord.Pos.Entities;
public class OrderGroup
{
    public int Id { get; set; }
    public int OrderGroupId { get; set; }
    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = default!;
}