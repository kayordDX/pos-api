namespace Kayord.Pos.Features.TableOrder.GetBill;

public class BillMenuItemDTO
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int DivisionId { get; set; }
    public DivisionDTO? Division { get; set; }

}