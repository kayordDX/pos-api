using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem;

public static class OrderItemUpdate
{
    public static async Task<StockOrderItem> UpdateOrderItem(StockOrderItem entity, Request req, AppDbContext dbContext, CurrentUserService currentUserService, CancellationToken ct)
    {
        // Update Stock Item Count 
        var item = await dbContext.StockItem
            .Where(x => x.StockId == req.StockId && x.DivisionId == entity.StockOrder.DivisionId)
            .FirstOrDefaultAsync(ct);

        bool isNew = false;
        if (item == null)
        {
            item = new StockItem()
            {
                StockId = req.StockId,
                DivisionId = entity.StockOrder.DivisionId,
                Actual = req.Actual,
                Threshold = 0,
            };
            isNew = true;
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        decimal previousActual = item?.Actual ?? 0;
        if (item != null)
        {
            if (!isNew)
            {
                item.Actual -= entity.Actual;
            }
            item.Actual += req.Actual;

            await dbContext.StockItemAudit.AddAsync(new StockItemAudit()
            {
                FromActual = previousActual,
                ToActual = item.Actual,
                StockItemAuditTypeId = 5,
                StockItemId = item.Id,
                UserId = currentUserService.UserId ?? "",
                Updated = DateTime.Now,
                StockId = req.StockId,
                StockOrderId = req.StockOrderId,
            });
        }

        // Check Order Items
        // If all closed close order

        await dbContext.SaveChangesAsync();
        return entity;
    }
}

public class Request
{
    public int StockOrderId { get; set; }
    public int StockId { get; set; }
    public decimal Actual { get; set; }
    public int StockOrderItemStatusId { get; set; }
}