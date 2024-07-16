namespace Kayord.Pos.Entities;

public class Address
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HouseNr { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string Suburb { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}