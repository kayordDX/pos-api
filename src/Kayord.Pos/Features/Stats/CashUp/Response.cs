namespace Kayord.Pos.Features.Stats.CashUp;

public class Response
{
    public string Name { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal ActualSales { get; set; }
    public decimal Adjustments { get; set; }
    public decimal Tips { get; set; }
    public decimal TipsPercentage { get; set; }
}