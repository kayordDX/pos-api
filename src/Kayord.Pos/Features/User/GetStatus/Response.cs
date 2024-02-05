namespace Kayord.Pos.Features.User.GetStatus;
using Kayord.Pos.Entities;
public class Response
{
    public int OutletId { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public SalesPeriod SalesPeriod { get; set; } = default!;
}