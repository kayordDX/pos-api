namespace Kayord.Pos.Features.Option.Group.Update;

public class Request
{
    public int OptionGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinSelections { get; set; }
    public int MaxSelections { get; set; }
}