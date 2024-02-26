using FluentValidation;

namespace Kayord.Pos.Features.SalesPeriod.CreateCashUp;
public class Request
{
    public decimal CashUpTotal { get; set; } = 0;
    public int TableCount { get; set; } = 0;
    public decimal CashUpBalance { get; set; } = 0;
    public decimal CashUpTotalPayments { get; set; } = 0;
    public int SalesPeriodId { get; set; } = 0;
    public string UserId { get; set; } = string.Empty;
    public string SignOffUserId { get; set; } = string.Empty;
    public DateTime? SignOffDate { get; set; }


}