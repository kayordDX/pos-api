namespace Kayord.Pos.Entities;

public class BulkUploadConfig : AuditableEntity
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public bool IsActive { get; set; }
}