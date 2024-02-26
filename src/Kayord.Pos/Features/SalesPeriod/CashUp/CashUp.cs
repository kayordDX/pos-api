using Kayord.Pos.DTO;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.SalesPeriod.CashUp;

public class CashUp : Entities.CashUp
{
    public List<UserCashUp> UserCashUps { get; set; } = new List<UserCashUp>();

}