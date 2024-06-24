namespace Kayord.Pos.DTO;

public class CashUpUserDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal? ClosingBalance { get; set; }
    public List<CashUpUserItemDTO> CashUpUserItems { get; set; } = default!;
}