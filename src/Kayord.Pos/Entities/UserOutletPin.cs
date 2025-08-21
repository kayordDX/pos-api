namespace Kayord.Pos.Entities;

public class UserOutletPin : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public string Pin { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
    public required byte[] Iv { get; set; }
}