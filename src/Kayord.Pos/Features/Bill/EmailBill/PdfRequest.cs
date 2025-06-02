using Kayord.Pos.Features.TableOrder.GetBill;

namespace Kayord.Pos.Features.Bill.EmailBill;


public class PdfRequest
{
    public string OutletName { get; set; } = string.Empty;
    public int TableBookingId { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public string? Address { get; set; }
    public string? Company { get; set; }
    public string? Registration { get; set; }
    public DateTime BillDate { get; set; }
    public decimal Total { get; set; } = 0;
    public decimal TotalExVAT { get; set; } = 0;
    public decimal VAT { get; set; } = 0;
    public decimal Balance { get; set; } = 0;
    public decimal TipAmount { get; set; } = 0;
    public decimal PaymentReceived { get; set; } = 0;
    public List<Item> Items { get; set; } = new List<Item>();
    public string? TableName { get; set; }
    public string? Waiter { get; set; }
    public bool IsClosed { get; set; }
    public List<DivisionDTO> Divisions { get; set; } = new List<DivisionDTO>();
}

public class Item
{
    public int Count { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public List<SubItem>? Items { get; set; }
}

public class SubItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
