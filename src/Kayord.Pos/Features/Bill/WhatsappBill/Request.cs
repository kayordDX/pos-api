

namespace Kayord.Pos.Features.Bill.WhatsappBill;

public class Request
{
    public int TableBookingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CountryCode { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}