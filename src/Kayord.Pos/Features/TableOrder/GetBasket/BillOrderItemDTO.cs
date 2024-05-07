using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.TableOrder.GetBasket;
public class BillOrderItemDTO
{
    public int OrderItemId { get; set; }
    public int TableBookingId { get; set; }
    public TableBookingDTO TableBooking { get; set; } = default!;
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public BillMenuItemDTO MenuItem { get; set; } = default!;
    public List<OrderItemOptionDTO>? OrderItemOptions { get; set; }
    public List<OrderItemExtraDTO>? OrderItemExtras { get; set; }
    public string? Note { get; set; }
}
