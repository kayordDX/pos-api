
namespace Kayord.Pos.Entities;
public class OrderGroup
{
    public int OrderGroupId { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}