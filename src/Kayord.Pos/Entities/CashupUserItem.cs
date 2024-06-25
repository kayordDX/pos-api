namespace Kayord.Pos.Entities;

public class CashUpUserItem
{
    public int Id { get; set; }
    public int CashUpUserId { get; set; }
    public CashUpUser CashUpUser { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int CashUpUserItemTypeId { get; set; }
    public CashUpUserItemType CashUpUserItemType { get; set; } = default!;
    public decimal Value { get; set; }
}