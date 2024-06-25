namespace Kayord.Pos.Features.CashUp.User.Create;
public class Request
{
    public int CashUpUserId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int CashUpUserItemTypeId { get; set; }
    public decimal Value { get; set; }

}