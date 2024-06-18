namespace Kayord.Pos.Entities;

public class Payment
{
    public int Id { get; set; }

    public string PaymentReference { get; set; } = string.Empty;
    public int TableBookingId { get; set; }
    public TableBooking TableBooking { get; set; } = default!;
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int? PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; } = default!;
    public DateTime DateReceived { get; set; } = DateTime.UtcNow;
}