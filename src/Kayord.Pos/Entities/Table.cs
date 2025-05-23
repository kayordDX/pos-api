namespace Kayord.Pos.Entities;

public class Table : AuditableEntity
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int Position { get; set; }
    public int SectionId { get; set; }
    public Section Section { get; set; } = default!;
    public ICollection<Customer> Customers { get; set; } = default!;

    public bool isDeleted { get; set; }
}