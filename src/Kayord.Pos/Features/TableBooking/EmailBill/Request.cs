

namespace Kayord.Pos.Features.TableBooking.EmailBill
{
    public class Request
    {
        public int TableBookingId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}