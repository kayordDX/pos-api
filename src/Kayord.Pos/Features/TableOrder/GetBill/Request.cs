using FluentValidation;

namespace Kayord.Pos.Features.TableOrder.GetBill
{
    public class Request
    {
        public int TableBookingId { get; set; } = default!;
    }


}