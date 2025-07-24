namespace Kayord.Pos.Entities;
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Order> Orders { get; set; } = default!;
}