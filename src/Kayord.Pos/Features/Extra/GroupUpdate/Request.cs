namespace Kayord.Pos.Features.Extra.GroupUpdate;

public class Request
{

    public int ExtraGroupId { get; set; }
    public bool isGlobal { get; set; }
    public string Name { get; set; } = string.Empty;

}

