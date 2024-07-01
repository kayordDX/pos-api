namespace Kayord.Pos.Features.CashUp.User.Close;
public class Request
{
    public int OutletId { get; set; }
    public string UserId { get; set; } = string.Empty;
}