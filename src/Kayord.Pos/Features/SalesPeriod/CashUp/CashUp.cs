using Kayord.Pos.DTO;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.SalesPeriod.CashUp;

public class CashUp
{
    public List<UserCashUp> UserCashUps { get; set; } = new List<UserCashUp>();
    public decimal CashUpTotal { get; set; } = 0;
    public decimal CashUpBalance { get; set; } = 0;
    public decimal CashUpTotalPayments { get; set; } = 0;
    public int SalesPeriodId { get; set; } = 0;
    public Entities.SalesPeriod SalesPeriod { get; set; } = default!;


}