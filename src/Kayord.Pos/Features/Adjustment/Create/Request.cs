namespace Kayord.Pos.Features.Adjustment.Create;

public class Request
{
    public int TableBookingId { get; set; }
    public int AdjustmentTypeId { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}


