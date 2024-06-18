

namespace Kayord.Pos.Features.CashUp.User.Get;

public class Response
{
    public string UserId { get; set; } = default!;
    public Entities.User User { get; set; } = default!;
    public decimal Sales { get; set; }

}