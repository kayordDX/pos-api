using Kayord.Pos.Entities;

namespace Kayord.POS.Entities;

public class Business
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Outlet>? Outlets { get; set; }
}