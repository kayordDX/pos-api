namespace Kayord.Pos.Features.TableOrder.GetBill;

public class BillMenuItemDTO
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int BillCategoryId { get; set; }
    public BillCategoryDTO? BillCategory { get; set; }

}