namespace Kayord.Pos.Features.Option.Update;

public class Request
{
    public int OptionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int OptionGroupId { get; set; }
}