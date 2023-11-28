namespace Kayord.Pos.Features.Table.GetAvailable;

public class Response
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int SectionId { get; set; }
    public SectionDto Section { get; set; } = default!;
}