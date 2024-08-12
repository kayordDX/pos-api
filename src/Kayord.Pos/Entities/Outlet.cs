using Kayord.Pos.Entities;

namespace Kayord.Pos.Entities;

public class Outlet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
    public ICollection<Section>? Sections { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public ICollection<OutletPaymentType>? OutletPaymentTypes { get; set; }
    public string? Address { get; set; }
    public string? Company { get; set; }
    public string? Registration { get; set; }
}