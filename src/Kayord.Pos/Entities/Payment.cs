using Kayord.Pos.Entities;

namespace Kayord.Pos.Entities;

public class Payment
{
    public int Id { get; set; }

    public string PaymentReference { get; set; } = string.Empty;
    public int TableBookingId { get; set; }
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime DateReceived { get; set; } = DateTime.UtcNow;
}