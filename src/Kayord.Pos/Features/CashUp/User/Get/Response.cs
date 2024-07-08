

namespace Kayord.Pos.Features.CashUp.User.Get;

public class Response
{
    public List<Items> Items { get; set; } = new List<Items>();
    public decimal TotalSales { get; set; }
    public decimal TotalTips { get; set; }
    public decimal TotalPayments { get; set; }



}

public class Items
{
    public string UserId { get; set; } = default!;
    public Entities.User User { get; set; } = default!;
    public decimal Sales { get; set; }
    public decimal Tips { get; set; }
    public decimal Payments { get; set; }
    public int OpenTableCount { get; set; } = 0;

}