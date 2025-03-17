namespace Kayord.Pos.Entities;

public class VATRate
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}