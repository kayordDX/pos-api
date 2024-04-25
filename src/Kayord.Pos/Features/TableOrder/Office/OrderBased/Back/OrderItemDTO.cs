using Humanizer;
using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;
public class OrderItemDTO
{
    public int OrderItemId { get; set; }
    public int? OrderGroupId { get; set; }
    public TableBookingDTO? TableBooking { get; set; }
    public int TableBookingId { get; set; }
    public MenuItemDTO MenuItem { get; set; } = default!;
    public int DivisionId { get; set; }
    public string? Note { get; set; }
    public DateTime OrderReceived { get; set; } = DateTime.Now;
    public DateTime OrderUpdated { get; set; } = DateTime.Now;

    public string OrderReceivedFormatted
    {
        get => OrderReceived.Humanize();
    }
    public string OrderUpdatedFormatted
    {
        get => OrderUpdated.Humanize();
    }
    public int OrderItemStatusId { get; set; }
    public int Priority { get; set; }
    public OrderItemStatusDTO OrderItemStatus { get; set; } = default!;
    public List<OrderItemOptionDTO>? OrderItemOptions { get; set; }
    public List<OrderItemExtraDTO>? OrderItemExtras { get; set; }
}