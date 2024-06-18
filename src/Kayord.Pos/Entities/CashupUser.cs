namespace Kayord.Pos.Entities;

public class CashupUser
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public List<CashupUserItem> CashupUserItems { get; set; } = default!;

}