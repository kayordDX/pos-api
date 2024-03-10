namespace Kayord.Pos.Features.User.GetStatus;
using Kayord.Pos.Entities;
public class Response
{
    public int OutletId { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public SalesPeriod? SalesPeriod { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}