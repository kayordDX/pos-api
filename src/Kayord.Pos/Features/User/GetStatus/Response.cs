namespace Kayord.Pos.Features.User.GetStatus;

public class Response
{
    public int OutletId { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}