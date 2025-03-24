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
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity, Type = StockItemAuditType.MenuItem })
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
            var menuItemBulkStock = await _dbContext.MenuItemBulkStock
                .Where(x => x.MenuItemId == orderInfo.MenuItemId)
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity, Type = StockItemAuditType.Bulk })
                .ToListAsync(ct);
            stockToUpdate.AddRange(menuItemBulkStock);

            foreach (var m in stockToUpdate)
            {
                var stockItem = await _dbContext.StockItem
                    .Where(x => x.StockId == m.StockId && x.DivisionId == orderInfo.DivisionId)
                    .FirstOrDefaultAsync(ct);

                if (stockItem == null) continue;

                bool isBulk = m.Type == StockItemAuditType.Bulk;
                int bulk = isBulk ? -1 : 1;

                decimal toActual = stockItem.Actual - m.Quantity * bulk;

                await StockCountAvailableCheck(stockItem.Id, stockItem.Actual, toActual, _dbContext, ct);

                if (stockItem.Actual != toActual)
                {
                    await _dbContext.StockItemAudit.AddAsync(new Entities.StockItemAudit()
                    {
                        OrderItemId = r,
                        FromActual = stockItem.Actual,
                        ToActual = toActual,
                        StockItemAuditTypeId = (int)m.Type,
                        StockItemId = stockItem.Id,
                        UserId = userId,
                        Updated = DateTime.Now,
                    });
                }

                stockItem.Actual -= m.Quantity * bulk;
            }
        }
        await _dbContext.SaveChangesAsync(ct);
    }

    public static async Task StockCountAvailableCheck(int stockId, decimal from, decimal to, AppDbContext dbContext, CancellationToken ct)
    {
        if (to < 0) to = 0;
        if (from < 0) from = 0;

        if (from <= 0 && from != to)
        {
            await StockUnavailable(stockId, true, dbContext, ct);
        }
        else if (to <= 0 && from != to)
        {
            await StockUnavailable(stockId, false, dbContext, ct);
        }
    }

    public static async Task StockUnavailable(int stockId, bool isAvailable, AppDbContext dbContext, CancellationToken ct)
    {
        await dbContext.Database.ExecuteSqlAsync($"""
            UPDATE menu_item mi
            SET is_available = {isAvailable}
            FROM menu_item_stock mis
            WHERE mi.menu_item_id = mis.menu_item_id 
            AND mis.stock_id = {stockId};
        """);

        await dbContext.Database.ExecuteSqlAsync($"""
            UPDATE extra i
            SET is_available = {isAvailable}
            FROM extra_stock o
            WHERE i.extra_id = o.extra_id 
            AND o.stock_id = {stockId};
        """);

        await dbContext.Database.ExecuteSqlAsync($"""
            UPDATE option i
            SET is_available = {isAvailable}
            FROM option_stock o
            WHERE i.option_id = o.option_id 
            AND o.stock_id = {stockId};
        """);

        await dbContext.Database.ExecuteSqlAsync($"""
            UPDATE menu_item mi
            SET is_available = {isAvailable}
            FROM menu_item_bulk_stock mis
            WHERE mi.menu_item_id = mis.menu_item_id 
            AND mis.stock_id = {stockId};
        """);
    }
}