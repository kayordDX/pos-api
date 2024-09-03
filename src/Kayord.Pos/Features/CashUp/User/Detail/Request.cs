namespace Kayord.Pos.Features.CashUp.User.Detail;
public class Request
{
    public int OutletId { get; set; }
    public int CashUpUserId { get; set; }
    public string UserId { get; set; } = string.Empty;
}