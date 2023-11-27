using FluentValidation;

namespace Kayord.Pos.Features.TableOrder.Create
{
    public class Request
    {
        public int TableBookingId { get; set; }
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.TableBookingId).GreaterThan(0).WithMessage("TableId must be greater than 0");
        }
    }
}