using FluentValidation;

namespace Kayord.Pos.Features.Order.SendToKitchen
{
    public class Request
    {
        public int TableBookingId { get; set; } = default!;

    }


}