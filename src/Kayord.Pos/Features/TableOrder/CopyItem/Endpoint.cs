using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.CopyItem;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/order/copyItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        OrderItem? orderItem = await _dbContext.OrderItem
            .Include(x => x.OrderItemOptions)
            .Include(x => x.OrderItemExtras)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OrderItemId == req.OrderItemId);

        if (orderItem == null)
        {
            await SendAsync(new Response() { IsSuccess = false });
        }
        else
        {
            orderItem.OrderItemId = 0;

            foreach (var i in orderItem.OrderItemOptions ?? [])
            {
                i.OrderItemOptionId = 0;
                i.OrderItem = orderItem;
                i.OrderItemId = 0;
            }
            foreach (var i in orderItem.OrderItemExtras ?? [])
            {
                i.OrderItemExtraId = 0;
                i.OrderItem = orderItem;
                i.OrderItemId = 0;
            }
            await _dbContext.OrderItem.AddAsync(orderItem);
            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { IsSuccess = true });
        }
    }
}
