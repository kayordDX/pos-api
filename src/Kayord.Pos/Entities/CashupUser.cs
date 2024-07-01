namespace Kayord.Pos.Entities;

public class CashUpUser
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal? ClosingBalance { get; set; }
    public string CompleterUserId { get; set; } = string.Empty;
    public List<CashUpUserItem> CashUpUserItems { get; set; } = default!;
}