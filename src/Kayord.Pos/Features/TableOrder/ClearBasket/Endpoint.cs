using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Order.ClearBasket;

public class Endpoint : Endpoint<Request, Pos.Entities.OrderItem>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/order/clearBasket");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        List<OrderItem>? entities = _dbContext.OrderItem.Where(x => x.TableBookingId == req.TableBookingId && x.OrderItemStatusId == 1).ToList();
        if (entities != null)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            await Send.OkAsync();
        }
        else
        {
            await Send.NotFoundAsync();
        }
    }
}