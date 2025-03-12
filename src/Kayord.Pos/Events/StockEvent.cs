namespace Kayord.Pos.Events;

public class StockEvent : IEvent
{
    public List<int> OrderItemIds { get; set; } = new();
}