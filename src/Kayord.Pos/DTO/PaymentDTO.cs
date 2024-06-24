namespace Kayord.Pos.DTO;

public class PaymentDTO
{
    public int Id { get; set; }

    public string PaymentReference { get; set; } = string.Empty;
    public int TableBookingId { get; set; }
    public TableBookingDTO TableBooking { get; set; } = default!;
    public decimal Amount { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int? PaymentTypeId { get; set; }
    public PaymentTypeDTO PaymentType { get; set; } = default!;
    public DateTime DateReceived { get; set; } = DateTime.UtcNow;
}