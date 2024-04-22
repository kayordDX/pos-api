namespace Kayord.Pos.Features.Order.BackOffice;
public class TableDTO
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public SectionDTO? Section { get; set; }

}