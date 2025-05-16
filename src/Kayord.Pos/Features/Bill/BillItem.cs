namespace Kayord.Pos.Features.Bill;

public class BillItem
{
    public int Count { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
