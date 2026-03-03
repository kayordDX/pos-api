namespace Kayord.Pos.Events;

public class StockPeriodSnapshotEvent : IEvent
{
    public int SalesPeriodId { get; set; }
    public int OutletId { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
