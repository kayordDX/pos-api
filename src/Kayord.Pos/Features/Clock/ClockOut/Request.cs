using FluentValidation;

namespace Kayord.Pos.Features.Clock.ClockOut;

public class Request
{
    public int StaffId { get; set; }
    public int SalesPeriodId { get; set; }
}

