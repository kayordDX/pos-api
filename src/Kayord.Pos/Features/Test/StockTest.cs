
using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Test;

public class StockTest : EndpointWithoutRequest<bool>
{
    private readonly AppDbContext _dbContext;

    public StockTest(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test/stock");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<int> orderItemIds = [304943];

        foreach (var item in orderItemIds)
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
        await SendAsync(true);
    }
}