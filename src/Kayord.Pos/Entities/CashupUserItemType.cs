using Kayord.Pos.Common.Enums;

namespace Kayord.Pos.Entities;

public class CashUpUserItemType
{
    public int Id { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public bool IsAuto { get; set; }
    public bool AffectsGrossBalance { get; set; }
    public int Position { get; set; }
    public CashUpUserItemRule CashUpUserItemRule { get; set; }
    public int? PaymentTypeId { get; set; }
    public PaymentType? PaymentType { get; set; }
    public int? AdjustmentTypeId { get; set; }
    public AdjustmentType? AdjustmentType { get; set; }
    public int? CashupConfigId { get; set; }
    public CashUpConfig? CashupConfig { get; set; }

}