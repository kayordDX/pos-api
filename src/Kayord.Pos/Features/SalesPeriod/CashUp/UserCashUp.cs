
using Kayord.Pos.DTO;
namespace Kayord.Pos.Features.SalesPeriod.CashUp;

public class UserCashUp
{
    public List<TableCashUp> TableCashUps { get; set; } = new List<TableCashUp>();
    public string UserId { get; set; } = string.Empty;
    public UserDTO User { get; set; } = default!;

    public decimal UserTotal { get; set; } = 0;
    public decimal UserBalance { get; set; } = 0;
    public decimal UserPaymentTotal { get; set; } = 0;
    public TimeOnly TableTurnaroundTime { get; set; }


}