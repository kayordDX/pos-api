namespace Kayord.Pos.Features.Stats.PaymentTypes;

public class Request
{
    public int SalesPeriodId { get; set; }
    public int Top { get; set; } = 5;
}