namespace Kayord.Pos.Entities;

public class Promotion
{
    public int PromotionId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int RequiredQuantity { get; set; }
    public int FreeQuantity { get; set; }
    public bool IsActive { get; set; }

    public bool IsApplicable(int purchasedQuantity)
    {
        return IsActive && purchasedQuantity >= RequiredQuantity;
    }

    public int GetFreeItems(int purchasedQuantity)
    {
        if (IsApplicable(purchasedQuantity))
        {
            return (purchasedQuantity / RequiredQuantity) * FreeQuantity;
        }
        return 0;
    }
}