using FluentValidation;

namespace Kayord.Pos.Features.TableOrder.GetBasket;

public class Request
{
    public int TableBookingId { get; set; } = default!;
}