using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock;

public static class StockManager
{
    public static async Task StockUpdate(List<int> orderItemIds, AppDbContext _dbContext, string userId, CancellationToken ct)
    {
        foreach (int r in orderItemIds)
        {
            var orderInfo = await _dbContext.OrderItem
                .Where(x => x.OrderItemId == r)
                .Select(x => new { x.MenuItemId, x.MenuItem.DivisionId })
                .FirstOrDefaultAsync(ct);

            if (orderInfo == null) continue;

            List<StockPatch> stockToUpdate = new();

            // Menu Items
            var menuItemStock = await _dbContext.MenuItemStock
                .Where(x => x.MenuItemId == orderInfo.MenuItemId)
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity, Type = 1 })
                .ToListAsync(ct);

            stockToUpdate.AddRange(menuItemStock);

            // Extras
            var extras = await _dbContext.Database.SqlQuery<StockPatch>($"""
                    select 
                        e.stock_id, 
                        e.quantity,
                        2 type
                    from order_item_extra o
                    join extra_stock e
                        on o.extra_id = e.extra_id
                    where o.order_item_id = {r};
                """).ToListAsync(ct);

            stockToUpdate.AddRange(extras);

            // Options
            var options = await _dbContext.Database.SqlQuery<StockPatch>($"""
                    select
                        s.stock_id,
                        s.quantity,
                        3 type
                    from
                    order_item_option o
                    join option_stock s
                        on s.option_id = o.option_id
                    where o.order_item_id = {r};
                """).ToListAsync(ct);

            stockToUpdate.AddRange(options);

            // Bulk Items
            // Type 4

            foreach (var m in stockToUpdate)
            {
                var stockItem = await _dbContext.StockItem
                    .Where(x => x.StockId == m.StockId && x.DivisionId == orderInfo.DivisionId)
                    .FirstOrDefaultAsync(ct);

                if (stockItem == null) continue;

                bool isBulk = m.Type == 4;
                int bulk = isBulk ? -1 : 1;

                await _dbContext.StockItemAudit.AddAsync(new Entities.StockItemAudit()
                {
                    OrderItemId = r,
                    FromActual = stockItem.Actual,
                    ToActual = stockItem.Actual - m.Quantity * bulk,
                    StockItemAuditTypeId = m.Type,
                    StockItemId = stockItem.Id,
                    UserId = userId,
                    Updated = DateTime.Now,
                });

                stockItem.Actual -= m.Quantity * bulk;
            }
        }
        await _dbContext.SaveChangesAsync(ct);
    }
}