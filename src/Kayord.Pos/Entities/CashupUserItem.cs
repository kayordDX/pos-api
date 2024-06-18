namespace Kayord.Pos.Entities;

public class CashUpUserItem
{
    public int Id { get; set; }
    public int UserCashUpId { get; set; }
    public CashUpUser CashUpUser { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public int CashUpItemTypeId { get; set; }
    public CashUpUserItemType CashUpItemTypes { get; set; } = default!;
}