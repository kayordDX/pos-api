using FluentValidation;

namespace Kayord.Pos.Features.SalesPeriod.CashUp
{
    public class Request
    {
        public int SalesPeriodId { get; set; } = default!;
        public string UserId { get; set; } = string.Empty;

    }


}