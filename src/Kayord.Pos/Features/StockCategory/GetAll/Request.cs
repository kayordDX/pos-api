namespace Kayord.Pos.Features.StockCategory.GetAll;

public class Request
{
    public int OutletId { get; set; }
    public bool parentOnly { get; set; }
    public int? parentId { get; set; }


}