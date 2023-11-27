namespace Kayord.Pos.Entities;

public class Table
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SectionId { get; set; }
    public Section Section { get; set; } = default!;
    public ICollection<Customer> Customers { get; set; } = default!;
}