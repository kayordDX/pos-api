namespace Kayord.Pos.Events;

public class PaymentCompletedEvent
{
    public string PaymentReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
}