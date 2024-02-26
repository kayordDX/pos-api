using Kayord.Pos.DTO;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Entities;

public class CashUp
{
    public int Id { get; set; }
    public decimal CashUpTotal { get; set; } = 0;
    public int TableCount { get; set; } = 0;
    public decimal CashUpBalance { get; set; } = 0;
    public decimal CashUpTotalPayments { get; set; } = 0;
    public int SalesPeriodId { get; set; } = 0;
    public Entities.SalesPeriod SalesPeriod { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public string SignOffUserId { get; set; } = string.Empty;
    public DateTime? SignOffDate { get; set; }



}