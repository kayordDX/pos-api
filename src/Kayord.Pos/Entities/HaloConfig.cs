namespace Kayord.Pos.Entities;

public class HaloConfig : AuditableEntity
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string XApiKey { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}