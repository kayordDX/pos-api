namespace Kayord.Pos.Entities;

public class CashupUserItem
{
    public int Id { get; set; }
    public int UserCashupId { get; set; }
    public CashupUser UserCashup { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public int CashupItemTypeId { get; set; }
    public CashupUserItemType CashupItemTypes { get; set; } = default!;

}