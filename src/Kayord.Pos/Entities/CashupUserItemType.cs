namespace Kayord.Pos.Entities;

public class CashupUserItemType
{
    public int Id { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public bool isAuto { get; set; }
    public int? PaymentTypeId { get; set; }
    public PaymentType? PaymentType { get; set; }

    public int? CashupConfigId { get; set; }
    public CashupConfig? CashupConfig { get; set; }

}