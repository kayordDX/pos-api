using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.CashUp.User.Detail;

public class Response
{
    public string UserId { get; set; } = string.Empty;
    public Entities.User User { get; set; } = default!;
    public int CashUpUserId { get; set; }
    public List<CashUpUserItemDTO> CashUpUserItems { get; set; } = default!;

}

public class PaymentTotal
{
    public int PaymentTypeId { get; set; }
    public PaymentTypeDTO PaymentType { get; set; } = default!;
    public decimal Total { get; set; }
    public decimal Tip { get; set; }
    public decimal Levy { get; set; }

}

