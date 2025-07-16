namespace Kayord.Pos.Features.Stats.PaymentTypes;

public class Response
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal AverageAmount { get; set; }
}