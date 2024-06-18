namespace Kayord.Pos.Features.CashUp.User.List;
public class Request
{
    public int OutletId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int SalesPeriodId { get; set; } = default!;


}