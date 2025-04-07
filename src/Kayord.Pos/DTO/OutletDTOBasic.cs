namespace Kayord.Pos.DTO;

public class OutletDTOBasic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string VATNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Company { get; set; }
    public string? Registration { get; set; }
}