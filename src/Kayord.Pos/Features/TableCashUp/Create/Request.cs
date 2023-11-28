using FluentValidation;

namespace Kayord.Pos.Features.TableCashUp.Create
{
    public class Request
    {
        public int TableBookingId { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int OutletId { get; set; }
        
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.TableBookingId).GreaterThan(0).WithMessage("TableBookingId must be greater than 0");
            RuleFor(v => v.SalesAmount).GreaterThan(0).WithMessage("Sales Amount must be greater than 0");
            RuleFor(v => v.TotalAmount).GreaterThan(0).WithMessage("Total Amount must be greater than 0");
        }
    }
}