using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock;

public class StockHandler : IEventHandler<StockEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public StockHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(StockEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        foreach (var item in eventModel.OrderItemIds)
        {
            await _dbContext.Database.ExecuteSqlAsync($"""
                update stock_item
                    set actual = si.actual - mis.quantity
                from order_item oi
                join menu_item mi
                    on oi.menu_item_id = mi.menu_item_id
                join menu_item_stock mis
                    on mis.menu_item_id = mi.menu_item_id
                join stock_item si
                    on si.stock_id = mis.stock_id
                and si.division_id = mi.division_id
                where stock_item.id = si.id
                and oi.order_item_id = {item};
            """);

            await _dbContext.Database.ExecuteSqlAsync($"""
                update stock_item
                    set actual = si.actual - es.quantity
                from order_item oi
                join menu_item mi
                    on oi.menu_item_id = mi.menu_item_id
                join order_item_extra oie
                    on oie.order_item_id = oi.order_item_id
                join extra_stock es
                    on es.extra_id = oie.extra_id
                join stock_item si
                    on si.stock_id = es.stock_id
                and si.division_id = mi.division_id
                where stock_item.id = si.id
                and oi.order_item_id = {item};
            """);

            await _dbContext.Database.ExecuteSqlAsync($"""
                update stock_item
                    set actual = si.actual - os.quantity
                from order_item oi
                join menu_item mi
                    on oi.menu_item_id = mi.menu_item_id
                join order_item_option oio
                    on oio.order_item_id = oi.order_item_id
                join option_stock os
                    on os.option_id = oio.option_id
                join stock_item si
                    on si.stock_id = os.stock_id
                and si.division_id = mi.division_id
                where stock_item.id = si.id
                and oi.order_item_id = {item};
            """);
        }

        // foreach (int r in eventModel.OrderItemIds)
        // {
        //     var orderItem = await _dbContext.OrderItem.FirstOrDefaultAsync(x => x.OrderItemId == r);
        //     if (orderItem == null)
        //     {
        //         continue;
        //     }
        //     // Menu Items
        //     var menuItemStock = await _dbContext.MenuItemStock.Where(x => x.MenuItemId == orderItem.MenuItemId)
        //         .Include(x => x.MenuItem)
        //         .ToListAsync();

        //     foreach (var m in menuItemStock)
        //     {
        //         var stockItem = await _dbContext.StockItem.FirstOrDefaultAsync(x => x.StockId == m.StockId && x.DivisionId == m.MenuItem.DivisionId);
        //         if (stockItem == null) continue;
        //         stockItem.Actual = stockItem.Actual - m.Quantity;
        //     }
        //     // Extras

        //     // Options

        // }
        // await _dbContext.SaveChangesAsync(ct);
    }
}