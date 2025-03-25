using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem;

public static class OrderItemUpdate
{
    public static async Task StockCount(int stockOrderId, decimal fromActual, int divisionId, int stockId, decimal actual, AppDbContext dbContext, CurrentUserService currentUserService, CancellationToken ct)
    {
        // Update Stock Item Count 
        var item = await dbContext.StockItem
            .Where(x => x.StockId == stockId && x.DivisionId == divisionId)
            .FirstOrDefaultAsync(ct);

        bool isNew = false;
        if (item == null)
        {
            item = new StockItem()
            {
                StockId = stockId,
                DivisionId = divisionId,
                Actual = actual,
                Threshold = 0,
            };
            isNew = true;
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync(ct);
        }
        decimal previousActual = item?.Actual ?? 0;
        if (item != null)
        {
            if (!isNew)
            {
                item.Actual -= fromActual;
            }
            item.Actual += actual;


            if (previousActual != item.Actual)
            {
                await dbContext.StockItemAudit.AddAsync(new StockItemAudit()
                {
                    FromActual = previousActual,
                    ToActual = item.Actual,
                    StockItemAuditTypeId = 5,
                    StockItemId = item.Id,
                    UserId = currentUserService.UserId ?? "",
                    Updated = DateTime.Now,
                    StockId = stockId,
                    StockOrderId = stockOrderId,
                });
            }
        }
    }

    public static async Task StockOrderStatus(int stockOrderId, AppDbContext dbContext, CancellationToken ct)
    {
        var orderItems = await dbContext.StockOrderItem
            .AsNoTracking()
            .Where(x => x.StockOrderId == stockOrderId)
            .ToListAsync(ct);

        int orderStatusId = 1;
        if (orderItems.All(x => x.StockOrderItemStatusId > 1))
        {
            orderStatusId = 3;
        }
        else if (orderItems.Any(x => x.StockOrderItemStatusId > 1))
        {
            orderStatusId = 2;
        }

        var order = await dbContext.StockOrder
            .Where(x => x.Id == stockOrderId)
            .FirstOrDefaultAsync(ct);

        if (order == null)
        {
            return;
        }

        if (order.StockOrderStatusId != orderStatusId)
        {
            order.StockOrderStatusId = orderStatusId;
            await dbContext.SaveChangesAsync(ct);
        }
    }
}