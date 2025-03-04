namespace Kayord.Pos.Features.Stock.Update;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public bool HasVat { get; set; }
}