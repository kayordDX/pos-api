using Kayord.Pos.Entities;

namespace Kayord.Pos.Entities;

public class Outlet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
    public ICollection<Section>? Sections { get; set; }
    // public ICollection<User>? User { get; set; }
}