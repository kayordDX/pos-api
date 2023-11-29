using FluentValidation;

namespace Kayord.Pos.Features.Clock.ClockIn;

public class Request
{
    public int StaffId { get; set; }
    public int OutletId { get; set; }
}

