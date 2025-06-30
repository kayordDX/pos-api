namespace Kayord.Pos.Entities;

public class Audit
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AuditTypeId { get; set; }
    public AuditType AuditType { get; set; } = default!;
    public string? UserId { get; set; }
    public string? Detail { get; set; }
}