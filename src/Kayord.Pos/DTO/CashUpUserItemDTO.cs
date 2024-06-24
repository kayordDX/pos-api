using Kayord.Pos.Entities;

namespace Kayord.Pos.DTO;

public class CashUpUserItemDTO
{
    public int Id { get; set; }
    public int UserCashUpId { get; set; }
    public CashUpUserDTO CashUpUser { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int CashUpUserItemTypeId { get; set; }
    public CashUpUserItemTypeDTO CashUpUserItemType { get; set; } = default!;
    public decimal Value { get; set; }
}