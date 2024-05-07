namespace Kayord.Pos.Features.Order.AddItems;

public class Request
{
    public List<Order> Orders { get; set; } = default!;
    public int TableBookingId { get; set; } = default!;

}